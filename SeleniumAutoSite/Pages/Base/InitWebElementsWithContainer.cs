using OpenQA.Selenium;
using TG.Test.WebApps.Common.Extensions;

namespace TG.Test.WebApps.Common.Pages.Base
{
    public class InitWebElementsWithContainer : InitWebElements
    {
        public IWebElement Container { get; private set; }

        public bool Displayed => Container.IsElementDisplayed();

        public InitWebElementsWithContainer(IWebElement container) : this(container, container)
        {

        }

        public InitWebElementsWithContainer(ISearchContext searchContext, IWebElement container) : base(searchContext)
        {
            Container = container;
        }

        protected void InitElements(IWebElement container)
        {
            base.InitElements(container);
            Container = container;
        }
    }
}
