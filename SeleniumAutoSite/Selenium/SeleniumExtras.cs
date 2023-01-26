using OpenQA.Selenium;
using TG.Test.WebApps.Common.Extensions;

namespace TG.Test.WebApps.Common.Selenium
{
    public static class SeleniumExtras
    {
        public static void FillForm(this IWebElement element, IWebDriver driver, string content)
        {
            if (false) //!DriverExtensions.IsMobileTestingEnabled()
            {
                element.Clear();
                element.SendKeys(content);
            }
            else
            {
                element.SendKeys(content);
            }
        }
    }
}
