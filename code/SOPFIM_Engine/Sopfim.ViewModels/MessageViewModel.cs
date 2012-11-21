using System;
using System.Configuration;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Microsoft.Practices.Prism.Commands;
using ORMapping;
using SOPFIM.Domain;

namespace Sopfim.ViewModels
{
    public class MessageViewModel : SuiviMessage, ICloneable
    {
        private ITable _blockTable;

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
            get
            {
                return _locate ?? (_locate = new DelegateCommand(LocateBlock,
                () => !string.IsNullOrEmpty(NoBloc)));
            }
        }

        private void LocateBlock()
        {
            _blockTable = _blockTable ?? DataSourceHelper.DataService.GetTable(ConfigurationManager.AppSettings["BlocTableName"]);
            var result = DataSourceHelper.DataService.GeneralQuery<BlocTBE>(_blockTable, string.Format("NoBloc = '{0}'", NoBloc));
            var envelopeTotal = new Envelope() as IEnvelope;
            result.ForEach(x =>
            {
                IEnvelope envelope = x.Shape.Envelope;
                envelopeTotal.Union(envelope);
            });
            DataSourceHelper.MapControl.ZoomToExtent(envelopeTotal);
        }

        public object Clone()
        {
            var message = new MessageViewModel
            {
                AppPrevue = this.AppPrevue,
                Application = this.Application,
                DateMessages = this.DateMessages,
                DateOuverture = this.DateOuverture,
                DatePrevision = this.DatePrevision,
                MessagesID = this.MessagesID,
                NomBase = this.NomBase,
                NoBloc = this.NoBloc,
                TypeBloc = this.TypeBloc,
                TimingIDI = this.TimingIDI,
                Produit = this.Produit,
                LarvesBr = this.LarvesBr,
                Prescription = this.Prescription,
                InterApp = this.InterApp,
                PrioriteEtat = this.PrioriteEtat,
                Ouvert_1jr = this.Ouvert_1jr,
                NbreLv30m = this.NbreLv30m,
                LvOuv30m = this.LvOuv30m,
                LvOuv80m = this.LvOuv80m,
                LvOuv100m = this.LvOuv100m,
                DateTr = this.DateTr,
                LvTr = this.LvTr,
                EtatBloc = this.EtatBloc,
                Remarque = this.Remarque
            };
            return message;
        }
    }
}