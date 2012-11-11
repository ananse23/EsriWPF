using NBehave.Spec.Extensions;
using NUnit.Framework;
using NFeature = NBehave.Narrator.Framework.Feature;


namespace Sopfim.Unittest.Domain
{
    [TestFixture]
    public class MessageViewModelTest : MessageViewModelScenario
    {
        [Test]
        public void dateOuverture_should_update_datePrevision()
        {
            var stepHelper = this;
            Feature.AddScenario()
                .WithHelperObject(stepHelper)
                .Given("DatePrevision has value")
                .When("set DateOuverture")
                .Then("should set DatePrevision to null");
        }

        [Test]
        public void dateOuverture_should_not_update_datePrevision_when_Ouverture_is_null()
        {
            var stepHelper = this;
            Feature.AddScenario()
                .WithHelperObject(stepHelper)
                .Given("DatePrevision has value")
                .When("set DateOuverture to null")
                .Then("should not set DatePrevision to null");
        }

        [Test]
        public void DatePrevisionIsEnabled_should_be_related_to_DateOuverture()
        {
            var stepHelper = this;
            Feature.AddScenario()
                .WithHelperObject(stepHelper)
                .Given("DatePrevision has value")
                .When("set DateOuverture")
                .Then("DatePrevisionIsEnabled should be false");
            Feature.AddScenario()
                .WithHelperObject(stepHelper)
                .Given("DatePrevision has value")
                .When("set DateOuverture to null")
                .Then("DatePrevisionIsEnabled should be true");
        }

        

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