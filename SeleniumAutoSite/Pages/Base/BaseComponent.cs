using OpenQA.Selenium;
using TG.Test.WebApps.Common.Extensions;
using TG.Test.WebApps.Common.Selenium;

namespace TG.Test.WebApps.Common.Pages.Base
{
    public class BaseComponent : InitWebElementsWithContainer
    {
        public Driver Driver { get; set; }
        public IWebDriver WebDriver => Driver.Browser;

        public BaseComponent(Driver driver) : this(driver.Browser, null, driver)
        {

        }

        public BaseComponent(IWebElement container, Driver driver) : this(container, container, driver)
        {

        }

        public BaseComponent(ISearchContext searchContext, IWebElement container, Driver driver) : base(searchContext, container)
        {
            Driver = driver;
        }

        public BaseComponent(By container, Driver driver) : this(GetContainer(driver, container), driver)
        {

        }

        public void ScrollIntoContainer()
        {
            WebDriver.ScrollIntoElement(Container);
        }

        protected static IWebElement GetContainer(Driver driver, By container)
        {
            try
            {
                return driver.Browser.FindElement(container);
            }
            catch (System.Exception)
            {
                return null;
            }
        }
    }
}
