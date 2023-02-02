using PracticeProject.Pages;
using System;
using TechTalk.SpecFlow;
using TG.Test.WebApps.Common.Selenium;
using TG.Test.WebApps.Common.Selenium.Enums;
using TG.Test.WebApps.TMV3.Test.Hooks;

namespace PracticeProject.StepDefinitions
{
    [Binding]
    public class NavigatingToThePageStepDefinitions
    {
        

        private Driver Driver { get; }

        

        public String url = "https://www.booking.com/";
        public NavigatingToThePageStepDefinitions(Driver driver)
        {
            Driver= driver;
            
            
        }
        

        [Given(@"I navigate to the booking\.com")]
        public void GivenINavigateToTheBooking_Com()
        {
            
            Driver.NavigateToUrl(url);
            
        }
    }
}
