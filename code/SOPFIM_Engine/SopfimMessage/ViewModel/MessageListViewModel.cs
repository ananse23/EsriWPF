using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Microsoft.Practices.Prism.Commands;
using SOPFIM.Domain;
using Sopfim.ViewModels;

namespace SopfimMessage.ViewModel
{
    public class MessageListViewModel : EditableListViewModel<MessageViewModel>
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
                QueryCurrentSelection();
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
                QueryCurrentSelection();
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
                QueryCurrentSelection();
            }
        }

        private int? _messageNumber;
        public int? MessageNumber
        {
            get { return _messageNumber; }
            set
            {
                _messageNumber = value;
                _messageSelectedDate = null;
                RaisePropertyChanged("MessageNumber");
                QueryCurrentSelection();
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
                QueryCurrentSelection();
            }
        }
        #endregion 

        #region EditableListViewModel implementation
        protected override string GenerteWhereClause()
        {
            var query = string.Empty;
            if (!string.IsNullOrEmpty(BlocType))
                query += string.Format("TypeBloc = '{0}' and ", BlocType);
            if (!string.IsNullOrEmpty(BaseOperation))
                query += string.Format("NomBase = '{0}' and ", BaseOperation);
            if (MessageNumber.HasValue && MessageNumber.Value > 0)
                query += string.Format("MessagesID = {0} and ", MessageNumber.Value);
            if (!string.IsNullOrEmpty(BlocNumber))
                query += string.Format("NoBloc = '{0}' and ", BlocNumber);
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

        protected override void CancelData()
        {
            if (_isNew)
                _messageNumber = _lastMessageNumber;
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

        public override void QueryCurrentSelection()
        {
            base.QueryCurrentSelection();
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
            //LastMessageNumber++;
            var newMessageNumber = LastMessageNumber + 1;
            list.ForEach(x =>
            {
                x.MessagesID = newMessageNumber;
                x.DateMessages = DateTime.Now;
                newList.Add(x);
            });
            DataList = new ObservableCollection<MessageViewModel>(list);
            IsReadOnly = false;
            _messageNumber = newMessageNumber;
            _messageSelectedDate = DateTime.Now;
            RaisePropertyChanged("MessageNumber");
            RaisePropertyChanged("MessageSelectedDate");
        }

        private DelegateCommand<string> _locate;
        public DelegateCommand<string> Locate
        {
            get
            {
                return _locate ?? (_locate = new DelegateCommand<string>(LocateBloc));
            }
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