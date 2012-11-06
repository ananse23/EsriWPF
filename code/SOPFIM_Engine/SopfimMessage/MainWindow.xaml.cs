using System;
using System.Configuration;
using System.Windows;
using Esri.CommonUtils;
using SOPFIM.Domain;
using Sopfim.CustomControls;
using Sopfim.ViewModels;
using SopfimMessage.ViewModel;

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

        private void _mapControl_MapLoaded(object sender, EventArgs e)
        {
            try
            {
                var fileGeodatabase = ConfigurationManager.AppSettings["fileGeodatabase"];
                var model =
                        new MainWindowViewModel<MessageEntityViewModel, Message>(
                            fileGeodatabase, _mapService);
                model.InitializeDataModel();
                this.DataContext = model;
                Logger.Log("Finish loading data: " + fileGeodatabase);
            }
            
            catch (Exception exception)
            {
                Logger.Error("error getting data", exception);
                Xceed.Wpf.Toolkit.MessageBox.Show("Error: " + exception.Message);
            }
        }
    }
}
