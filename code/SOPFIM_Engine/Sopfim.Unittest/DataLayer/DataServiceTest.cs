using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using Moq;
using NUnit.Framework;
using SOPFIM.DataLayer;

namespace Sopfim.Unittest.DataLayer
{
    [TestFixture]
    public class DataServiceTest
    {
        private IDataService _dataService;
        private Mock<FileGDBWorkspaceFactory> _workspaceFactory;
        private Mock<IFeatureWorkspace> _workspace;

        [SetUp]
        public void Setup()
        {
            _workspaceFactory = new Mock<FileGDBWorkspaceFactory>();
            _workspace = new Mock<IFeatureWorkspace>();
            _workspaceFactory.Setup(x => x.OpenFromFile("fgdb", 0)).Returns((IWorkspace) _workspace.Object);
            _dataService = new DataService(_workspaceFactory.Object, "fgdb");
        }

        [Test]
        public void GetTable_Should_Get_Table()
        {
            _dataService.GetTable("any_table");
            
        }
    }
}