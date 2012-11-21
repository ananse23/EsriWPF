using System.Configuration;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Microsoft.Practices.Prism.Commands;
using SOPFIM.Domain;

namespace Sopfim.ViewModels
{
    public class PulverisationViewModel : SuiviPulverisation
    {
        private ITable _blockTable;
        public string AppPrevue { get; set; }
        public override string NoBloc
        {
            get
            {
                return base.NoBloc;
            }
            set
            {
                base.NoBloc = value;
                Locate.RaiseCanExecuteChanged();
            }
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
    }
}