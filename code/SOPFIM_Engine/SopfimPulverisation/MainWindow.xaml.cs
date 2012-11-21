using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows;
using Esri.CommonUtils;
using SOPFIM.Domain;
using Sopfim.CustomControls;
using Sopfim.Reports;
using Sopfim.ViewModels;
using SopfimPulverisation.ViewModels;
using log4net;

namespace SopfimPulverisation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMapControl _mapService;
        private MainWindowViewModel<PulverisationListViewModel, PulverisationViewModel> _model;
        public MainWindow()
        {
            InitializeComponent();
            _mapService = (IMapControl)_mapControl;
        }

        private void _mapControl_MapLoaded(object sender, System.EventArgs e)
        {
            try
            {
                IBaseExcelExportCommand<EditableEntity>[] reports = {new SuiviMessageExporter(), new PulverisationExporter()};
                _mapService.AddReportMenuItems<EditableEntity>(reports);
                Logger.Log("map was successfully loaded ");
                Logger.Log("Loading data...: " + ConfigurationManager.AppSettings["fileGeodatabase"]);
                _model =
                        new MainWindowViewModel<PulverisationListViewModel, PulverisationViewModel>(
                            ConfigurationManager.AppSettings["fileGeodatabase"], _mapService);
                _model.InitializeDataModel();
                this.DataContext = _model;
                LogManager.GetLogger(typeof(MainWindow)).Info("Finish loaded data: " + ConfigurationManager.AppSettings["fileGeodatabase"]);
            }
            catch (Exception exception)
            {
                LogManager.GetLogger("ErrorLogger").Fatal("error getting data", exception);
                Xceed.Wpf.Toolkit.MessageBox.Show("Error loading data. Please see the log for details");
            }
        }

        private void _tabularData_InitializingNewItem(object sender, System.Windows.Controls.InitializingNewItemEventArgs e)
        {
            _model.DataViewModel.DataList.Add((PulverisationViewModel)e.NewItem);
        }
    }
}
