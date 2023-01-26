using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;


namespace TG.Test.WebApps.Common.Extensions
{
    public static class DriverExtensionsJs
    {
        public static void WaitForAngularPageToBeLoaded(this IWebDriver driver)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

            Func<IWebDriver, Boolean> waitForAngularPage = new Func<IWebDriver, Boolean>((IWebDriver Driver) =>
            {
                return (Boolean)ExecuteJavaScript(Driver, "return window.getAllAngularTestabilities()[0]._ngZone.isStable");
                //return (Boolean)executeJavaScript(Driver, "return window.getAllAngularTestabilities().findIndex(x=>!x.isStable()) === -1");
            });

            wait.Until(waitForAngularPage);
        }

        public static object ExecuteJavaScript(IWebDriver driver, String scriptcontent)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript(scriptcontent);
        }

        public static object ExecuteJavaScript(IWebDriver driver, IWebElement element, String scriptcontent)
        {
            return ((IJavaScriptExecutor)driver).ExecuteScript(scriptcontent, element);
        }

        public static void MoveElementToTopLeftCorner(this IWebDriver driver, IWebElement element)
        {
            ExecuteJavaScript(driver, element, "arguments[0].style = 'top: 0px; left: 0px;'");
        }

        public static void ScrollToTop(this IWebDriver driver)
        {
            ExecuteJavaScript(driver, "window.scrollTo(0, 0);");
        }

        public static void ScrollToMiddle(this IWebDriver driver)
        {
            var script = @"var scrollbarHeight = window.innerHeight * (window.innerHeight/document.body.offsetHeight);
                           window.scrollTo(0, document.body.scrollHeight/2 - scrollbarHeight);";
            ExecuteJavaScript(driver, script);
        }

        public static void ScrollToBottom(this IWebDriver driver)
        {
            ExecuteJavaScript(driver, "window.scrollTo(0, document.body.scrollHeight);");
        }

        public static void ClearLocalStorage(this IWebDriver driver)
        {
            ExecuteJavaScript(driver, "window.localStorage.clear();");
        }

        public static string GetItemFromLocalStorage(this IWebDriver driver, string item)
        {
            return (String)ExecuteJavaScript(driver, $"return localStorage.getItem('{item}');");
        }

        public static void JsClick(this IWebDriver driver, IWebElement element)
        {
            ExecuteJavaScript(driver, element, "arguments[0].click()");
        }

        // Use this method when needs to scroll into element in main window.
        public static void ScrollWindowIntoElementTop(this IWebDriver driver, IWebElement element)
        {
            string OffsetHeight = driver.FindElement(By.TagName("global-nav")).GetAttribute("offsetHeight");
            ExecuteJavaScript(driver, element, $"arguments[0].scrollIntoView(true);window.scrollBy(0, -{OffsetHeight});");
        }

        public static void ScrollIntoElementTop(this IWebDriver driver, IWebElement element)
        {
            ExecuteJavaScript(driver, element, "arguments[0].scrollIntoView(true)");
        }

        public static string GetValueFromHtmlSchemeByName(this IWebDriver driver, string name)
        {
            return ExecuteJavaScript(driver, $"return getComputedStyle(document.getElementsByTagName(\"html\")[0]).getPropertyValue(\"{name}\");").ToString().Trim();
        }

        public static string GetElementInnerHtmlByTagName(this IWebDriver driver, string selector)
        {
            return ExecuteJavaScript(driver, $"return document.getElementsByTagName('{selector}')[0].innerHTML;").ToString();
        }

        public static string GetElementPropertyByClassName(this IWebDriver driver, string selector, string name)
        {
            return ExecuteJavaScript(driver, $"return window.getComputedStyle(document.getElementsByClassName('{selector}')[0]).{name};").ToString();
        }

        // CSS pseudo-elements such as ::before and ::after styles helper
        public static string GetValueFromHtmlForPseudoElement(this IWebDriver driver, string selector, string style)
        {
            return ExecuteJavaScript(driver, $"return window.getComputedStyle(document.querySelector('{selector}'), ':{style}').getPropertyValue('content');").ToString();
        }

        //public static T GetJsObjectPropertyDefault<T>(this IWebDriver driver, IWebElement element, JsObjectProperty propertyName)
        //{
        //    try
        //    {
        //        return GetJsObjectProperty<T>(driver, element, propertyName);
        //    }
        //    catch (Exception)
        //    {
        //        return default;
        //    }
        //}

        //public static T GetJsObjectProperty<T>(this IWebDriver driver, IWebElement element, JsObjectProperty propertyName)
        //{
        //    return (T)ExecuteJavaScript(driver, element, $"return arguments[0].{propertyName.GetDefaultValueAttribute<string>()}");
        //}

        //public static void InvokeJsObjectMethod(this IWebDriver driver, IWebElement element, JsObjectMethod methodName)
        //{
        //    InvokeJsObjectMethod<object>(driver, element, methodName);
        //}

        //public static T InvokeJsObjectMethod<T>(this IWebDriver driver, IWebElement element, JsObjectMethod methodName)
        //{
        //    return (T)ExecuteJavaScript(driver, element, $"return arguments[0].{methodName.GetDefaultValueAttribute<string>()}()");
        //}

        //public static string GetJsObjectCssProperty(this IWebDriver driver, IWebElement element, JsObjectCssProperty cssPropertyName)
        //{
        //    return GetJsObjectCssProperty<string>(driver, element, cssPropertyName);
        //}

        //public static T GetJsObjectCssProperty<T>(this IWebDriver driver, IWebElement element, JsObjectCssProperty cssPropertyName)
        //{
        //    return (T)ExecuteJavaScript(driver, element, $"return getComputedStyle(arguments[0]).{cssPropertyName.GetDefaultValueAttribute<string>()}");
        //}
    }
}
