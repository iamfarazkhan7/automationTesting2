using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace TG.Test.WebApps.Common.Drivers
{
    public class ChromeWebDriver
    {
        public static IWebDriver LoadChromeDriver()
        {
            var driverService = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory);
            driverService.HideCommandPromptWindow = true;
            var options = new ChromeOptions();
            //"Forcepoint Endpoint for Windows" extension fix (for London office))
            //options.AddArgument("--disable-extensions");
            options.AddArgument("--kiosk");
            options.AddArgument("--disable-popup-blocking");
            return new ChromeDriver(driverService, options);
        }
    }
}
