using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using ESRI.ArcGIS.Geometry;
using Microsoft.Practices.Prism.Commands;
using SOPFIM.DataLayer;
using SOPFIM.Domain;
using Sopfim.ViewModels;

namespace SopfimMessage.ViewModel
{
    public class MessageEntityViewModel : EditableDataViewModel<Message>
    {
        private bool _isNew;

        protected override string WhereTemplate
        {
            get { return "TypeBloc = '{0}' and NomBase = '{1}' and MessagesID = {2}"; }
        }

        protected override string GenerteWhereClause()
        {
            const string dateString = "EXTRACT(YEAR FROM DateMessages) = {0} AND EXTRACT(MONTH FROM DateMessages) = {1} AND EXTRACT(DAY FROM DateMessages) = {2} AND ";
            var query = string.Empty;
            if (!string.IsNullOrEmpty(BlocType))
                query += string.Format("TypeBloc = '{0}' and ", BlocType);
            if (!string.IsNullOrEmpty(BaseOperation))
                query += string.Format("NomBase = '{0}' and ", BaseOperation);
            if (MessageNumber.HasValue && MessageNumber.Value > 0)
                query += string.Format("MessagesID = {0} and ", MessageNumber.Value);
            if (MessageSelectedDate.HasValue)
                query += string.Format(dateString, MessageSelectedDate.Value.Year, MessageSelectedDate.Value.Month,
                                       MessageSelectedDate.Value.Day);
            if (!string.IsNullOrEmpty(BlocNumber))
                query += string.Format("NoBloc = '{0}' and ", BlocNumber);
            return string.IsNullOrEmpty(query) ? query : query.Substring(0, query.Length - 5);
        }

        protected override string TableName
        {
            get
            {
                return ConfigurationManager.AppSettings["MessageTableName"];
            }
        }

        private string _blocType;
        public string BlocType
        {
            get { return _blocType; }
            set
            {
                _blocType = value;
                RaisePropertyChanged("BlocType");
                QueryData();
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
                QueryData();
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
                QueryData();
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
                QueryData();
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
            set
            {
                _messageSelectedDate = value;
                _messageNumber = null;
                RaisePropertyChanged("MessageSelectedDate");
                QueryData();
            }
        }

        private string _largeur;
        public string Largeur
        {
            get { return _largeur; }
            set { _largeur = value;
            RaisePropertyChanged("Largeur");
                QueryData();
            }
        }

        private DelegateCommand _openLastMessage;
        public DelegateCommand OpenLastMessage
        {
            get
            {
                return _openLastMessage ?? (_openLastMessage = new DelegateCommand(() =>
                                                                                       {
                                                                                           MessageNumber =
                                                                                               LastMessageNumber;
                                                                                       }));
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

        private void LocateBloc(string bloc)
        {
            if (_bufferRepository == null)
                _bufferRepository = new Repository<BufferLvTBE>(DataService, ConfigurationManager.AppSettings["BlocTableName"]);
            var result = _bufferRepository.QueryData(string.Format("NoBloc = '{0}'", bloc));
            var envelopeTotal = new Envelope() as IEnvelope;
            result.ForEach(x =>
            {
                IEnvelope envelope = x.Shape.Envelope;
                envelopeTotal.Union(envelope);
            });
            MapService.ZoomToExtent(envelopeTotal);
        }

        public override void InitialQuery()
        {
            var messages = Repository.QueryData(null);
            LastMessageNumber = messages.Select(x => x.MessagesID).Max();
            MessageNumber = LastMessageNumber;
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
            _isNew = true;
            var list = Repository.QueryData(string.Format("MessagesID = {0}", LastMessageNumber));
            var newList = new ObservableCollection<Message>();
            LastMessageNumber++;
            list.ForEach(x =>
                             {
                                 x.MessagesID = LastMessageNumber;
                                 x.DateMessages = DateTime.Now;
                                 newList.Add(x);
                             });
            DataList = new ObservableCollection<Message>(list);
            IsReadOnly = false;
        }

        protected override void CancelData()
        {
            base.CancelData();
            if (_isNew)
                LastMessageNumber--;
        }

        private IRepository<BufferLvTBE> _bufferRepository;
        

        public override void QueryData()
        {
            base.QueryData();
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
            //_blocTable = DataService.GetTable(ConfigurationManager.AppSettings["BufferTableName"]);
            //if (string.IsNullOrEmpty(Largeur)) return;
            //var query = string.Format("LargeurTr = '{0}'", Largeur);
            //var bufferList = DataService.GeneralQuery<BufferLvTBE>(_blocTable, query);
            //var blocList = bufferList.Select(x => x.NoBloc).Distinct().ToList();
            //DataList = new ObservableCollection<Message>(DataList.Where(x => blocList.Contains(x.NoBloc)).ToList());
        }

    }
}