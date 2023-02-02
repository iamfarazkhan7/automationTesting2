using BasePageObjectModel;
using DocumentFormat.OpenXml.Bibliography;
using DocumentFormat.OpenXml.Drawing.Charts;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.DevTools.V85.Emulation;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using PracticeProject.Pages;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.ObjectModel;
using TechTalk.SpecFlow;
using TG.Test.WebApps.Common.Extensions;
using TG.Test.WebApps.Common.Pages.Base;
using TG.Test.WebApps.Common.Selenium;
using TG.Test.WebApps.Common.Selenium.Enums;

namespace PracticeProject.StepDefinitions
{
    [Binding]
    public class BookingAPropertyStepDefinitions
    {
        public Driver Driver { get; set; }

        
        public BookingInputs BookingInputs { get; set; }

        

        String Url = "https://www.booking.com/";

        

        public BookingAPropertyStepDefinitions(Driver driver, BookingInputs bookingInputs)
        {   
            Driver = driver;
            BookingInputs = bookingInputs;
            
        }
       
        [Given(@"User is on the homepage of Browser")]
        public void GivenUserIsOnTheHomepageOfBrowser()
        {
            
        }


        [When(@"User navigates to homepage of Booking\.com")]
        public void WhenUserNavigatesToHomepageOfBooking_Com()
        {
           Driver.NavigateToUrl(Url);
        }

        [When(@"User typed London in the search bar")]
        public void WhenUserTypedLondonInTheSearchBar()
        {
            BookingInputs.EnterDestination();

        }

        [When(@"User selected the Date")]
        public void WhenUserSelectedTheDate()
        {
            BookingInputs.SelectDate();
        }

        [When(@"User clicked on the search button")]
        public void WhenUserClickedOnTheSearchButton()
        {
            BookingInputs.ClickSearchButton();
        }

        [When(@"User selected the (.*)star checkbox from Star Rating")]
        public void WhenUserSelectedTheStarCheckboxFromStarRating(int p0)
        {
            Driver.WaitForSpinnerToFinish();
            BookingInputs.ClickRating();
        }

        [When(@"List the total count of the properties which have Breakfast Included tag\.")]
        public void WhenListTheTotalCountOfThePropertiesWhichHaveBreakfastIncludedTag_()
        {
            BookingInputs.ListCountStar();
        }

        [When(@"User selected the Breakfast Included checkbox from Meals")]
        public void WhenUserSelectedTheBreakfastIncludedCheckboxFromMeals()
        { 
            BookingInputs.ClickMeals();
        }
        

        [When(@"List the total count of the properties which have Breakfast Included tag while both checkbox are selected\.")]
        public void WhenListTheTotalCountOfThePropertiesWhichHaveBreakfastIncludedTagWhileBothCheckboxAreSelected_()
        {
            BookingInputs.ListCountBreakfastIncluded();
        }

        [Then(@"Validate the results of both of the total counts")]
        public void ThenValidateTheResultsOfBothOfTheTotalCounts()
        {
            BookingInputs.Validation();
        }


    }
}
