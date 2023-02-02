using OpenQA.Selenium;
using TG.Test.WebApps.Common.Selenium;
using TG.Test.WebApps.Common.Selenium.Enums;

//[assembly: CollectionBehavior(MaxParallelThreads = 5)]
namespace TG.Test.WebApps.TMV3.Test.Hooks
{
    [Binding]
    public static class Hooks
    {
        [BeforeFeature]
        public static void InitializeBrowser(FeatureContext featureContext)
        {
            Driver Browser = new Driver();

            BrowserType selectedBrowser = BrowserType.CHROME;

            Browser.InitializeDriver(selectedBrowser);
            Browser.SetImplicitWait(3);

            featureContext.FeatureContainer.RegisterInstanceAs(Browser);
        }

        [AfterFeature]
        public static void CloseBrowser(FeatureContext featureContext)
        {
            Driver Browser = featureContext.FeatureContainer.Resolve<Driver>();
            //Browser.CloseDriver();
        }

        [AfterScenario]
        public static void Teardown(FeatureContext featureContext, ScenarioContext scenarioContext)
        {
            if (scenarioContext.ScenarioExecutionStatus == ScenarioExecutionStatus.TestError)
            {
                var driver = featureContext.FeatureContainer.Resolve<Driver>();

                string dateToday = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                string name = featureContext.FeatureInfo.Title + " " + scenarioContext.ScenarioInfo.Title;
                string fileName = dateToday + " " + name + ".png";

                Directory.CreateDirectory(Directory.GetCurrentDirectory() + "\\screenshots");

                var screenshot = ((ITakesScreenshot)driver.Browser).GetScreenshot();
                screenshot.SaveAsFile("screenshots\\" + fileName, ScreenshotImageFormat.Png);
            }
        }

        [AfterScenario]
        public static void DisposeNetworkInterception(FeatureContext featureContext)
        {
            Driver driver = featureContext.FeatureContainer.Resolve<Driver>();

            //if (driver.DevTools.HasActiveDevToolsSession)
            //{
            //    driver.DevTools.CloseDevToolsSession();
            //}
        }
    }
}
