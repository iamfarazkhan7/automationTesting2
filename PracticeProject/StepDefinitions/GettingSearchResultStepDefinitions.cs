
using NUnit.Framework;
using OpenQA.Selenium;
using TG.Test.WebApps.Common.Selenium;
using TG.Test.WebApps.Common.Selenium.Enums;

namespace FirstProject.StepDefinitions
{
    [Binding]
    public class GettingSearchResultStepDefinitions
    {
        
        Driver driverObject = new Driver();

        List<int> expectedCounts = new List<int>() { 15, 10, 10, 10, 10 };

        public string Url = "https://www.google.com/";

        [Given(@"User is on the homepage")]
        public void GivenUserIsOnTheHomepage()
        {
            driverObject.InitializeDriver(BrowserType.CHROME);
        }

        [When(@"User navigates to homepage of Google")]
        public void WhenUserNavigatesToHomepageOfGoogle()
        {

            driverObject.NavigateToUrl(Url);

        }

        [When(@"User typed automation Training in the search bar")]
        public void WhenUserTypedAutomationTrainingInTheSearchBar()
        {
            IWebElement element = driverObject.Browser.FindElement(By.Name("q"));

            element.SendKeys("automation Training");

            element.SendKeys(Keys.Enter);
        }




        [Then(@"The results counts are validated with expected counts")]
        public void ThenTheResultsCountsAreValidatedWithExpectedCounts()
        {
            int page = 1;

            while (page <= 4)
            {

                IList<IWebElement> all = driverObject.Browser.FindElements(By.TagName("h3"));

                var totalResults = all.Count();

                Assert.AreEqual(expectedCounts[page - 1], totalResults, "Count values are not equal");

                IWebElement element = driverObject.Browser.FindElement(By.LinkText("Next"));

                element.SendKeys(Keys.Enter);

                page += 1;
            }

            driverObject.CloseDriver();
        }


        [When(@"User enters (.*) in the search bar")]
        public void WhenUserTypedAutomationTrainingInTheSearchBar(String searchValue)
        {
            IWebElement element = driverObject.Browser.FindElement(By.Name("q"));

            element.SendKeys(searchValue);

            element.SendKeys(Keys.Enter);

        }

        [When(@"The result counts are validated with these (.*)")]
        public void WhenTheResultCountsAreValidatedWithThese(String counts)
        {

            List<string> result = counts.Split(',').ToList();

            List<int> expectedCount = result.Select(int.Parse).ToList();

            int page = 1;

            while (page <= 4)
            {

                IList<IWebElement> all = driverObject.Browser.FindElements(By.TagName("h3"));
                    
                var totalResults = all.Count();

                Assert.AreEqual(expectedCount[page - 1], totalResults, "Count values are not equal");

                IWebElement element = driverObject.Browser.FindElement(By.LinkText("Next"));

                element.SendKeys(Keys.Enter);

                page += 1;
            }

            driverObject.CloseDriver();
        }






    }
}
