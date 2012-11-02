using System;
using System.Configuration;
using System.Windows;
using SOPFIM.Domain;
using Sopfim.CustomControls;
using Sopfim.ViewModels;
using SopfimMessage.ViewModel;
using log4net;

namespace SopfimMessage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMapControl _mapService;
        public MainWindow()
        {
            InitializeComponent();
            _mapService = (IMapControl) _mapControl;
        }

        private void _mapControl_MapLoaded(object sender, System.EventArgs e)
        {
            try
            {
                
                Logger.Log(typeof(MainWindow), "map was successfuly loaded");
                var fileGeodatabase = ConfigurationManager.AppSettings["fileGeodatabase"];
                Logger.Log(typeof(MainWindow), "Loading data...: " + fileGeodatabase);
                var model =
                        new MainWindowViewModel<MessageEntityViewModel, Message>(
                            fileGeodatabase, _mapService);
                model.InitializeDataModel();
                this.DataContext = model;
                LogManager.GetLogger(typeof(MainWindow)).Info("Finish loaded data: " + fileGeodatabase);
            }
            
            catch (Exception exception)
            {
                Logger.Error("error getting data", exception);
                Xceed.Wpf.Toolkit.MessageBox.Show("Error: " + exception.Message);
            }
        }
    }
}
