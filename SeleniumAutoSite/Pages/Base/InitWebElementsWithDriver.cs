using OpenQA.Selenium;
using TG.Test.WebApps.Common.Selenium;
using SeleniumExtras.PageObjects;
using System.Linq;
using TechTalk.SpecFlow;
using TG.Test.WebApps.Common.Extensions;
using TG.Test.WebApps.Common.Pages.Base;



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
