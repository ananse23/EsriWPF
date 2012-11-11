using NBehave.Spec.Extensions;
using NUnit.Framework;
using NFeature = NBehave.Narrator.Framework.Feature;

namespace Sopfim.Unittest.DataLayerTest
{
    [TestFixture]
    public class DataServiceTest : InitializeDataServiceScenario
    {

        [Test]
        public void Should_initialize_dataservice_when_passing_existing_file()
        {
            var stepHelper = this;
            Feature.AddScenario()
                .WithHelperObject(stepHelper)
                .Given("file geodatabase exist")
                .And("passing the file name to data service")
                .When("initializing the data service")
                .Then("should delegate to workspace");
        }

        [Test]
        public void Calling_GetTable_should_delegate_to_workspace()
        {
            var stepHelper = this;
            Feature.AddScenario()
                .WithHelperObject(stepHelper)
                .Given("data service initialized correctly")
                .When("call get table")
                .Then("should delegate to workspace");
        }

        // Cannot do it because ORMapping is not testable
        /*[Test]
        public void Calling_Query_should_delegate_to_table()
        {
            var stepHelper = this;
            Feature.AddScenario()
                .WithHelperObject(stepHelper)
                .Given("file geodatabase exist")
                .When("call query")
                .Then("should delegate query to table");
        }*/

        protected override NFeature CreateFeature()
        {
            return new NFeature("initialize the dataservice")
                .AddStory()
                .AsA("user")
                .IWant("initialize data service with existing file name")
                .SoThat("the data service initialized successfully");
        }
    }
}