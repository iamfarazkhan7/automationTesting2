using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;

namespace TG.Test.WebApps.Common.Drivers
{
    public class HeadlessFirefoxWebDriver
    {
        public static IWebDriver LoadHeadlessFirefoxDriver()
        {
            var driverService = FirefoxDriverService.CreateDefaultService(Environment.CurrentDirectory);
            driverService.HideCommandPromptWindow = true;
            driverService.Host = "::1";
            var options = new FirefoxOptions();
            options.AddArgument("--headless");
            options.AddArgument("--disable-extensions");
            options.AcceptInsecureCertificates = true;
            return new FirefoxDriver(driverService, options);
        }
    }
}
