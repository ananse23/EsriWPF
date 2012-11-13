using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ESRI.ArcGIS.Geodatabase;
using SOPFIM.Domain;
using Sopfim.CustomControls;
using Sopfim.ViewModels;

namespace SopfimPulverisation.ViewModels
{
    public class PulverisationListViewModel : EditableListViewModel<SuiviPulverisation>
    {
        private ITable _messageTable;

        public PulverisationListViewModel() : base()
        {
            _dateRapport = ((DateTime?) DateTime.Now).ToSopfimDateTime();
            _baseOperation = "Baie-Comeau";
        }

        protected override string GenerteWhereClause()
        {
            var query = string.Empty;
            if (this.DateRepport.HasValue)
                query += string.Format("DateRapport = date '{0}' and ",
                                       this.DateRepport.Value.ToString("yyyy-MM-dd HH:mm:ss"));
            if (!string.IsNullOrEmpty(this.BaseOperation))
                query += string.Format("NomBase = '{0}' and ", BaseOperation);
            if (Traitement.HasValue)
                query += string.Format("Traitement = '{0}' and ", Traitement.Value ? "Oui" : "Non");
            if (!string.IsNullOrEmpty(this.Raison))
                query += string.Format("Raison = '{0}' and ", Raison);
            return string.IsNullOrEmpty(query) ? query : query.Substring(0, query.Length - 5);
        }

        protected override string TableName
        {
            get { return ConfigurationManager.AppSettings["pulverisationTableName"]; }
        }

        public override void InitialQuery()
        {
            QueryCurrentSelection();
        }


        private DateTime? _dateRapport;
        public DateTime? DateRepport
        {
            get { return _dateRapport; }
            set
            {
                _dateRapport = value;
                RaisePropertyChanged("DateRepport");
                QueryCurrentSelection();
            }
        }

        private string _baseOperation;
        public string BaseOperation
        {
            get { return _baseOperation; }
            set { _baseOperation = value;
            RaisePropertyChanged("BaseOperation");
            }
        }

        private bool? _traitement;
        public bool? Traitement
        {
            get { return _traitement; }
            set
            {
                _traitement = value;
                RaisePropertyChanged("Traitement");
                QueryCurrentSelection();
            }
        }

        private string _raison;
        public string Raison
        {
            get { return _raison; }
            set
            {
                _raison = value;
                RaisePropertyChanged("Raison");
                QueryCurrentSelection();
            }
        }


        protected override void SaveDataList()
        {
            this.DataList.ToList().ForEach(y =>
                    {
                        y.IsDirty = true;
                        y.DateRapport = this.DateRepport;
                        y.Traitement = this.Traitement.HasValue ? (this.Traitement.Value ? "Oui" : "Non") : null;
                        y.Raison = this.Raison;
                        y.NomBase = this.BaseOperation;
                    });
            base.SaveDataList();
            SaveMessage(DataList.ToList());
        }

        private void SaveMessage(List<SuiviPulverisation> sprays)
        {
            _messageTable = _messageTable ?? DataService.GetTable(ConfigurationManager.AppSettings["MessageTableName"]);
            sprays.ForEach(x =>
                               {
                                   var whereClause = string.Format("NoBloc = '{0}' and DatePrevision = date '{1}'",
                                                                   x.NoBloc,
                                                                   x.DateRapport.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                                   var messages = DataService.GeneralQuery<MessageViewModel>(_messageTable, whereClause);
                                   messages.ToList().ForEach(y =>
                                                                 {
                                                                     y.LvTr = "1-20";
                                                                 });
                                   //DataService.Save();
                               });
        }
        
    }
}