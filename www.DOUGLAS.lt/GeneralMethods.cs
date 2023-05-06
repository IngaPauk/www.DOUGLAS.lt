using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing.Imaging;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;

namespace www.DOUGLAS.lt
{
    public class GeneralMethods
    {
        IWebDriver driver;
        DefaultWait<IWebDriver> wait;

        public GeneralMethods(IWebDriver driver)
        {
            this.driver = driver;
            wait = new DefaultWait<IWebDriver>(driver);  
            wait.Timeout = TimeSpan.FromSeconds(10);   
            wait.PollingInterval = TimeSpan.FromMilliseconds(250);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));  
            wait.IgnoreExceptionTypes(typeof(ElementNotInteractableException));
        }

        public static IWebDriver CreateDriverWithoutNotification() 
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-notifications"); // to disable notification
            return new ChromeDriver(options);
        }

        public IWebElement WaitElement(string xPath, IWebDriver driver) 
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.PollingInterval = TimeSpan.FromSeconds(0.5);
            return wait.Until(d => d.FindElement(By.XPath(xPath)));
        }        

        public void ClickByJavaScript(string xpath)
        {
            IJavaScriptExecutor javascriptExecutor = (IJavaScriptExecutor)driver;
            javascriptExecutor.ExecuteScript("arguments[0].click();",
            driver.FindElement(By.XPath(xpath)));
        }

        public void ClickByJavaScriptWait(string xpath)
        {
            IJavaScriptExecutor javascriptExecutor = (IJavaScriptExecutor)driver;
            var element = WaitElement(xpath, driver);
            javascriptExecutor.ExecuteScript("arguments[0].click();", element);
        }

        public void HoverAndCLickWithTry(string xpath)
        {
            try
            {
                Actions action = new Actions(driver);
                action.MoveToElement(driver.FindElement(By.XPath(xpath))).Perform();
                driver.FindElement(By.XPath(xpath)).Click();
            }
            catch (Exception)
            {
                throw new Exception($"Element '{xpath}' not found");
            }
        }

        public void EnterTextByWait(string xpath, string text)
        {
            wait.Message = "Not found";
            IWebElement elm = wait.Until(x => x.FindElement(By.XPath(xpath)));
            elm.SendKeys(text);
        }

        public int GetElementsCountByXpath(string xpath)
        {
            By elements = By.XPath(xpath);
            return driver.FindElements(elements).Count();
        }

        public void CheckIfElementExistsWithWait(string xpath)  // is M
        {
            wait.Message = "Element cannot be found";
            IWebElement name = wait.Until(x => x.FindElement(By.XPath(xpath)));
        }        

        public bool FindElementExistsWithWait(string xpath)
        {
            wait.Message = "Element cannot be found";
            IWebElement el = wait.Until(x => x.FindElement(By.XPath(xpath)));
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].scrollIntoView(true);", el);
            return true;
        }

        public void CheckElementExistsWithDriverAndXpath(IWebDriver driver, string xpath)
        {
            driver.FindElement(By.XPath(xpath));
        }

        public void HoverMenu(string Xpath)
        {
            By categoryMenu = By.XPath(Xpath);
            Actions action = new Actions(driver);
            action.MoveToElement(driver.FindElement(categoryMenu)).Perform();
        }

        public static void CaptureScreenShot(IWebDriver driver, string fileName)
        {
            var screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();

            if (!Directory.Exists("Screenshots"))
            {
                Directory.CreateDirectory("Screenshots");
            }

            screenshot.SaveAsFile(
                $"Screenshots\\{fileName}.png",
                ScreenshotImageFormat.Png);
        }
    }
}
