using System;
using ORMapping;

namespace SOPFIM.Domain
{
    public class SuiviPulverisation : EditableEntity
    {
        private int? _pulverisationID;
        [MappedField("PulverisationID")]
        public int? PulverisationID
        {
            get { return _pulverisationID; }
            set
            {
                if (_pulverisationID == value) return;
                _pulverisationID = value;
                RaisePropertyChanged("PulverisationID");
                IsDirty = true;
            }
        }

        private DateTime? _dateRapport;
        [MappedField("DateRapport")]
        public DateTime? DateRapport
        {
            get { return _dateRapport; }
            set
            {
                if (_dateRapport == value) return;
                _dateRapport = value;
                RaisePropertyChanged("DateRapport");
                IsDirty = true;
            }
        }

        private string _nomBase;
        [MappedField("NomBase", 30)]
        public string NomBase
        {
            get { return _nomBase; }
            set
            {
                if (_nomBase == value) return;
                _nomBase = value;
                RaisePropertyChanged("NomBase");
                IsDirty = true;
            }
        }

        private string _noBloc;
        [MappedField("NoBloc", 8)]
        public string NoBloc
        {
            get { return _noBloc; }
            set
            {
                if (_noBloc == value) return;
                _noBloc = value;
                RaisePropertyChanged("NoBloc");
                IsDirty = true;
            }
        }

        private string _produit;
        [MappedField("Produit", 20)]
        public string Produit
        {
            get { return _produit; }
            set
            {
                if (_produit == value) return;
                _produit = value;
                RaisePropertyChanged("Produit");
                IsDirty = true;
            }
        }

        private string _periode;
        [MappedField("Periode", 2)]
        public string Periode
        {
            get { return _periode; }
            set
            {
                if (_periode == value) return;
                _periode = value;
                RaisePropertyChanged("Periode");
                IsDirty = true;
            }
        }

        private string _traitement;
        [MappedField("Traitement", 10)]
        public string Traitement
        {
            get { return _traitement; }
            set
            {
                if (_traitement == value) return;
                _traitement = value;
                RaisePropertyChanged("Traitement");
                IsDirty = true;
            }
        }

        private string _raison;
        [MappedField("Raison", 30)]
        public string Raison
        {
            get { return _raison; }
            set
            {
                if (_raison == value) return;
                _raison = value;
                RaisePropertyChanged("Raison");
                IsDirty = true;
            }
        }

        private string _application;
        [MappedField("Application", 1)]
        public string Application
        {
            get { return _application; }
            set
            {
                if (_application == value) return;
                _application = value;
                RaisePropertyChanged("Application");
                IsDirty = true;
            }
        }

        private DateTime? _dateTr;
        [MappedField("DateTr")]
        public DateTime? DateTr
        {
            get { return _dateTr; }
            set
            {
                if (_dateTr == value) return;
                _dateTr = value;
                RaisePropertyChanged("DateTr");
                IsDirty = true;
            }
        }

        private string _largeurTr;
        [MappedField("LargeurTr", 4)]
        public string LargeurTr
        {
            get { return _largeurTr; }
            set
            {
                if (_largeurTr == value) return;
                _largeurTr = value;
                RaisePropertyChanged("LargeurTr");
                IsDirty = true;
            }
        }

        private string _lvTr;
        [MappedField("LvTr", 50)]
        public string LvTr
        {
            get { return _lvTr; }
            set
            {
                if (_lvTr == value) return;
                _lvTr = value;
                RaisePropertyChanged("LvTr");
                IsDirty = true;
            }
        }

        private string _etatBloc;
        [MappedField("EtatBloc", 20)]
        public string EtatBloc
        {
            get { return _etatBloc; }
            set
            {
                if (_etatBloc == value) return;
                _etatBloc = value;
                RaisePropertyChanged("EtatBloc");
                IsDirty = true;
            }
        }

        private string _lvTrPartiel;
        [MappedField("LvTrPartiel", 50)]
        public string LvTrPartiel
        {
            get { return _lvTrPartiel; }
            set
            {
                if (_lvTrPartiel == value) return;
                _lvTrPartiel = value;
                RaisePropertyChanged("LvTrPartiel");
                IsDirty = true;
            }
        }
    }
}
