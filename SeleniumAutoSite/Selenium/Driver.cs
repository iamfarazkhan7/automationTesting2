using OpenQA.Selenium;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections.Specialized;
using System.Drawing;
using System.Web;
using TG.Test.WebApps.Common.Drivers;
using TG.Test.WebApps.Common.DTO;
using TG.Test.WebApps.Common.Extensions;
using TG.Test.WebApps.Common.Selenium.Enums;
using DevTools = OpenQA.Selenium.DevTools.V108;
using DevToolsFetch = OpenQA.Selenium.DevTools.V108.Fetch;
using DevToolsNetwork = OpenQA.Selenium.DevTools.V108.Network;

namespace TG.Test.WebApps.Common.Selenium
{
    public class Driver
    {
        public IWebDriver Browser;

        public List<NetworkRequest> NetworkRequests = new List<NetworkRequest>();

        private TimeSpan implicitWait;

        //public IDevTools DevTools => Browser as IDevTools;

        public void InitializeDriver(BrowserType browserType, string featureTitle = "")
        {
            if (!IsBrowserExist())
            {
                switch (browserType)
                {
                    case BrowserType.CHROME:
                        Browser = ChromeWebDriver.LoadChromeDriver();
                        Browser.Manage().Window.Maximize();
                        break;
                    
                    case BrowserType.FIREFOX:
                        Browser = FirefoxWebDriver.LoadFirefoxDriver();
                        Browser.Manage().Window.FullScreen();
                        break;
                    case BrowserType.REMOTE_FIREFOX:
                        Browser = HeadlessFirefoxWebDriver.LoadHeadlessFirefoxDriver();
                        Browser.Manage().Window.Size = new Size(1920, 1080);
                        break;
                    case BrowserType.EDGE:
                        Browser = EdgeWebDriver.LoadEdgeDriver();
                        break;
                    
                    default:
                        string message = "Invalid browser type!";
                        //LoggerHelper.SLog.Error(message);
                        throw new Exception(message);
                }
            }
        }

        public void SetImplicitWait(double seconds)
        {
            implicitWait = TimeSpan.FromSeconds(seconds);
            Browser.Manage().Timeouts().ImplicitWait = implicitWait;
        }

        public void SetImplicitWaitToZero()
        {
            Browser.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(0);
        }

        public void RevertImplicitWait()
        {
            Browser.Manage().Timeouts().ImplicitWait = implicitWait;
        }

        public void NavigateToUrl(string url)
        {
            try
            {
                Browser.Navigate().GoToUrl(new Uri(url));
            }
            catch (WebDriverException)
            {
                //Temporarily it is disabled and let scenario continue
                //https://github.com/SeleniumHQ/selenium/issues/10799
                System.Threading.Thread.Sleep(500);
            }
        }

        public string GetCurrentUrl()
        {
            return Browser.Url;
        }

        public NameValueCollection GetCurrentUrlParameters()
        {
            var currentUri = new Uri(GetCurrentUrl());
            var queryParameters = HttpUtility.ParseQueryString(currentUri.Query);

            return queryParameters;
        }

        public void RefreshPage()
        {
            Browser.Navigate().Refresh();
        }

        public void ClickOnBackButton()
        {
            Browser.Navigate().Back();
        }

        public void CloseDriver()
        {
            if (IsBrowserExist())
            {
                Browser.Dispose();
                Browser.Quit();
                Browser = null;
            }
        }

        public void CloseCurrentWindowAndSwitchTo(int tabIndex = 0)
        {
            Browser.Close();
            SwitchToWindow(tabIndex, 1);
        }

        public bool IsBrowserExist()
        {
            return Browser != null;
        }

        public IEnumerable<string> WaitForWindowsCountToBe(int windowsCount = 2, int timeoutInSeconds = 120)
        {
            Browser.WaitUntil((driver) =>
            {
                var browserTabs = driver.WindowHandles;
                return browserTabs.Count >= windowsCount;
            }, TimeSpan.FromSeconds(timeoutInSeconds));

            return Browser.WindowHandles;
        }

        public void SwitchToWindow(int windowIndex = 1, int windowsCount = 2, int timeoutInSeconds = 120)
        {
            var browserTabs = WaitForWindowsCountToBe(windowsCount, timeoutInSeconds);
            Browser.SwitchTo().Window(browserTabs.ElementAt(windowIndex));

            //if (DevTools.HasActiveDevToolsSession)
            //{
            //    InitNetworkAdapter();
            //}
        }

        public void WaitForSpinnerToFinish()
        {
            Browser.WaitForSpinnerToFinish();
        }

        public int GetWindowsCount()
        {
            return Browser.WindowHandles.Count();
        }

        public bool SwitchToTabContaining(string urlPart, int repeatCount = 10, int timeoutInSeconds = 2)
        {
            for (int i = 0; i < repeatCount; i++)
            {
                var windowHandles = GetAllWindowWillBeOpened(timeoutInSeconds);

                foreach (var windowHandle in windowHandles)
                {
                    Browser.SwitchTo().Window(windowHandle);

                    if (GetCurrentUrl().Contains(urlPart))
                    {
                        //if (DevTools.HasActiveDevToolsSession)
                        //{
                        //    InitNetworkAdapter();
                        //}

                        return true;
                    }
                }
            }

            return false;
        }




        private IEnumerable<string> GetAllWindowWillBeOpened(int timeoutInSeconds = 5)
        {
            int expectedWindowsCount = 1;
            IEnumerable<string> windowHandles = new List<string>();

            try
            {
                while (expectedWindowsCount > windowHandles.Count())
                {
                    windowHandles = WaitForWindowsCountToBe(expectedWindowsCount, timeoutInSeconds);
                    expectedWindowsCount = windowHandles.Count() + 1;
                }
            }
            catch (Exception)
            {

            }

            return windowHandles;
        }

        
    }
}
