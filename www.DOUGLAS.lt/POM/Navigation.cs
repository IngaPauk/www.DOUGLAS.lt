using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace www.DOUGLAS.lt.POM
{
    internal class Navigation
    {
        IWebDriver driver;
        GeneralMethods generalMethods;

        public Navigation(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
        }

        public string Get1stCategoryXpath(string firstCategorieName)
        {
            return "//img[@alt='" + firstCategorieName + "']";   
        }

        public string Get2ndCategoryXpath(string secondCategorieName)
        {
            return "//a[contains(text(),'" + secondCategorieName + "')]";            
        }

        public string Get3rdCategoryXpath(string thirdCategorieName)
        {
            return "//a[contains(text(),'" + thirdCategorieName + "')]";            
        }

        public void NavigateToProductList(string firstCategorie, string secondCategorie, string product)
        {
            generalMethods.ClickByJavaScript(Get1stCategoryXpath(firstCategorie));
            generalMethods.ClickByJavaScript(Get2ndCategoryXpath(secondCategorie));
            generalMethods.ClickByJavaScript(Get3rdCategoryXpath(product));
        }

        public string NavigateToTitle() 
        {
            By titleName = By.XPath("//h1[contains(@class,'page_title')]");
            return driver.FindElement(titleName).Text;
        }

        public void NavigateFromMainPage(string parent, string child)
        {
            generalMethods.HoverMenu("//img[@alt='" + parent + "']");
            generalMethods.ClickByJavaScriptWait("//a[normalize-space()='" + child + "']");
        }
    }
}
