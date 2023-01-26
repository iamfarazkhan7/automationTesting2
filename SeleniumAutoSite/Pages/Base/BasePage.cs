using TG.Test.WebApps.Common.Selenium;

namespace TG.Test.WebApps.Common.Pages.Base
{
    public class BasePage : InitWebElementsWithDriver
    {
        public BasePage() : base(null, null)
        {

        }

        public BasePage(Driver driver) : base(driver.Browser, driver)
        {

        }
    }
}
