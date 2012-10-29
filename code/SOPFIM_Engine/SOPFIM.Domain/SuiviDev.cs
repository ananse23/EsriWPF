using System;
using ORMapping;

namespace SOPFIM.Domain
{
    public class SuiviDev : EditableEntity
    {
        private int? _developpementID;
        [MappedField("DeveloppementID")]
        public int? DeveloppementID
        {
            get { return _developpementID; }
            set
            {
                if (_developpementID == value) return;
                _developpementID = value;
                RaisePropertyChanged("DeveloppementID");
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

        private string _noPe;
        [MappedField("NoPe", 9)]
        public string NoPe
        {
            get { return _noPe; }
            set
            {
                if (_noPe == value) return;
                _noPe = value;
                RaisePropertyChanged("NoPe");
                IsDirty = true;
            }
        }

        private string _typeBloc;
        [MappedField("TypeBloc", 20)]
        public string TypeBloc
        {
            get { return _typeBloc; }
            set
            {
                if (_typeBloc == value) return;
                _typeBloc = value;
                RaisePropertyChanged("TypeBloc");
                IsDirty = true;
            }
        }

        private DateTime? _dateAnalyse;
        [MappedField("DateAnalyse")]
        public DateTime? DateAnalyse
        {
            get { return _dateAnalyse; }
            set
            {
                if (_dateAnalyse == value) return;
                _dateAnalyse = value;
                RaisePropertyChanged("DateAnalyse");
                IsDirty = true;
            }
        }

        private DateTime? _dateRecolte;
        [MappedField("DateRecolte")]
        public DateTime? DateRecolte
        {
            get { return _dateRecolte; }
            set
            {
                if (_dateRecolte == value) return;
                _dateRecolte = value;
                RaisePropertyChanged("DateRecolte");
                IsDirty = true;
            }
        }

        private double? _iDP;
        [MappedField("IDP")]
        public double? IDP
        {
            get { return _iDP; }
            set
            {
                if (_iDP == value) return;
                _iDP = value;
                RaisePropertyChanged("IDP");
                IsDirty = true;
            }
        }

        private double? _iDI;
        [MappedField("IDI")]
        public double? IDI
        {
            get { return _iDI; }
            set
            {
                if (_iDI == value) return;
                _iDI = value;
                RaisePropertyChanged("IDI");
                IsDirty = true;
            }
        }

        private double? _larvesBr;
        [MappedField("LarvesBr")]
        public double? LarvesBr
        {
            get { return _larvesBr; }
            set
            {
                if (_larvesBr == value) return;
                _larvesBr = value;
                RaisePropertyChanged("LarvesBr");
                IsDirty = true;
            }
        }

        private double? _larvesBg;
        [MappedField("LarvesBg")]
        public double? LarvesBg
        {
            get { return _larvesBg; }
            set
            {
                if (_larvesBg == value) return;
                _larvesBg = value;
                RaisePropertyChanged("LarvesBg");
                IsDirty = true;
            }
        }

        private double? _defoliation;
        [MappedField("Defoliation")]
        public double? Defoliation
        {
            get { return _defoliation; }
            set
            {
                if (_defoliation == value) return;
                _defoliation = value;
                RaisePropertyChanged("Defoliation");
                IsDirty = true;
            }
        }

        private double? _moyIDP;
        [MappedField("MoyIDP")]
        public double? MoyIDP
        {
            get { return _moyIDP; }
            set
            {
                if (_moyIDP == value) return;
                _moyIDP = value;
                RaisePropertyChanged("MoyIDP");
                IsDirty = true;
            }
        }

        private double? _moyIDI;
        [MappedField("MoyIDI")]
        public double? MoyIDI
        {
            get { return _moyIDI; }
            set
            {
                if (_moyIDI == value) return;
                _moyIDI = value;
                RaisePropertyChanged("MoyIDI");
                IsDirty = true;
            }
        }

        private double? _moyLarvesBr;
        [MappedField("MoyLarvesBr")]
        public double? MoyLarvesBr
        {
            get { return _moyLarvesBr; }
            set
            {
                if (_moyLarvesBr == value) return;
                _moyLarvesBr = value;
                RaisePropertyChanged("MoyLarvesBr");
                IsDirty = true;
            }
        }

        private double? _moyLarvesBg;
        [MappedField("MoyLarvesBg")]
        public double? MoyLarvesBg
        {
            get { return _moyLarvesBg; }
            set
            {
                if (_moyLarvesBg == value) return;
                _moyLarvesBg = value;
                RaisePropertyChanged("MoyLarvesBg");
                IsDirty = true;
            }
        }

        private double? _moyDefoliation;
        [MappedField("MoyDefoliation")]
        public double? MoyDefoliation
        {
            get { return _moyDefoliation; }
            set
            {
                if (_moyDefoliation == value) return;
                _moyDefoliation = value;
                RaisePropertyChanged("MoyDefoliation");
                IsDirty = true;
            }
        }

        private string _statut;
        [MappedField("Statut", 30)]
        public string Statut
        {
            get { return _statut; }
            set
            {
                if (_statut == value) return;
                _statut = value;
                RaisePropertyChanged("Statut");
                IsDirty = true;
            }
        }
    }
}
