using OpenQA.Selenium;
using SeleniumExtras.PageObjects;

namespace TG.Test.WebApps.Common.Pages.Base
{
    public class InitWebElements
    {
        public InitWebElements(ISearchContext context)
        {
            if (context == null)
            {
                return;
            }

            InitElements(context);
        }

        protected void InitElements(ISearchContext context)
        {
            PageFactory.InitElements(context, this);
        }
    }
}
