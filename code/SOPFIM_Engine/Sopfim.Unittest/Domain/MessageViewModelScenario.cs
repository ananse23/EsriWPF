using System;
using NBehave.Spec.NUnit;
using Sopfim.ViewModels;

namespace Sopfim.Unittest.Domain
{
    public abstract class MessageViewModelScenario : ScenarioDrivenSpecBase
    {
        protected MessageViewModel Message;
        
        
        public void Given_DatePrevision_has_value()
        {
            Message = new MessageViewModel() {DatePrevision = DateTime.Now};
        }

        public void When_set_DateOuverture()
        {
            Message.DateOuverture = DateTime.Now;
        }

        public void When_set_DateOuverture_to_null()
        {
            Message.DateOuverture = null;
        }

        public void Then_should_set_DatePrevision_to_null()
        {
            Message.DatePrevision.ShouldBeNull();
        }

        public void Then_should_not_set_DatePrevision_to_null()
        {
            Message.DatePrevision.ShouldNotBeNull();
        }

        public void Then_DatePrevisionIsEnabled_should_be_false()
        {
            Message.DatePrevisionIsEnabled.ShouldBeFalse();
        }
    }
}