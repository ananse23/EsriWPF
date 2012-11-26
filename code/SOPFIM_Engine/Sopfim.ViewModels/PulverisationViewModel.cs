using System.Configuration;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;
using Microsoft.Practices.Prism.Commands;
using Sopfim.CustomControls;
using SOPFIM.DataLayer;
using SOPFIM.Domain;

namespace Sopfim.ViewModels
{
    public class PulverisationViewModel : SuiviPulverisation
    {
        private readonly IDataService _dataService;
        private readonly IMapControl _mapControl;
        public PulverisationViewModel(IDataService dataService, IMapControl mapControl)
        {
            _dataService = dataService;
            _mapControl = mapControl;
        }

        public PulverisationViewModel() : this(ApplicationSources.DataService, ApplicationSources.MapControl)
        {
            
        }

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
            _blockTable = _blockTable ?? _dataService.GetTable(ConfigurationManager.AppSettings["BlocTableName"]);
            var result = _dataService.GeneralQuery<BlocTBE>(_blockTable, string.Format("NoBloc = '{0}'", NoBloc));
            var envelopeTotal = new Envelope() as IEnvelope;
            result.ForEach(x =>
            {
                IEnvelope envelope = x.Shape.Envelope;
                envelopeTotal.Union(envelope);
            });
            _mapControl.ZoomToExtent(envelopeTotal);
        }
    }
}