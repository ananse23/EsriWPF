using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Microsoft.Practices.Prism.Commands;
using SOPFIM.Domain;
using Sopfim.ViewModels;
using Xceed.Wpf.Toolkit;

namespace SopfimMessage.ViewModel
{
    public sealed class MessageListViewModel : EditableListViewModel<MessageViewModel>
    {
        private int? _messageNumberBeforeNewMessage;
        private bool _isNew;
        private ITable _blockTable;

        public MessageListViewModel() : base()
        {
            this.EditEffectedCommands.Add(OpenLastMessage);
            this.EditEffectedCommands.Add(NewMessage);
        }

        #region properties

        private string _blocType;
        public string BlocType
        {
            get { return _blocType; }
            set
            {
                _blocType = value;
                RaisePropertyChanged("BlocType");
                RefreshFilter();
            }
        }

        private MessageViewModel _selectedMessage;
        public MessageViewModel SelectedMessage
        {
            get { return _selectedMessage; }
            set
            {
                _selectedMessage = value;
                RaisePropertyChanged("SelectedMessage");
            }
        }

        private string _blocNumber;
        public string BlocNumber
        {
            get { return _blocNumber; }
            set
            {
                _blocNumber = value;
                RaisePropertyChanged("BlocNumber");
                RefreshFilter();
            }
        }

        private string _baseOperation;
        public string BaseOperation
        {
            get { return _baseOperation; }
            set
            {
                _baseOperation = value;
                RaisePropertyChanged("BaseOperation");
                RefreshFilter();
            }
        }

        private int? _messageNumber;
        public int? MessageNumber
        {
            get { return _messageNumber; }
            set
            {
                _messageNumber = value;
                BeginEdit.RaiseCanExecuteChanged();
                RaisePropertyChanged("MessageNumber");
                QueryCurrentCriteria();
            }
        }

        private int? _lastMessageNumber;
        public int? LastMessageNumber
        {
            get { return _lastMessageNumber; }
            set
            {
                _lastMessageNumber = value;
                RaisePropertyChanged("LastMessageNumber");
            }
        }


        private DateTime? _messageSelectedDate;
        public DateTime? MessageSelectedDate
        {
            get { return _messageSelectedDate; }
        }

        private string _largeur;
        public string Largeur
        {
            get { return _largeur; }
            set
            {
                _largeur = value;
                RaisePropertyChanged("Largeur");
                RefreshFilter();
            }
        }
        #endregion 

        #region EditableListViewModel implementation
        
        protected override bool CanEdit()
        {
            return MessageNumber.HasValue && MessageNumber.Value > 0;
        }

        protected override string GenerteWhereClause()
        {
            var query = string.Empty;
            if (MessageNumber.HasValue && MessageNumber.Value > 0)
                query += string.Format("MessagesID = {0} and ", MessageNumber.Value);
            return string.IsNullOrEmpty(query) ? query : query.Substring(0, query.Length - 5);
        }

        protected override string TableName
        {
            get { return ConfigurationManager.AppSettings["MessageTableName"]; }
        }

        public override void InitialQuery()
        {
            var messages = this.QueryListData(null);
            LastMessageNumber = messages.Select(x => x.MessagesID).Max();
            MessageNumber = LastMessageNumber;
        }

        public override Func<MessageViewModel, bool> FilterCriteria
        {
            get { return (x => 
                (string.IsNullOrEmpty(BlocType) || x.TypeBloc == BlocType) &&
                (string.IsNullOrEmpty(BlocNumber) || x.NoBloc == BlocNumber) &&
                (string.IsNullOrEmpty(BaseOperation) || x.NomBase == BaseOperation) 
                ); 
            }
        }

        protected override void CancelData()
        {
            if (_isNew)
                _messageNumber = _messageNumberBeforeNewMessage;
            RaisePropertyChanged("MessageNumber");
            base.CancelData();
        }

        protected override void SaveData()
        {
            base.SaveData();
            if (_isNew)
            {
                LastMessageNumber = MessageNumber;
                _isNew = false;
            }
        }

        public override void QueryCurrentCriteria()
        {
            base.QueryCurrentCriteria();
            if(DataList.Count == 0)
                return;
            if (DataList.Select(x => x.MessagesID).Distinct().Count() > 1)
            {
                _messageNumber = 0;
                _messageSelectedDate = null;
            }
            else
            {
                _messageNumber = DataList[0].MessagesID;
                _messageSelectedDate = DataList[0].DateMessages;
                RaisePropertyChanged("MessageNumber");
                RaisePropertyChanged("MessageSelectedDate");
            }
            _isNew = false;
        }
        #endregion 
        
        #region commands
        private DelegateCommand _openLastMessage;
        public DelegateCommand OpenLastMessage
        {
            get
            {
                return _openLastMessage ?? (_openLastMessage = new DelegateCommand(() =>
                {
                    MessageNumber =
                        LastMessageNumber;
                },
                () => this.IsReadOnly));
            }
        }

        private DelegateCommand _newMessage;
        public DelegateCommand NewMessage
        {
            get
            {
                return _newMessage ?? (_newMessage = new DelegateCommand(CreateNewMessage, () => this.IsReadOnly));
            }
        }

        private void CreateNewMessage()
        {
            _messageNumberBeforeNewMessage = MessageNumber;
            _isNew = true;
            var list = QueryListData(string.Format("MessagesID = {0}", LastMessageNumber));
            var newList = new ObservableCollection<MessageViewModel>();
            var newMessageNumber = LastMessageNumber + 1;
            list.ForEach(x =>
            {
                x.MessagesID = newMessageNumber;
                x.DateMessages = DateTime.Now;
                x.IsDirty = true;
                newList.Add(x);
            });
            DataList = new ObservableCollection<MessageViewModel>(list);
            IsReadOnly = false;
            _messageNumber = newMessageNumber;
            _messageSelectedDate = DateTime.Now;
            RaisePropertyChanged("MessageNumber");
            RaisePropertyChanged("MessageSelectedDate");
            RefreshFilter();
        }

        private DelegateCommand _splitMessage;
        public DelegateCommand SplitMessageCommand
        {
            get
            {
                return _splitMessage ?? (_splitMessage = new DelegateCommand(SplitMessage));
            }
        }

        private DelegateCommand<string> _locate;
        public DelegateCommand<string> Locate
        {
            get
            {
                return _locate ?? (_locate = new DelegateCommand<string>(LocateBloc));
            }
        }

        private void SplitMessage()
        {
            var newRecord = (MessageViewModel) this.SelectedMessage.Clone() ;
            newRecord.LvTr = null;
            newRecord.DateTr = null;
            this.DataList.Insert(this.DataList.IndexOf(this.SelectedMessage) + 1, newRecord);
            RefreshFilter();
        }

        private void LocateBloc(string bloc)
        {
            _blockTable = _blockTable ?? DataService.GetTable(ConfigurationManager.AppSettings["BlocTableName"]);
            var result = DataService.GeneralQuery<BlocTBE>(_blockTable, string.Format("NoBloc = '{0}'", bloc));
            var envelopeTotal = new Envelope() as IEnvelope;
            result.ForEach(x =>
            {
                IEnvelope envelope = x.Shape.Envelope;
                envelopeTotal.Union(envelope);
            });
            MapService.ZoomToExtent(envelopeTotal);
        }
        #endregion 
    }
}