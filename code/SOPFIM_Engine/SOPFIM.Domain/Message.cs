using System;
using ORMapping;

namespace SOPFIM.Domain
{
    public class Message : EditableEntity
    {
        private int? _messagesID;
        [MappedField("MessagesID")]
        public int? MessagesID
        {
            get { return _messagesID; }
            set
            {
                if (_messagesID == value) return;
                _messagesID = value;
                RaisePropertyChanged("MessagesID");
                IsDirty = true;
            }
        }

        private DateTime? _dateMessages;
        [MappedField("DateMessages")]
        public DateTime? DateMessages
        {
            get { return _dateMessages; }
            set
            {
                if (_dateMessages == value) return;
                _dateMessages = value;
                RaisePropertyChanged("DateMessages");
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

        private string _timingIDI;
        [MappedField("TimingIDI", 3)]
        public string TimingIDI
        {
            get { return _timingIDI; }
            set
            {
                if (_timingIDI == value) return;
                _timingIDI = value;
                RaisePropertyChanged("TimingIDI");
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

        private string _prescription;
        [MappedField("Prescription", 8)]
        public string Prescription
        {
            get { return _prescription; }
            set
            {
                if (_prescription == value) return;
                _prescription = value;
                RaisePropertyChanged("Prescription");
                IsDirty = true;
            }
        }

        private string _appPrevue;
        [MappedField("AppPrevue", 6)]
        public string AppPrevue
        {
            get { return _appPrevue; }
            set
            {
                if (_appPrevue == value) return;
                _appPrevue = value;
                RaisePropertyChanged("AppPrevue");
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

        private int? _interApp;
        [MappedField("InterApp")]
        public int? InterApp
        {
            get { return _interApp; }
            set
            {
                if (_interApp == value) return;
                _interApp = value;
                RaisePropertyChanged("InterApp");
                IsDirty = true;
            }
        }

        private string _prioriteEtat;
        [MappedField("PrioriteEtat", 25)]
        public string PrioriteEtat
        {
            get { return _prioriteEtat; }
            set
            {
                if (_prioriteEtat == value) return;
                _prioriteEtat = value;
                RaisePropertyChanged("PrioriteEtat");
                IsDirty = true;
            }
        }

        private double? _ouvert_1jr;
        [MappedField("Ouvert_1jr")]
        public double? Ouvert_1jr
        {
            get { return _ouvert_1jr; }
            set
            {
                if (_ouvert_1jr == value) return;
                _ouvert_1jr = value;
                RaisePropertyChanged("Ouvert_1jr");
                IsDirty = true;
            }
        }

        private int? _nbreLv30m;
        [MappedField("NbreLv30m")]
        public int? NbreLv30m
        {
            get { return _nbreLv30m; }
            set
            {
                if (_nbreLv30m == value) return;
                _nbreLv30m = value;
                RaisePropertyChanged("NbreLv30m");
                IsDirty = true;
            }
        }

        private DateTime? _datePrevision;
        [MappedField("DatePrevision")]
        public DateTime? DatePrevision
        {
            get { return _datePrevision; }
            set
            {
                if (_datePrevision == value) return;
                _datePrevision = value;
                RaisePropertyChanged("DatePrevision");
                IsDirty = true;
            }
        }

        private DateTime? _dateOuverture;
        [MappedField("DateOuverture")]
        public DateTime? DateOuverture
        {
            get { return _dateOuverture; }
            set
            {
                if (_dateOuverture == value) return;
                _dateOuverture = value;
                if (_dateOuverture.HasValue)
                    _datePrevision = null;
                
                RaisePropertyChanged("DateOuverture");
                RaisePropertyChanged("DatePrevision");
                RaisePropertyChanged("DatePrevisionIsEnabled");

                IsDirty = true;
            }
        }

        private string _lvOuv30m;
        [MappedField("LvOuv30m", 50)]
        public string LvOuv30m
        {
            get { return _lvOuv30m; }
            set
            {
                if (_lvOuv30m == value) return;
                _lvOuv30m = value;
                RaisePropertyChanged("LvOuv30m");
                IsDirty = true;
            }
        }

        private string _lvOuv80m;
        [MappedField("LvOuv80m", 50)]
        public string LvOuv80m
        {
            get { return _lvOuv80m; }
            set
            {
                if (_lvOuv80m == value) return;
                _lvOuv80m = value;
                RaisePropertyChanged("LvOuv80m");
                IsDirty = true;
            }
        }

        private string _lvOuv100m;
        [MappedField("LvOuv100m", 50)]
        public string LvOuv100m
        {
            get { return _lvOuv100m; }
            set
            {
                if (_lvOuv100m == value) return;
                _lvOuv100m = value;
                RaisePropertyChanged("LvOuv100m");
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

        public bool DatePrevisionIsEnabled
        {
            get { return !DateOuverture.HasValue; }
        }
    }
}