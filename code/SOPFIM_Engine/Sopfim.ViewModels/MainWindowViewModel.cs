using System.Collections.Generic;
using System.Configuration;
using Esri.CommonUtils;
using Microsoft.Practices.Prism.ViewModel;
using SOPFIM.DataLayer;
using SOPFIM.Domain;
using Sopfim.CustomControls;
using System.Linq;

namespace Sopfim.ViewModels
{
    public class MainWindowViewModel<TModel, TEntity> : NotificationObject where TEntity : EditableEntity, new()
        where TModel : EditableListViewModel<TEntity>, new()
    {
        private readonly IDataService _service;
        private readonly IMapControl _mapService;
        public MainWindowViewModel(IDataService service, IMapControl mapService)
        {
            _service = service;
            _mapService = mapService;
            GetGeodatabaseDomains();
        }

        public virtual void InitializeDataModel()
        {
            DataViewModel = new TModel {DataService = _service, MapService = _mapService};
            DataViewModel.InitialQuery();
        }

        public MainWindowViewModel(string fileGeodatabase, IMapControl mapService) 
            : this(new DataService(fileGeodatabase), mapService)
        {
            
        }

        private TModel _dataViewModel;
        public TModel DataViewModel
        {
            get { return _dataViewModel; }
            set { _dataViewModel = value;
            RaisePropertyChanged("DataViewModel");
            }
        }

        public List<DomainRecord> TimingValues { get; set; }
        public List<DomainRecord> ProduitValues { get; set; }
        public List<DomainRecord> PerscriptionValues { get; set; }
        public List<DomainRecord> ApplicationValues { get; set; }
        public List<DomainRecord> InterApplicationValues { get; set; }
        public List<DomainRecord> PrioriteEtatValues { get; set; }
        public List<DomainRecord> BlocTypeValues { get; set; }
        public List<DomainRecord> BaseOperationValues { get; set; }
        public List<DomainRecord> BlocNumberValues { get; set; }
        public List<DomainRecord> LargeurValues { get; set; }
        public List<DomainRecord> RaisonValues { get; set; }
        public List<DomainRecord> BlockStateValues { get; set; }

        private void GetGeodatabaseDomains()
        {
            Logger.Log("retreiving lookup domains");
            this.TimingValues = _service.GetDomain(DatabaseTableNames.TimingValues);
            this.ProduitValues = _service.GetDomain(DatabaseTableNames.ProduitValues);
            this.PerscriptionValues = _service.GetDomain(DatabaseTableNames.PerscriptionValues);
            this.ApplicationValues = _service.GetDomain(DatabaseTableNames.ApplicationValues);
            this.InterApplicationValues = _service.GetDomain(DatabaseTableNames.InterApplicationValues);
            this.PrioriteEtatValues = _service.GetDomain(DatabaseTableNames.PrioriteEtatValues);
            this.BlocTypeValues = _service.GetDomain(DatabaseTableNames.BlocTypeValues);
            this.BaseOperationValues = _service.GetDomain(DatabaseTableNames.BaseOperationValues);
            this.LargeurValues = _service.GetDomain(DatabaseTableNames.LargeurValues);
            this.RaisonValues = _service.GetDomain(DatabaseTableNames.RaisonValues);
            this.BlockStateValues = _service.GetDomain(DatabaseTableNames.EtatBloc);
            GetBlocNumberValues();
        }

        private void GetBlocNumberValues()
        {
            var table = _service.GetTable(ConfigurationManager.AppSettings["BlocTableName"]);
            var blocs = _service.GeneralQuery<BlocTBE>(table, string.Empty);
            BlocNumberValues = blocs.Select(x => new DomainRecord {Code = x.NoBloc, Description = x.NoBloc}).Distinct().OrderBy(x => x.Code).ToList();
            BlocNumberValues.Insert(0, new DomainRecord() { Code = null, Description = string.Empty } );
        }
    }
}