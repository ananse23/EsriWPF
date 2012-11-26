using System;
using System.Configuration;
using Esri.CommonUtils;
using log4net;
using SOPFIM.DataLayer;
using SOPFIM.Domain;

namespace Sopfim.ViewModels
{
    public class SopfimApplication<TList, TEntity>
        where TEntity : EditableEntity, new()
        where TList : EditableListViewModel<TEntity>, new()
    {
        protected ISopfimMainWindow SopfimMainWindow;
        private IViewModelFactory<TList, TEntity> _factory;

        public SopfimApplication(ISopfimMainWindow mainWindow, IDataService dataService, IViewModelFactory<TList, TEntity> factory)
        {
            SopfimMainWindow = mainWindow;
            SopfimMainWindow.MapControl.MapLoaded += MapLoaded;
            ApplicationSources.DataService = dataService;
            ApplicationSources.MapControl = SopfimMainWindow.MapControl;
            _factory = factory;
        }

        public SopfimApplication(ISopfimMainWindow mainWindow)
            : this(mainWindow, new DataService(ConfigurationManager.AppSettings["fileGeodatabase"]), new ViewModelFactory<TList, TEntity>())
        {
            
        }



        void MapLoaded(object sender, EventArgs e)
        {
            try
            {
//                IBaseExcelExportCommand<EditableEntity>[] reports = { new SuiviMessageExporter(), new PulverisationExporter() };
//                _mapService.AddReportMenuItems<EditableEntity>(reports);
                Logger.Log("map was successfully loaded ");
                Logger.Log("Loading data...: " + ConfigurationManager.AppSettings["fileGeodatabase"]);
                var model = _factory.CreateViewModel(ApplicationSources.DataService, ApplicationSources.MapControl);
                model.InitializeDataModel();
                SopfimMainWindow.DataContext = model;
                LogManager.GetLogger("SopfimApplication").Info("Finish loaded data: " + ConfigurationManager.AppSettings["fileGeodatabase"]);
            }
            catch (Exception exception)
            {
                Logger.Error("error getting data", exception);
                Xceed.Wpf.Toolkit.MessageBox.Show("Error loading data. Please see the log for details");
            }
        }

        public void Initialize()
        {
           SopfimMainWindow.Show();
        }
    }
}