using OpenQA.Selenium;
using System;


namespace TG.Test.WebApps.Common.Extensions
{
    public static class DriverExtensionsIFrame
    {
        public static void SwitchToIframeByName(this IWebDriver driver, string frameName)
        {
            driver.WaitForElementToBeVisible(By.CssSelector($"iframe[name='{frameName}']"));
            driver.WaitForPageToLoad();
            driver.SwitchTo().Frame(frameName);
        }

        public static void SwitchToIframeByClassName(this IWebDriver driver, string frameName)
        {
            driver.WaitForElementToBeVisible(By.CssSelector($".{frameName}"));
            driver.WaitForPageToLoad();
            driver.SwitchTo().Frame(driver.FindElement(By.CssSelector($".{frameName}")));
        }

        public static void SwitchToMainPageFromFrame(this IWebDriver driver)
        {
            driver.SwitchTo().DefaultContent();
        }

        public static void ExecuteFunctionOnIFrame(this IWebDriver driver, IWebElement iFrame, Action action)
        {
            driver.SwitchTo().Frame(iFrame);
            action.Invoke();
            driver.SwitchToMainPageFromFrame();
        }

        public static void ExecuteFunctionOnIFrame(this IWebDriver driver, Action action)
        {
            driver.ExecuteFunctionOnIFrame(driver.FindElement(By.TagName("iframe")), action);
        }

        public static void ExecuteFunctionOnIFrameWithContainer(this IWebDriver driver, IWebElement container, Action action)
        {
            var iFrame = container.FindElement(By.TagName("iframe"));
            driver.ExecuteFunctionOnIFrame(iFrame, action);
        }

        public static string InvokeIFrameElementGetMethod(this IWebDriver driver, IWebElement iFrame, By elementLocator, IWebElementMethodName methodName, params object[] parameters)
        {
            return driver.InvokeIFrameElementMethod<string>(iFrame, elementLocator, methodName, parameters);
        }

        public static void InvokeIFrameElementMethod(this IWebDriver driver, IWebElement iFrame, By elementLocator, IWebElementMethodName methodName, params object[] parameters)
        {
            driver.InvokeIFrameElementMethod<object>(iFrame, elementLocator, methodName, parameters);
        }

        //public static T GetIFrameElementPropertyValue<T>(this IWebDriver driver, IWebElement iFrame, By elementLocator, IWebElementPropertyName propertyName)
        //{
        //    object value = null;
        //    driver.ExecuteFunctionOnIFrame(iFrame, () =>
        //    {
        //        value = driver.FindElement(elementLocator).GetPropertyValue<T>(propertyName.ToString());
        //    });
        //    return (T)value;
        //}

        //public static T GetIFrameElementPropertyValueOrDefault<T>(this IWebDriver driver, IWebElement iFrame, By elementLocator, IWebElementPropertyName propertyName)
        //{
        //    try
        //    {
        //        return driver.GetIFrameElementPropertyValue<T>(iFrame, elementLocator, propertyName);
        //    }
        //    catch (Exception)
        //    {
        //        return default;
        //    }
        //}

        private static T InvokeIFrameElementMethod<T>(this IWebDriver driver, IWebElement iFrame, By elementLocator, IWebElementMethodName methodName, params object[] parameters)
        {
            object value = null;
            driver.ExecuteFunctionOnIFrame(iFrame, () =>
            {
                var element = driver.FindElement(elementLocator);
                value = element.GetType().GetMethod(methodName.ToString()).Invoke(element, parameters);
            });
            return (T)value;
        }
    }
}
