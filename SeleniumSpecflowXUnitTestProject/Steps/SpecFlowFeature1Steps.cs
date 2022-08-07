using System;
using TechTalk.SpecFlow;
using Xunit;

namespace SeleniumSpecflowXUnitTestProject.Steps
{
    [Binding]
    public class SpecFlowFeature1Steps
    {
        private ScenarioContext _scenarioContext;
        public SpecFlowFeature1Steps(ScenarioContext scenarioContext)
        {
            _scenarioContext = scenarioContext;
        }

        [Given(@"the first number is (.*)")]
        public void GivenTheFirstNumberIs(int p0)
        {
            _scenarioContext.Set<int>(p0,"a");
        }
        
        [Given(@"the second number is (.*)")]
        public void GivenTheSecondNumberIs(int p0)
        {
            _scenarioContext.Set<int>(p0, "b");
        }
        
        [When(@"the two numbers are added")]
        public void WhenTheTwoNumbersAreAdded()
        {
            var a = _scenarioContext.Get<int>("a");
            var b = _scenarioContext.Get<int>("b");
            var c = a + b;

            _scenarioContext.Set<int>(c, "c");
        }
        
        [Then(@"the result should be (.*)")]
        public void ThenTheResultShouldBe(int p0)
        {
            var actVal = _scenarioContext.Get<int>("c");

            Assert.Equal(p0,actVal);
        }
    }
}
