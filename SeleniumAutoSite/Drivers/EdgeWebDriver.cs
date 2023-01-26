using OpenQA.Selenium;
using OpenQA.Selenium.Edge;

namespace TG.Test.WebApps.Common.Drivers
{
    public class EdgeWebDriver
    {
        public static IWebDriver LoadEdgeDriver()
        {
            var driverService = EdgeDriverService.CreateDefaultService(".");
            driverService.HideCommandPromptWindow = true;
            return new EdgeDriver(driverService);
        }
    }
}
