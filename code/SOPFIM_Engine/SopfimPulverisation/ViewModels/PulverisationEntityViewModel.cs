using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using ORMapping;
using SOPFIM.Domain;
using Sopfim.ViewModels;

namespace SopfimPulverisation.ViewModels
{
    public class PulverisationEntityViewModel : EditableDataViewModel<SuiviPulverisation>
    {
        protected override string WhereTemplate
        {
            get { return string.Empty; }
        }

        protected override string GenerteWhereClause()
        {
            return "NomBase = 'xxx'";
        }

        protected override string TableName
        {
            get { return ConfigurationManager.AppSettings["pulverisationTableName"]; }
        }

        public override void InitialQuery()
        {
            throw new NotImplementedException();
        }


        private DateTime? _dateRapport;

        public DateTime? DateRepport
        {
            get { return _dateRapport; }
            set
            {
                _dateRapport = value;
                RaisePropertyChanged("DateRepport");
            }
        }

        private string _periode;

        public string Periode
        {
            get { return _periode; }
            set
            {
                _periode = value;
                RaisePropertyChanged("Periode");
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
            }
        }

        private string _nomBase;

        public string NomBase
        {
            get { return _nomBase; }
            set
            {
                _nomBase = value;
                RaisePropertyChanged("NomBase");
            }
        }

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

        public List<string> Periods = new List<string> {"AM", "PM"};

        protected override void SaveDataList()
        {
            this.DataList.Where(x => x.IsDirty).ToList().ForEach(y =>
            {
                if (y.OID < 0)
                {
                    y.DateRapport = this.DateRepport;
                    this.Traitement = this.Traitement.HasValue && this.Traitement.Value;
                    y.Traitement = this.Traitement.Value ? "Oui" : "Non";
                    y.NomBase = this.NomBase;
                    y.Raison = this.Raison;
                    y.InsertInto(DataService.GetTable(TableName));
                }
                else
                    y.Update();
                y.IsDirty = false;
            });
        }
    }
}