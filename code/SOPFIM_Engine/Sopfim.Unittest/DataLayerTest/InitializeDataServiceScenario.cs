using System.Collections.Generic;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using Moq;
using NBehave.Spec.NUnit;
using ORMapping;
using SOPFIM.DataLayer;
using SOPFIM.Domain;

namespace Sopfim.Unittest.DataLayerTest
{
    
    public abstract class InitializeDataServiceScenario : ScenarioDrivenSpecBase 
    {
        protected IDataService DataService;
        protected Mock<FileGDBWorkspaceFactory> WorkspaceFactory;
        protected Mock<IFeatureWorkspace> Workspace;
        protected Mock<ITable> Table;
        protected List<SuiviMessage> Data;
        public virtual void Given_file_geodatabase_exist()
        {
            WorkspaceFactory = new Mock<FileGDBWorkspaceFactory>();
            Workspace = new Mock<IFeatureWorkspace>();
            Table = new Mock<ITable>();
            Data = new List<SuiviMessage>()
                       {
                           new SuiviMessage() {IsDirty = true}
                       };
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
            DataService.GetTable("anytable");
        }

        public void When_call_query()
        {
            Table.Setup(x => x.Map<SuiviMessage>(It.Is<IQueryFilter>(q => q.WhereClause == "where")))
                .Returns(Data);
            DataService.GeneralQuery<SuiviMessage>(Table.Object, "where");
        }

        public void Then_should_delegate_query_to_table()
        {
            Table.VerifyAll();
        }

        public void Then_should_delegate_to_workspace()
        {
            WorkspaceFactory.VerifyAll();
            Workspace.VerifyAll();
        }
    }
}