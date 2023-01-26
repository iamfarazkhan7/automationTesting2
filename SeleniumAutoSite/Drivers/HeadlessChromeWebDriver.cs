using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace TG.Test.WebApps.Common.Drivers
{
    public class HeadlessChromeWebDriver
    {
        public static IWebDriver LoadHeadlessChromeDriver()
        {
            var driverService = ChromeDriverService.CreateDefaultService(Environment.CurrentDirectory);
            driverService.HideCommandPromptWindow = true;
            var options = new ChromeOptions();
            options.AddArgument("--headless");
            //options.AddArgument("--disable-gpu");
            return new ChromeDriver(driverService, options);
        }
    }
}
