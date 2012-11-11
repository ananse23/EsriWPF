using System;
using Microsoft.Practices.Prism.Commands;
using ORMapping;
using SOPFIM.Domain;

namespace Sopfim.ViewModels
{
    public class MessageViewModel : SuiviMessage
    {

        [MappedField("DateOuverture")]
        public override DateTime? DateOuverture
        {
            get
            {
                return base.DateOuverture;
            }
            set
            {
                if (base.DateOuverture == value) return;
                if (value.HasValue)
                    DatePrevision = null;
                base.DateOuverture = value;
                RaisePropertyChanged("DatePrevision");
                RaisePropertyChanged("DateOuverture");
                RaisePropertyChanged("DatePrevisionIsEnabled");
                IsDirty = true;
            }
        }

        [MappedField("LvTr", 50)]
        public override string LvTr
        {
            get
            {
                return base.LvTr;
            }
            set
            {
                if (base.LvTr == value) return;
                base.LvTr = value;
            }
        }

        public bool DatePrevisionIsEnabled
        {
            get { return !DateOuverture.HasValue; }
        }

        private DelegateCommand _locate;
        public DelegateCommand Locate
        {
            get { return _locate ?? (_locate = new DelegateCommand(LocateRecord, () => true)); }
        }

        private void LocateRecord()
        {
            
        }
    }
}