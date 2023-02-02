

using DocumentFormat.OpenXml.Bibliography;
using NUnit.Framework.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System.Collections.ObjectModel;
using TG.Test.WebApps.Common.Pages.Base;
using TG.Test.WebApps.Common.Selenium;


namespace PracticeProject.Pages
{
    public class BookingInputs : BasePage
    {
        int resultstar, resultbrek;
        public FeatureContext FeatureContext { get; set; }

        public Driver Driver { get; set; }

        

        public BookingInputs(Driver driver, FeatureContext featureContext) : base(driver)
        {
            Driver= driver;
            FeatureContext = featureContext;
        }



        [FindsBy(How = How.Name, Using = "ss")]
        IWebElement Destination { get; set; }

        [FindsBy(How = How.ClassName, Using = "xp__dates-inner")]
        IWebElement DateField { get; set; }

        [FindsBy(How = How.ClassName, Using = "sb-searchbox__button")]
        IWebElement SearchButton { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#frm > div.xp__fieldset.js--sb-fieldset.accommodation > div.xp__dates.xp__group > div.xp-calendar > div > div > div.bui-calendar__content > div:nth-child(1) > table > tbody > tr:nth-child(4) > td:nth-child(5) > span")]
        IWebElement CheckIn { get; set; }

        [FindsBy(How = How.CssSelector, Using = ".bui-calendar__wrapper:nth-child(1) .bui-calendar__row:nth-child(4) > .bui-calendar__date:nth-child(6) > span > span")]
        IWebElement CheckOut { get; set; }

        // 2nd Page locators
        [FindsBy(How = How.XPath, Using = "//div[@id='filter_group_class_:R1cq:']/div[7]/label/span[2]")]
        IWebElement Star { get; set; }

        [FindsBy(How = How.ClassName, Using = "a53696345b")]
        IList<IWebElement> BreakfastTag { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#search_results_table > div:nth-child(2) > div > div > div > div.d7a0553560 > div.a826ba81c4.fa71cba65b.fa2f36ad22.afd256fc79.d08f526e0d.ed11e24d01.ef9845d4b3.b727170def > nav > div > div.f32a99c8d1.f78c3700d2 > button")]
        IWebElement NextPageBtn { get; set; }

        [FindsBy(How = How.CssSelector, Using = "#filter_group_mealplan_\\:R2kq\\: > div:nth-child(5) > label > span.bbdb949247")]
        IWebElement BreakfastInc { get; set; }

        [FindsBy(How = How.ClassName, Using = "a53696345b")]
        IWebElement Filter { get; set; }





        public void EnterDestination()
        {
            Destination.SendKeys("London");
        }

        public void SelectDate()
        {
           Thread.Sleep(5000);
           DateField.Click();
           CheckIn.Click();
           CheckOut.Click();
        }

        public void ClickSearchButton() 
        { 
            SearchButton.Click();
        }

        public void ClickRating()
        {
            Star.Click();
        }

        public void ListCountStar() 
        {
            while (NextPageBtn.Enabled)
            {
                NextPageBtn.Click();

                Driver.WaitForSpinnerToFinish();

                int resullt = BreakfastTag.Count();

                resultstar += resullt;

                resullt = 0;

            }
        }

        public void ClickMeals()
        {
            Thread.Sleep(10000);

            //BreakfastInc.Click();
        }

        public void ListCountBreakfastIncluded() 
        {
            while (NextPageBtn.Enabled)
            {
                NextPageBtn.Click();

                Driver.WaitForSpinnerToFinish();

                int resullt = BreakfastTag.Count();

                resultbrek += resullt;

                resullt = 0;

            }
        }

        public void Validation() 
        {
            Assert.AreEqual(resultstar, resultbrek, "Values are not Equal!");
        }
    }
}
