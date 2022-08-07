using AventStack.ExtentReports;
using AventStack.ExtentReports.Gherkin.Model;
using BoDi;
using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumSpecflowXUnitTestProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Bindings;

namespace SeleniumSpecflowXUnitTestProject.Hooks
{
    [Binding]
    public sealed class MainHooks
    {
        //private static FeatureContext _featureContext;
        //private static ScenarioContext _scenarioContext;
        //private ScenarioStepContext _stepContext;
        private static ApplicationSettings _config;

        private static ExtentTest featureName;
        private static ExtentTest scenario;
        private static AventStack.ExtentReports.ExtentReports extentReport;

        // private readonly IObjectContainer _objectcontainer;
        public static ApplicationSettings Configuration => _config;

        //public MainHooks(FeatureContext featureContext ,ScenarioContext scenarioContext, ScenarioStepContext stepContext)
        //{
        //    _featureContext = featureContext;
        //    _scenarioContext = scenarioContext;
        //    _stepContext = stepContext;
        //}

        [BeforeTestRun]
        public static void BeforeAllTest()
        {
              var config = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
#if DEBUG
                .AddUserSecrets<ApplicationSettings>()
#else
                .AddJsonFile("appSettings.json")
#endif
                .Build();

           // var baseUrl = config["BaseUrl"]; Simplest way to get the config
            _config = config.Get<ApplicationSettings>();
        } 

        [BeforeTestRun]
        public static void InitializeExtentReport()
        {
            var htmlReporter = new AventStack.ExtentReports.Reporter.ExtentHtmlReporter(@"D:\Dev\Codes\cSharp\SeleniumSpecflowXUnitTestProject\Report\");
            htmlReporter.Config.Theme = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            //htmlReporter.Config.ReportName = "extentReport.html";
            // htmlReporter. = AventStack.ExtentReports.Reporter.Configuration.Theme.Dark;
            extentReport = new AventStack.ExtentReports.ExtentReports();
            extentReport.AnalysisStrategy = AnalysisStrategy.BDD;
            extentReport.AttachReporter(htmlReporter);
            
        }

        [AfterTestRun]
        public static void TeardownExtentReport()
        {
            extentReport.Flush();
        }

        [BeforeFeature]
        public static void BeforeFeature(FeatureContext featureContext)
        {
            featureName = extentReport.CreateTest<Feature>(featureContext.FeatureInfo.Title);
            
        }

        [BeforeScenario]
        public static void BefoerScenario(ScenarioContext scernarioContext)
        {
            scenario = featureName.CreateNode(new GherkinKeyword("Feature"),scernarioContext.ScenarioInfo.Title);
        }

        [BeforeScenario]
        public void InitializeBrowser(ScenarioContext scernarioContext)
        {
            DriverContext driverContext = new()
            { 
                Driver = new ChromeDriver()
            };
            driverContext.Wait = new WebDriverWait(driverContext.Driver, TimeSpan.FromSeconds(10));

            //driverContext.Driver.Navigate().GoToUrl(_config.BaseUrl);
            //Thread.Sleep(1000);
            scernarioContext.Set(driverContext);
        }

        [AfterScenario]
        public void AfterScenario(ScenarioContext scernarioContext)
        {
            var driverContext = scernarioContext.Get<DriverContext>();
            driverContext.Driver.Quit();
        }

        [AfterStep]
        public void InsertRepportingSteps(ScenarioContext scenarioContext)
        {
            var stepType = scenarioContext.StepContext.StepInfo.StepDefinitionType;

            if(scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.OK)
            {
                switch (stepType)

                {
                    case StepDefinitionType.Given:
                        scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case StepDefinitionType.When:
                        scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text);
                        break;
                    case StepDefinitionType.Then:
                        scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text);
                        break;
                    default:
                        break;
                }
            }
            else if(scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.Skipped)
            {
                switch (stepType)

                {
                    case StepDefinitionType.Given:
                        scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Skip("Test "+ScenarioExecutionStatus.Skipped.ToString());
                        break;
                    case StepDefinitionType.When:
                        scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Skip("Test " + ScenarioExecutionStatus.Skipped.ToString());
                        break;
                    case StepDefinitionType.Then:
                        scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Skip("Test " + ScenarioExecutionStatus.Skipped.ToString());
                        break;
                    default:
                        break;
                }

            }
            else
            {
                var takeScreeshot = scenarioContext.Get<DriverContext>().CaptureScreenshotAndReturnAsBase64();
                var screenshot = MediaEntityBuilder.CreateScreenCaptureFromBase64String(takeScreeshot, scenarioContext.ScenarioInfo.Title.Trim()).Build();
                switch (stepType)

                {
                    case StepDefinitionType.Given:
                        scenario.CreateNode<Given>(scenarioContext.StepContext.StepInfo.Text).Fail("Test " + scenarioContext.ScenarioExecutionStatus.ToString(), screenshot);
                        break;
                    case StepDefinitionType.When:
                        scenario.CreateNode<When>(scenarioContext.StepContext.StepInfo.Text).Fail("Test " + scenarioContext.ScenarioExecutionStatus.ToString(), screenshot);
                        break;
                    case StepDefinitionType.Then:
                        scenario.CreateNode<Then>(scenarioContext.StepContext.StepInfo.Text).Fail("Test " + scenarioContext.ScenarioExecutionStatus.ToString(), screenshot);
                        break;
                    default:
                        break;
                }
            }

            
        }
    }
}
