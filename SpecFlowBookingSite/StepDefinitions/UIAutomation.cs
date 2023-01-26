using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Presentation;

using FluentAssertions;
using Gherkin.CucumberMessages.Types;
using LivingDoc.Dtos;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.Extensions;
using System.Diagnostics;
using System.Xml.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using System.Collections.Generic;
using SeleniumAutoSite;


namespace SpecFlowCalculator.Specs.Steps
{
    [Binding]
    public class UIAutomation
    {
        public IWebDriver Driver { get; set; }
        public string TestUrl = "https://automationintesting.online/";

        public UIAutomation()
        {
            Driver = new ChromeDriver(@"C:\\Users\shezs\\ChromeDriver");
            Driver.Manage().Window.Maximize();
        }

        [Given("I open the automation testing site")]
        public void IOpenTheAutomationTestingSite()
        {
            Driver.Navigate().GoToUrl(TestUrl);
        }

        [Given("I wait for the site to load")]
        public void IWaitForTheSiteToLoad()
        {            
            System.Threading.Thread.Sleep(2000);
        }

        [Given("I scroll down and go to Contact us form")]
        public void IScrollDownAndGoToContactUsForm()
        {
            var elem = Driver.FindElement(By.ClassName("col-sm-5"));
            Actions actions = new Actions(Driver);
            actions.MoveToElement(elem);
            actions.Perform();

            Debug.WriteLine("Page scroll down Element found");
        }

        [Given("I fill the Contact details")]
        public void IFillTheContactDetails(Table table)
        {
            ContactFormData contact = table.CreateInstance<ContactFormData>();

            for (int i = 0; i < table.RowCount; i++)
            {
                Driver.FindElement(By.Id(table.Rows[i][2])).SendKeys(table.Rows[i][1]);
            }
        }

        [Given ("I Complete the details I click submit button")]
        public void WhenICompleteTheDetailsIClickSubmitButton()
        {
            var submit = Driver.FindElement(By.Id("submitContact"));
            submit.Click();
        }

        [Then ("I get an acknowledgement that the form is submitted")]
        public void WhenIGetAnAcknowledgementThatTheFormIsSubmitted()
        {
            Driver.FindElement(By.ClassName("col-sm-5"));
            Debug.WriteLine("Confirmation message found");
        }

        [Then("I close the automation testing site")]
        public void ThenICloseTheAutomationTestingSite()
        {
            Driver.Quit();
        }
    }
}


      

    
