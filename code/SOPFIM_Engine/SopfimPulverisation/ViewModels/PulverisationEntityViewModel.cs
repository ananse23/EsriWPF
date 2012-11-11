using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using ORMapping;
using SOPFIM.Domain;
using Sopfim.CustomControls;
using Sopfim.ViewModels;

namespace SopfimPulverisation.ViewModels
{
    public class PulverisationEntityViewModel : EditableListViewModel<SuiviPulverisation>
    {
        public PulverisationEntityViewModel()
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
//            if (!string.IsNullOrEmpty(NomBase))
//                query += string.Format("NomBase = '{0}' and ", NomBase);
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

/*
        private double? _volumeProgram;
        public double? VolumeProgram
        {
            get { return _volumeProgram; }
            set { _volumeProgram = value;
            RaisePropertyChanged("VolumeProgram");
            }
        }

        private double? _volumeCumulatif;
        public double? VolumeCumulatif
        {
            get { return _volumeCumulatif; }
            set { _volumeCumulatif = value;
            RaisePropertyChanged("VolumeCumulatif");
            }
        }

        public List<string> Periods = new List<string> {"AM", "PM"};*/

        protected override void SaveDataList()
        {
            this.DataList.ToList().ForEach(y =>
                    {
                        y.IsDirty = true;
                        y.DateRapport = this.DateRepport;
                        y.Traitement = this.Traitement.HasValue ? (this.Traitement.Value ? "Oui" : "Non") : null;
                        y.Raison = this.Raison;
                    });
            base.SaveDataList();
/*
            this.DataList.Where(x => x.IsDirty).ToList().ForEach(y =>
            {
                if (y.OID < 0)
                {
                    y.DateRapport = this.DateRepport;
//                    this.Traitement = this.Traitement.HasValue && this.Traitement.Value;
//                    y.Traitement = this.Traitement.Value ? "Oui" : "Non";
//                    y.NomBase = this.NomBase;
//                    y.Raison = this.Raison;
                    y.InsertInto(DataService.GetTable(TableName));
                }
                else
                    y.Update();
                y.IsDirty = false;
            });
*/
            
        }

        
    }
}