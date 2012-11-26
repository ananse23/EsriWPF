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
    public partial class MainWindow : Window, ISopfimMainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void _tabularData_InitializingNewItem(object sender, System.Windows.Controls.InitializingNewItemEventArgs e)
        {
            ((MainWindowViewModel<PulverisationListViewModel, PulverisationViewModel>) this.DataContext).DataViewModel.DataList.Add((PulverisationViewModel)e.NewItem);
        }

        public IMapControl MapControl
        {
            get { return (IMapControl) _mapControl; }
        }
    }
}
