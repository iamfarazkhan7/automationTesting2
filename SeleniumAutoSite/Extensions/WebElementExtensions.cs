using System;
using System.Linq;
using System.Threading;
using HtmlAgilityPack;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace TG.Test.WebApps.Common.Extensions
{
    public static class WebElementExtensions
    {
        public static bool IsElementDisplayed(this IWebElement element)
        {
            try
            {
                return element.Displayed;
            }
            catch
            {
                return false;
            }
        }
        
        public static bool IsElementEnabled(this IWebElement element)
        {
            try
            {
                return element.Enabled;
            }
            catch
            {
                return false;
            }
        }

        public static IWebElement GetChildInputField(this IWebElement element)
        {
            return element.FindElement(By.TagName("input"));
        }

        public static string GetValueAttribute(this IWebElement element)
        {
            return element.GetAttribute("value");
        }

        public static void SendKeysToInput(this IWebElement element, string value)
        {
            if (value == null)
                return;

            element.GetChildInputField().SendKeysLazy(element, value);
        }

        public static void SendKeysLazy(this IWebElement element, string value, bool withClear = true)
        {
            SendKeysLazy(element, null, value, withClear);
        }

        public static string GetInputAttributeValue(this IWebElement element)
        {
            return element.FindElement(By.TagName("input")).GetAttribute("value");
        }

        public static void SendKeysLazy(this IWebElement element, IWebElement parentElement, string value, bool withClear = true)
        {
            if (value == null)
                return;

            if (value != string.Empty)
            {
                if (withClear)
                {
                    element.Clear();
                    Thread.Sleep(50);
                }
                element.SendKeys(value);
            }
            else if (parentElement != null && !string.IsNullOrEmpty(element.GetValueAttribute()))
            {
                parentElement.ClearFieldWithButton();
            }
        }

        public static bool ClearFieldWithButton(this IWebElement element)
        {
            var clearButton = element.FindElement(By.TagName("button"));
            clearButton.Click();

            var elementValue = element.GetValueAttribute();
            return string.IsNullOrEmpty(elementValue);
        }

        public static void HoverByMouse(this IWebElement element, IWebDriver webDriver, int sleepAmount = 500)
        {
            var actions = new Actions(webDriver);
            actions.MoveToElement(element).Perform();

            Thread.Sleep(sleepAmount);
        }

        public static void ClickByMouse(this IWebElement element, IWebDriver webDriver)
        {
            var actions = new Actions(webDriver);
            actions.Click(element).Perform();
        }

        public static void ClickOffsetByMouse(this IWebDriver webDriver, int x, int y)
        {
            var actions = new Actions(webDriver);
            actions.MoveByOffset(x, y).Click().Build().Perform();
        }

        public static bool WaitForElementToBeEnabled(this IWebElement element, int timeoutInMilliseconds = 2000, int pollingIntervalInMilliseconds = 500)
        {
            return element.WaitUntil((element) => element.Enabled, timeoutInMilliseconds, pollingIntervalInMilliseconds);
        }

        public static bool WaitForElementToBeDisabled(this IWebElement element, int timeoutInMilliseconds = 2000, int pollingIntervalInMilliseconds = 500)
        {
            return element.WaitUntil((element) => !element.Enabled, timeoutInMilliseconds, pollingIntervalInMilliseconds);
        }

        public static bool WaitForElementIsDisplayed(this IWebElement element, int timeoutInMilliseconds = 2000)
        {
            try
            {
                return element.WaitUntil((element) => element.Displayed, timeoutInMilliseconds);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static IWebElement WaitForElementIsClickable(this IWebElement element, IWebDriver driver, int timeoutInMilliseconds = 3000)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutInMilliseconds));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        public static IWebElement WaitForElementIsDisplayedAndClickable(this IWebElement element, IWebDriver driver, int timeoutInMilliseconds = 3000)
        {
            element.WaitForElementIsDisplayed(timeoutInMilliseconds);

            var wait = new WebDriverWait(driver, TimeSpan.FromMilliseconds(timeoutInMilliseconds));
            return wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(element));
        }

        public static bool WaitForTextIsNotNullOrEmpty(this IWebElement element, int timeoutInMilliseconds = 2000)
        {
            return element.WaitUntil((element) => !string.IsNullOrEmpty(element.Text), timeoutInMilliseconds);
        }

        public static TResult WaitUntil<TResult>(this IWebElement element, Func<IWebElement, TResult> func, int timeoutInMilliseconds = 2000, int pollingIntervalInMilliseconds = 500)
        {
            var wait = new DefaultWait<IWebElement>(element);
            wait.PollingInterval = TimeSpan.FromMilliseconds(pollingIntervalInMilliseconds);
            wait.Timeout = TimeSpan.FromMilliseconds(timeoutInMilliseconds);

            return wait.Until(func);
        }

        public static IWebElement FindParentElementByTagName(this IWebElement element, string tagName)
        {
            IWebElement parent = element;

            do
            {
                parent = parent.FindElement(By.XPath("./parent::*"));
            } while (parent.TagName != tagName);

            return parent;
        }

        public static IWebElement FindElementOrDefault(this IWebElement element, By locator)
        {
            try
            {
                return element.FindElement(locator);
            }
            catch (Exception)
            {
                return default;
            }
        }

        public static bool IsAttributeContains(this IWebElement element, string attributeName, string value)
        {
            return element.GetAttribute(attributeName).Contains(value);
        }

        /// <summary>
        /// Get own inner text of element
        /// </summary>
        /// <param name="element">This element</param>
        /// <returns>Inner text of the current element. child nodes text is not included.</returns>
        public static string OwnText(this IWebElement element)
        {
            var outerHTML = element.GetAttribute("outerHTML");
            HtmlDocument html = new HtmlDocument();
            html.LoadHtml(outerHTML);
            var nodes = html.DocumentNode.FirstChild.ChildNodes;
            return nodes.First(node=> node.OriginalName.Equals(("#text"), StringComparison.OrdinalIgnoreCase)).InnerText;
        }

        public static string GetTextOrDefault(this IWebElement element, string defaultText = null)
        {
            try
            {
                return element.Text;
            }
            catch (Exception)
            {
                return defaultText;
            }
        }

        public static string GetTextOrEmpty(this IWebElement element)
        {
            return element.GetTextOrDefault(string.Empty);
        }

        public static bool WaitForElementIsNotDisplayed(this IWebElement element, int timeoutInMilliseconds = 4000)
        {
            try
            {
                return element.WaitUntil((element) => !element.Displayed, timeoutInMilliseconds);
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void SendKeys(this IWebDriver webDriver, string text)
        {
            var actions = new Actions(webDriver);
            actions.SendKeys(text).Perform();
        }
    }
}
