using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using Moq;
using NBehave.Spec.NUnit;
using SOPFIM.DataLayer;

namespace Sopfim.Unittest.DataLayerTest
{
    
    public abstract class InitializeDataServiceScenario : ScenarioDrivenSpecBase 
    {
        protected IDataService DataService;
        protected Mock<FileGDBWorkspaceFactory> WorkspaceFactory;
        protected Mock<IFeatureWorkspace> Workspace;
        protected Mock<ITable> Table;

        public virtual void Given_file_geodatabase_exist()
        {
            WorkspaceFactory = new Mock<FileGDBWorkspaceFactory>();
            Workspace = new Mock<IFeatureWorkspace>();
            Table = new Mock<ITable>();
        }

        public void Given_data_service_initialized_correctly()
        {
            Given_file_geodatabase_exist();
            And_passing_the_file_name_to_data_service();
            When_initializing_the_data_service();
        }


        public virtual void And_passing_the_file_name_to_data_service()
        {
            WorkspaceFactory.Setup(x => x.OpenFromFile("fgdb", 0)).Returns(Workspace.As<IWorkspace>().Object);
        }

        
        public virtual void When_initializing_the_data_service()
        {
            DataService = new DataService(WorkspaceFactory.Object, "fgdb");
        }

        

        public void When_call_get_table()
        {
            Workspace.Setup(x => x.OpenTable("anytable")).Returns(Table.Object);
            
        }

        public void Then_the_table_should_be_not_null()
        {
            var table = DataService.GetTable("anytable");
            table.ShouldNotBeNull();
        }

        public void Then_the_data_service_initialized_successfully()
        {
            DataService.ShouldNotBeNull();
        }
        
    }
}