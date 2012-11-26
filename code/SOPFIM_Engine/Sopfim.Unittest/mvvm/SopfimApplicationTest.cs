using System;
using Moq;
using NUnit.Framework;
using Sopfim.CustomControls;
using SOPFIM.DataLayer;
using Sopfim.ViewModels;

namespace Sopfim.Unittest.mvvm
{
    [TestFixture]
    public class SopfimApplicationTest
    {
        private SopfimApplication<TestListViewModel, TestEntityViewModel> _application;
        private Mock<ISopfimMainWindow> _mainWindow;
        private Mock<IMapControl> _mapControl;
        private Mock<IDataService> _dataService;
        private Mock<IViewModelFactory<TestListViewModel, TestEntityViewModel>> _factory;
        private Mock<IMainWindowViewModel<TestListViewModel, TestEntityViewModel>> _viewModel;
        [SetUp]
        public void Setup()
        {
            _mainWindow = new Mock<ISopfimMainWindow>();
            _mapControl = new Mock<IMapControl>();
            _dataService = new Mock<IDataService>();
            _factory = new Mock<IViewModelFactory<TestListViewModel, TestEntityViewModel>>();
            _viewModel = new Mock<IMainWindowViewModel<TestListViewModel, TestEntityViewModel>>();
            _mainWindow.SetupGet(x => x.MapControl).Returns(_mapControl.Object);
            _factory.Setup(x => x.CreateViewModel(_dataService.Object, _mapControl.Object)).Returns(_viewModel.Object);
            _application = new SopfimApplication<TestListViewModel, TestEntityViewModel>(_mainWindow.Object,
                                                                                         _dataService.Object, _factory.Object);
        }

        [Test]
        public void Initialize_should_show_the_window()
        {
            _application.Initialize();
            _mainWindow.Verify(x => x.Show());
        }

        [Test]
        public void MapLoadedEvent_should_create_view_model()
        {
            _mapControl.Raise(x => x.MapLoaded += null, EventArgs.Empty);
            _factory.Verify(x => x.CreateViewModel(_dataService.Object, _mapControl.Object));
        }

        [Test]
        public void MapLoadedEvent_should_assign_window_context_to_viewModel()
        {
            _mapControl.Raise(x => x.MapLoaded += null, EventArgs.Empty);
            _mainWindow.VerifySet(x => x.DataContext = _viewModel.Object);
        }


    }
}