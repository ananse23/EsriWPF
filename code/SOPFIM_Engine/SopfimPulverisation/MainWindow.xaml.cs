using System;
using System.Configuration;
using System.Windows;
using SOPFIM.Domain;
using Sopfim.CustomControls;
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
        private IMapControl _mapService;
        public MainWindow()
        {
            InitializeComponent();
            _mapService = (IMapControl)_mapControl;
        }

        private void _mapControl_MapLoaded(object sender, System.EventArgs e)
        {
            try
            {
                LogManager.GetLogger(typeof(MainWindow)).Info("map was successfully loaded ");
                LogManager.GetLogger(typeof(MainWindow)).Info("Loading data...: " + ConfigurationManager.AppSettings["fileGeodatabase"]);
                var model =
                        new MainWindowViewModel<PulverisationEntityViewModel, SuiviPulverisation>(
                            ConfigurationManager.AppSettings["fileGeodatabase"], _mapService);
                model.InitializeDataModel();
                this.DataContext = model;
                LogManager.GetLogger(typeof(MainWindow)).Info("Finish loaded data: " + ConfigurationManager.AppSettings["fileGeodatabase"]);
            }
            catch (Exception exception)
            {
                LogManager.GetLogger("ErrorLogger").Fatal("error getting data", exception);
                Xceed.Wpf.Toolkit.MessageBox.Show("Error loading data. Please see the log for details");
            }
        }
    }
}
