using OpenQA.Selenium;
using TG.Test.WebApps.Common.Selenium;

namespace TG.Test.WebApps.Common.Pages.Base
{
    public class InitWebElementsWithDriver : InitWebElements
    {
        public Driver Driver { get; set; }
        public IWebDriver WebDriver => Driver.Browser;

        public InitWebElementsWithDriver(ISearchContext searchContext, Driver driver) : base(searchContext)
        {
            Driver = driver;
        }
    }
}
