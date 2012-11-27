using System.Collections.Generic;
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
        private readonly List<BlocTBE> _blocs;
        
        public PulverisationViewModel(IDataService dataService, IMapControl mapControl, List<BlocTBE> blocks)
        {
            _dataService = dataService;
            _mapControl = mapControl;
            _blocs = blocks;
        }

        public PulverisationViewModel() : this(ApplicationSources.DataService, ApplicationSources.MapControl, ApplicationSources.Blocks)
        {
            
        }

        private ITable _blockTable;

        private string _appPrevue;
        public string AppPrevue
        {
            get { return _appPrevue; }
            set { _appPrevue = value;
            RaisePropertyChanged("AppPrevue");
            }
        }

        public override string NoBloc
        {
            get
            {
                return base.NoBloc;
            }
            set
            {
                if (base.NoBloc == value)
                    return;
                base.NoBloc = value;
                Locate.RaiseCanExecuteChanged();
                AppPrevue = _blocs.Find(x => x.NoBloc == base.NoBloc).AppPrevue;
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