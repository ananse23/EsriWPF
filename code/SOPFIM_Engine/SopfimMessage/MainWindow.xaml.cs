using System;
using System.Configuration;
using System.Windows;
using Esri.CommonUtils;
using SOPFIM.Domain;
using Sopfim.CustomControls;
using Sopfim.ViewModels;
using SopfimMessage.ViewModel;
using SopfimMessage.reports;

namespace SopfimMessage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IMapControl _mapService;
        private MainWindowViewModel<MessageListViewModel, MessageViewModel> _model;
        public MainWindow()
        {
            InitializeComponent();
            _mapService = (IMapControl) _mapControl;
        }


        private void _mapControl_MapLoaded(object sender, EventArgs e)
        {
            try
            {
                GenerateExcelReportCommand[] reps = {new MessageReport()};
                _mapService.AddReportMenuItems(reps);
                var fileGeodatabase = ConfigurationManager.AppSettings["fileGeodatabase"];
                _model =
                    new MainWindowViewModel<MessageListViewModel, MessageViewModel>(
                        fileGeodatabase, _mapService);
                _model.InitializeDataModel();
                this.DataContext = _model;
                Logger.Log("Finish loading data: " + fileGeodatabase);
            }

            catch (Exception exception)
            {
                Logger.Error("error getting data", exception);
                Xceed.Wpf.Toolkit.MessageBox.Show("Error: " + exception.Message);
            }
        }

        private void _tabularData_CellEditEnding(object sender, System.Windows.Controls.DataGridCellEditEndingEventArgs e)
        {
            _model.DataViewModel.SetSelectedRecordAsDirty();
        }
    }
}
