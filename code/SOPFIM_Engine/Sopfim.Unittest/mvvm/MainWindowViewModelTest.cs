using System.Collections.Generic;
using ESRI.ArcGIS.Geodatabase;
using Moq;
using NUnit.Framework;
using Sopfim.CustomControls;
using SOPFIM.DataLayer;
using SOPFIM.Domain;
using Sopfim.ViewModels;

namespace Sopfim.Unittest.mvvm
{
    [TestFixture]
    public class MainWindowViewModelTest
    {
        //private Mock<ISopfimMainWindow> _mainWindow;
        private Mock<IMapControl> _mapControl;
        private Mock<IDataService> _dataService;
        //private Mock<IViewModelFactory<TestListViewModel, TestEntityViewModel>> _factory;
        private MainWindowViewModel<TestListViewModel, TestEntityViewModel> _viewModel;

        [SetUp]
        public void Setup()
        {
            _dataService = new Mock<IDataService>();
            _mapControl = new Mock<IMapControl>();
            _dataService.Setup(x => x.GeneralQuery<BlocTBE>(It.IsAny<ITable>(), string.Empty)).Returns(new List<BlocTBE>() {new BlocTBE() {NoBloc = "abc"}});
            _viewModel = new MainWindowViewModel<TestListViewModel, TestEntityViewModel>(_dataService.Object, _mapControl.Object);
        }

        [Test]
        public void Should_get_all_domain()
        {
            _dataService.Verify(x => x.GetDomain(DatabaseTableNames.TimingValues));
            _dataService.Verify(x => x.GetDomain(DatabaseTableNames.ProduitValues));
            _dataService.Verify(x => x.GetDomain(DatabaseTableNames.PerscriptionValues));
            _dataService.Verify(x => x.GetDomain(DatabaseTableNames.ApplicationValues));
            // ........ etc
        }

        [Test]
        public void InitializeModel_should_set_model_property()
        {
            Assert.IsNull(_viewModel.DataViewModel);
            _viewModel.InitializeDataModel();
            Assert.IsNotNull(_viewModel.DataViewModel);
        }

        [Test]
        public void InitializeModel_should_call_init_query()
        {
            _viewModel.InitializeDataModel();
            Assert.AreEqual(2, _viewModel.DataViewModel.Couter);
        }

    }
}