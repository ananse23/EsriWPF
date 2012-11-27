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
    public class PulverisationViewModelTest
    {
        private PulverisationViewModel _viewModel;
        private Mock<IDataService> _dataService;
        private Mock<IMapControl> _mapControl;
        private Mock<ITable> _blocTable;

        [SetUp]
        public void Setup()
        {
            _mapControl = new Mock<IMapControl>();
            _dataService = new Mock<IDataService>();
            var d = new List<BlocTBE>();
            //_viewModel = new PulverisationViewModel(_dataService.Object, _mapControl.Object);
            _viewModel = new PulverisationViewModel(_dataService.Object, _mapControl.Object, d);
            _blocTable = new Mock<ITable>();
            _dataService.Setup(x => x.GetTable("BlocTBE")).Returns(_blocTable.Object);
        }

        [Test]
        public void Locate_cannot_execute_when_Block_number_is_null()
        {
            Assert.IsFalse(_viewModel.Locate.CanExecute());
        }

        [Test]
        public void Locate_can_execute_when_Block_number_is_not_null()
        {
            _viewModel.NoBloc = "123";
            Assert.IsTrue(_viewModel.Locate.CanExecute());
        }

        [Test]
        public void Locate_should_delegate_to_dataSevice()
        {
            _viewModel.NoBloc = "123";
            _viewModel.Locate.Execute();
            _dataService.Verify(x => x.GeneralQuery<BlocTBE>(_blocTable.Object, "NoBloc = '123'"));
        }

    }
}