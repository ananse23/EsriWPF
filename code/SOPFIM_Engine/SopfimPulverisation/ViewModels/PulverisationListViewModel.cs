using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ESRI.ArcGIS.Geodatabase;
using SOPFIM.Domain;
using SOPFIM.Domain.BD_SOPFIMProject;
using Sopfim.CustomControls;
using Sopfim.ViewModels;

namespace SopfimPulverisation.ViewModels
{
    public sealed class PulverisationListViewModel : EditableListViewModel<SuiviPulverisation>
    {
        private ITable _messageTable;
        public Dictionary<string, SopfimVolume> Volumes;

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
            return string.IsNullOrEmpty(query) ? query : query.Substring(0, query.Length - 5);
        }

        protected override string TableName
        {
            get { return ConfigurationManager.AppSettings["pulverisationTableName"]; }
        }

        public override void InitialQuery()
        {
            QueryCurrentCriteria();
            var volumeList = DataService.GeneralQuery<SopfimVolume>(DataService.GetTable("Volume"), null);
            Volumes = new Dictionary<string, SopfimVolume>();
            volumeList.ForEach(e => Volumes.Add(e.NomBase, e));
            SelectedVolCumulatif = Volumes[BaseOperation].VolCumulatif;
            SelectedVolProgramme = Volumes[BaseOperation].VolProgramme;
            RaisePropertyChanged("SelectedVolProgramme");
            RaisePropertyChanged("SelectedVolCumulatif");
        }

        public override Func<SuiviPulverisation, bool> FilterCriteria
        {
            get { return (x => 
                (string.IsNullOrEmpty(BaseOperation) || x.NomBase == BaseOperation) &&
                (!Traitement.HasValue || x.Traitement == (Traitement.Value ? "Oui" : "Non")) &&
                (string.IsNullOrEmpty(Raison) || x.Raison == Raison) 
                ); 
            }
        }


        private DateTime? _dateRapport;
        public DateTime? DateRepport
        {
            get { return _dateRapport; }
            set
            {
                _dateRapport = value;
                RaisePropertyChanged("DateRepport");
                QueryCurrentCriteria();
            }
        }

        private string _baseOperation;
        public string BaseOperation
        {
            get { return _baseOperation; }
            set { _baseOperation = value;
                RaisePropertyChanged("BaseOperation");
                if (string.IsNullOrEmpty(_baseOperation))
                    return;
                SelectedVolCumulatif = Volumes[value].VolCumulatif;
                SelectedVolProgramme = Volumes[value].VolProgramme;
                RaisePropertyChanged("SelectedVolProgramme");
                RaisePropertyChanged("SelectedVolCumulatif");
                RefreshFilter();
            }
        }

        public double? SelectedVolProgramme { get; set; }
        public double? SelectedVolCumulatif { get; set; }

        private bool? _traitement;
        public bool? Traitement
        {
            get { return _traitement; }
            set
            {
                _traitement = value;
                RaisePropertyChanged("Traitement");
                RefreshFilter();
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
                RefreshFilter();
            }
        }


        protected override void SaveData()
        {
            this.DataList.ToList().ForEach(y =>
                    {
                        y.IsDirty = true;
                        y.DateRapport = this.DateRepport;
                        y.Traitement = this.Traitement.HasValue ? (this.Traitement.Value ? "Oui" : "Non") : null;
                        y.Raison = this.Raison;
                        y.NomBase = this.BaseOperation;
                    });
            base.SaveData();
            SaveMessage(DataList.ToList());
        }

        private void SaveMessage(List<SuiviPulverisation> sprays)
        {
            _messageTable = _messageTable ?? DataService.GetTable(ConfigurationManager.AppSettings["MessageTableName"]);
            var blockArray = sprays.Select(x => string.Format("'{0}'", x.NoBloc)).ToArray();
            var blockString = string.Format("NoBloc in ({0})", string.Join(",", blockArray));
            var messageList = DataService.GeneralQuery<MessageViewModel>(_messageTable, blockString);
            messageList.ForEach(x =>
                                    {
                                        var sprayData = DataList.FirstOrDefault(y => y.NoBloc == x.NoBloc);
                                        x.LvTr = sprayData.LvTr;
                                        x.DateTr = sprayData.DateRapport;
                                    } );
            DataService.Save(messageList, _messageTable);
        }
        
    }
}