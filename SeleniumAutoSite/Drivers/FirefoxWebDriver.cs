using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace TG.Test.WebApps.Common.Drivers
{
    public class FirefoxWebDriver
    {
        public static IWebDriver LoadFirefoxDriver()
        {
            var driverService = FirefoxDriverService.CreateDefaultService();
            driverService.Host = "::1";
            driverService.HideCommandPromptWindow = true;
            var options = new FirefoxOptions();
            options.AddArgument("--disable-extensions");
            options.AcceptInsecureCertificates = true;
            return new FirefoxDriver(driverService, options);
        }
    }
}
