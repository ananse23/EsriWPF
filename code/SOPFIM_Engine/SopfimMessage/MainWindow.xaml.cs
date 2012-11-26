using System.Windows;
using Sopfim.CustomControls;
using Sopfim.ViewModels;
using SopfimMessage.ViewModel;


namespace SopfimMessage
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, ISopfimMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void _tabularData_CellEditEnding(object sender, System.Windows.Controls.DataGridCellEditEndingEventArgs e)
        {
            ((MainWindowViewModel<MessageListViewModel, MessageViewModel>) this.DataContext).DataViewModel.SetSelectedRecordAsDirty();
        }

        public IMapControl MapControl
        {
            get { return (IMapControl) _mapControl; }
        }
    }
}
