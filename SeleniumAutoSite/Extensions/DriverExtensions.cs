using OpenQA.Selenium;
using OpenQA.Selenium.Appium.Interfaces;
using OpenQA.Selenium.Appium.MultiTouch;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Linq;


namespace TG.Test.WebApps.Common.Extensions
{
    public static class DriverExtensions
    {
        public static void WaitForPageToLoad(this IWebDriver driver, int retryCount = 0)
        {
            try
            {
                WaitForSplashLoaderToBeInvisible(driver);
                WaitForPageToLoadContent(driver);
                WaitForProgressBarToFinish(driver, 20);
            }
            catch (StaleElementReferenceException ex)
            {
                retryCount++;
                if (retryCount > 3)
                {
                    throw new Exception($"Page couldn't load after 3 retries with error: {ex}");
                }
                WaitForPageToLoad(driver, retryCount);
            }
        }

        public static TResult WaitUntil<TResult>(this IWebDriver driver, Func<IWebDriver, TResult> func, TimeSpan timeout)
        {
            WebDriverWait wait = new WebDriverWait(driver, timeout);
            return wait.Until(func);
        }

        public static void WaitForSplashLoaderToBeInvisible(this IWebDriver driver)
        {
            WaitForElementPropertyToBe(driver, "splash", "display", "none");
        }

        public static void WaitForPageToLoadContent(this IWebDriver driver)
        {
            WaitForElementInnerHtmlNotToBe(driver, "ten-app-root", "");
        }

        public static void WaitForElementToBeVisible(this IWebDriver driver, By element)
        {
            WaitForElementToBeVisible(driver, element, 15);
        }

        public static void WaitForElementToBeVisible(this IWebDriver driver, By element, int timeout)
        {
            driver.WaitUntil(ExpectedConditions.ElementIsVisible(element), TimeSpan.FromSeconds(timeout));
        }

        public static void WaitForElementToBeInvisible(this IWebDriver driver, By element, int timeout = 90)
        {
            driver.WaitUntil(ExpectedConditions.InvisibilityOfElementLocated(element), TimeSpan.FromSeconds(timeout));
        }

        public static void WaitForElementToBeDetached(this IWebDriver driver, IWebElement element, int timeout = 30)
        {
            driver.WaitUntil(ExpectedConditions.StalenessOf(element), TimeSpan.FromSeconds(timeout));
        }

        public static void WaitForElementToBeAttached(this IWebDriver driver, IWebElement element)
        {
            driver.WaitUntil(ExpectedConditions.ElementToBeClickable(element), TimeSpan.FromSeconds(30));
        }

        public static void WaitForElementCountToBe(this IWebDriver driver, By selector, int expectedValue)
        {
            driver.WaitUntil((driver) =>
            {
                return driver.FindElements(selector).Count == expectedValue;
            }, TimeSpan.FromSeconds(10));
        }

        public static void WaitForElementTextToContain(this IWebDriver driver, IWebElement selector, string expectedText)
        {
            driver.WaitUntil((driver) =>
            {
                return selector.Text.Contains(expectedText);
            }, TimeSpan.FromSeconds(10));
        }

        public static void WaitForElementAttributeToContain(this IWebDriver driver, IWebElement selector, string attributeName, string expectedText, int timeoutInSeconds = 10)
        {
            driver.WaitUntil((driver) =>
            {
                return selector.GetAttribute(attributeName).Contains(expectedText);
            }, TimeSpan.FromSeconds(timeoutInSeconds));
        }

        public static void WaitForElementAttributeToNotContain(this IWebDriver driver, IWebElement selector, string attributeName, string expectedText)
        {
            driver.WaitUntil((driver) =>
            {
                return !(selector.GetAttribute(attributeName).Contains(expectedText));
            }, TimeSpan.FromSeconds(10));
        }

        public static void WaitForElementPropertyToBe(this IWebDriver driver, string selector, string attributeName, string expectedText)
        {
            driver.WaitUntil((driver) =>
            {
                return DriverExtensionsJs.GetElementPropertyByClassName(driver, selector, attributeName).Equals(expectedText);

            }, TimeSpan.FromSeconds(30));
        }

        public static void WaitForElementPropertyNotToBe(this IWebDriver driver, string selector, string attributeName, string expectedText)
        {
            driver.WaitUntil((driver) =>
            {
                return !DriverExtensionsJs.GetElementPropertyByClassName(driver, selector, attributeName).Equals(expectedText);

            }, TimeSpan.FromSeconds(30));
        }

        public static void WaitForElementInnerHtmlToBe(this IWebDriver driver, string selector, string expectedText)
        {
            driver.WaitUntil((driver) =>
            {
                return DriverExtensionsJs.GetElementInnerHtmlByTagName(driver, selector).Equals(expectedText);
            }, TimeSpan.FromSeconds(10));
        }

        public static void WaitForElementInnerHtmlNotToBe(this IWebDriver driver, string selector, string expectedText)
        {
            driver.WaitUntil((driver) =>
            {
                return !DriverExtensionsJs.GetElementInnerHtmlByTagName(driver, selector).Equals(expectedText);
            }, TimeSpan.FromSeconds(10));
        }

        public static void WaitForElementAttributeToBe(this IWebDriver driver, IWebElement selector, string attributeName, string expectedText, int second = 10)
        {
            driver.WaitUntil((driver) =>
            {
                return selector.GetAttribute(attributeName).Equals(expectedText);
            }, TimeSpan.FromSeconds(second));
        }

        public static void WaitForElementAttributeToBeNotNull(this IWebDriver driver, IWebElement selector, string attributeName)
        {
            driver.WaitUntil((driver) =>
            {
                return selector.GetAttribute(attributeName) != null;
            }, TimeSpan.FromSeconds(10));
        }

        public static void WaitForElementAttributeToBeNull(this IWebDriver driver, IWebElement selector, string attributeName)
        {
            driver.WaitUntil((driver) =>
            {
                return selector.GetAttribute(attributeName) == null;
            }, TimeSpan.FromSeconds(10));
        }

        public static void WaitForRootElementToBecomeActive(this IWebDriver driver)
        {
            IWebElement rootElement = driver.FindElement(By.TagName("ten-app-root"));
            driver.WaitUntil((driver) =>
            {
                return rootElement.GetAttribute("aria-hidden") == null;
            }, TimeSpan.FromSeconds(10));
        }

        public static void WaitForAnimationToFinish(this IWebDriver driver, IWebElement selector)
        {
            WaitForElementAttributeToNotContain(driver, selector, "class", "ng-animating");
        }

        public static void WaitForAnimationsToFinish(this IWebDriver driver, IEnumerable<IWebElement> selectors)
        {
            foreach (var selector in selectors)
            {
                WaitForAnimationToFinish(driver, selector);
            }
        }

        public static void WaitForElementToBeVisibleThenInvisible(this IWebDriver driver, By element)
        {
            WaitForElementToBeVisible(driver, element);
            WaitForElementToBeInvisible(driver, element);
        }

        public static void WaitForLazyProgressBarToFinish(this IWebDriver driver, int second = 30)
        {
            //There are some actions which does not activate the progress bar immediately.
            System.Threading.Thread.Sleep(500);
            WaitForProgressBarToFinish(driver, second);
        }

        public static void WaitForProgressBarToFinish(this IWebDriver driver, int second = 30)
        {
            var progressBars = driver.FindElements(By.CssSelector("mat-progress-bar[aria-valuenow]"));
            if (progressBars.Count > 0)
            {
                //Between 2 pages the progess bar is counting after redirection, minimal waiting is required.
                System.Threading.Thread.Sleep(100);
                WaitForElementAttributeToBe(driver, progressBars[0], "aria-valuenow", "0", second);
            }
        }

        public static void WaitForSpinnerToFinish(this IWebDriver driver, int second = 90)
        {
            var spinner = By.CssSelector("mat-progress-spinner");
            WaitForElementToBeInvisible(driver, spinner, second);
        }

        public static void WaitForElementColorChange(this IWebDriver driver)
        {
            //Waiting for the button to change colour which is not part of animation.
            System.Threading.Thread.Sleep(500);
        }

        //public static bool IsMobileTestingEnabled()
        //{
        //    return bool.Parse(ConfigurationReaderHelper.GetProperty("is_mobile_testing_enabled"));
        //}

        //public static bool IsIosMobileTestingEnabled()
        //{
        //    return IsMobileTestingEnabled() && ConfigurationReaderHelper.GetProperty("mobile_testing_parameters:device_type").Equals("iPhone");
        //}

        public static void ScrollIntoElementAndClick(this IWebDriver driver, IWebElement element)
        {
            ScrollIntoElement(driver, element);
            element.Click();
        }

        public static void ScrollIntoElementAndSendKeys(this IWebDriver driver, IWebElement element, string keys)
        {
            ScrollIntoElement(driver, element);
            element.SendKeys(keys);
        }

        public static IWebElement ScrollIntoElementAndFindElementWithin(this IWebDriver driver, IWebElement element, By elementToFind)
        {
            ScrollIntoElement(driver, element);
            return element.FindElement(elementToFind);
        }

        public static ReadOnlyCollection<IWebElement> ScrollIntoElementAndFindElementsWithin(this IWebDriver driver, IWebElement element, By elementsToFind)
        {
            ScrollIntoElement(driver, element);
            return element.FindElements(elementsToFind);
        }

        public static void ScrollIntoElement(this IWebDriver driver, IWebElement element)
        {
            if (false)//IsMobileTestingEnabled())
            {
                var timeout = 30;
                DriverExtensionsJs.ScrollToTop(driver);
                var displaySize = driver.Manage().Window.Size;

                var startScrollingTime = DateTime.UtcNow;
                while (!element.IsElementDisplayed())
                {
                    TouchAction action = new TouchAction((IPerformsTouchActions)driver);
                    ScrollDown(action, "large", displaySize);
                    if ((DateTime.UtcNow - startScrollingTime).Seconds > timeout)
                    {
                        throw new Exception($"Could not find element in {timeout} seconds while scrolling");
                    }
                }
                while (!element.IsElementEnabled())
                {
                    TouchAction action = new TouchAction((IPerformsTouchActions)driver);
                    ScrollDown(action, "small", displaySize);
                    if ((DateTime.UtcNow - startScrollingTime).Seconds > timeout)
                    {
                        throw new Exception($"Could not find element in {timeout} seconds while scrolling");
                    }
                }
            }
            else
            {
                DriverExtensionsJs.ExecuteJavaScript(driver, element, "arguments[0].scrollIntoView(false)");
            }
        }

        public static void ScrollIntoPopupElement(this IWebDriver driver, IWebElement element)
        {
            Actions action = new Actions(driver);
            action.MoveToElement(element);
        }

        private static void ScrollDown(TouchAction action, string stepSize, Size displaySize)
        {
            var centerOfDisplayX = displaySize.Width / 2;
            var centerOfDisplayY = displaySize.Height / 2;
            var minorStepSize = 10;

            var minorScrollTargetPositionY = centerOfDisplayY - minorStepSize;
            var largeScrollTargetPositionY = 1; //top of the screen

            var targetY = stepSize.Equals("large")
                ? largeScrollTargetPositionY
                : minorScrollTargetPositionY;

            action.Press(x: centerOfDisplayX, y: centerOfDisplayY);
            action.Wait(500);
            action.MoveTo(x: centerOfDisplayX, y: targetY);
            action.Release();
            action.Perform();
        }

        public static void ScrollIntoElementAndClickOnAnother(this IWebDriver driver, IWebElement elementToScroll, IWebElement elementToClick)
        {
            ScrollIntoElement(driver, elementToScroll);
            elementToClick.Click();
        }

        public static void ClickOnElementOutsideOfWindow(this IWebDriver driver, IWebElement element)
        {
            try
            {
                element.Click();
            }
            catch
            {
                // Gentle wait
                System.Threading.Thread.Sleep(500);
                element.Click();
            }
        }

        public static void WaitForCheckboxToBeChecked(this IWebDriver driver, IWebElement selector)
        {
            var checkbox = selector.FindElement(By.CssSelector(".mat-checkbox-input"));
            WaitForElementAttributeToBe(driver, checkbox, "aria-checked", "true");
            WaitForElementColorChange(driver);
        }

        public static bool IsMcSite(this IWebDriver driver)
        {
            return driver.Url.Contains("mastercard", StringComparison.OrdinalIgnoreCase) ||
                driver.Url.Contains("snb", StringComparison.OrdinalIgnoreCase) ||
                driver.Url.Contains("itau", StringComparison.OrdinalIgnoreCase);
        }

        public static List<IWebElement> FindElementsByCssSelector(this IWebDriver driver, string locator)
        {
            return driver.FindElements(By.CssSelector(locator)).ToList();
        }

        public static void WaitTillVisibilityOfAllElementsLocatedBy(this IWebDriver driver, string locator)
        {
            driver.WaitUntil((driver) =>
            {
                return ExpectedConditions.VisibilityOfAllElementsLocatedBy(By.CssSelector(locator));
            }, TimeSpan.FromSeconds(30));
        }
    }
}
