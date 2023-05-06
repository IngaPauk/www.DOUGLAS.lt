using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace www.DOUGLAS.lt.POM
{
    internal class ProductCart
    {
        IWebDriver driver;
        GeneralMethods generalMethods;
        TopMenu topMenu;

        string breadCrumbXpath = "//div[@class='breadcrumb hidden-xs hidden-sm']";
        string breadCrumbCount = "//span[@class='separator']";

        string buttonToCart = "//div[@class='col col-md-12']//button[contains(@class,'add_to_cart btn btn-primary')]";

        string productDescription = "//div[@class='tabs']";
        string productArea_RinkisKartu = "//div[@class='swogo-box']";
        string favoritesProductIcon = "//i[@class='ico ico-heart']";

        string productPrice = "(//div[@class='price']//span)[1]";
        string checkCartSum = "//div[@class='grand_total']//span";

        string noProduct = "//div[@class='alert alert-danger']";
        
        public ProductCart(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
            topMenu = new TopMenu(driver);
        }

        public bool CheckBreadCrumbExists()
        {
            return generalMethods.FindElementExistsWithWait(breadCrumbXpath);
        }

        public void CheckBreadCrumbsCount()
        {
            int breadCrumbsNumber = 0;  
            int countOfBreadcrumbs = generalMethods.GetElementsCountByXpath(breadCrumbCount);            
            if (breadCrumbsNumber == 4)
            {
            Assert.AreEqual(breadCrumbsNumber, countOfBreadcrumbs, "Expected 4 breadcrumbs, but got : " + countOfBreadcrumbs);
            }
            if (breadCrumbsNumber == 5)
            {
                Assert.AreEqual(breadCrumbsNumber, countOfBreadcrumbs, "Expected 5 breadcrumbs, but got : " + countOfBreadcrumbs);
            }
            Console.WriteLine("Breadcrumbs: " + countOfBreadcrumbs);
        }

        public void CheckOrExistProductItems()
        {
            generalMethods.CheckIfElementExistsWithWait(buttonToCart);              // mygtukas i krepseli
            generalMethods.CheckIfElementExistsWithWait(favoritesProductIcon);      // sirdeles icon
            generalMethods.CheckIfElementExistsWithWait(productDescription);        // prekes aprasymas 
            generalMethods.CheckIfElementExistsWithWait(productArea_RinkisKartu);   // "Rinkis kartu"
        }

        public void CklickButtonToCart()
        {
            generalMethods.ClickByJavaScriptWait(buttonToCart);
        }

        public string ProductPrice()   
        {
            By productPricee = By.XPath(productPrice);
            IWebElement el = driver.FindElement(productPricee);
            string price = el.Text;
            Console.WriteLine(price);
            string[] strings = price.Split('€');
            string prices = strings[0];
            return prices;
        }

        public void NoProduct()
        {
            generalMethods.CheckIfElementExistsWithWait(noProduct);
        }
        
        public string CheckCartSum()
        {
            generalMethods.CheckIfElementExistsWithWait(checkCartSum);         
            
            By productPriceInCart = By.XPath(checkCartSum);
            IWebElement el = driver.FindElement(productPriceInCart);
            string onePriceInCart = el.Text.Substring(0, el.Text.Length - 1);
            Console.WriteLine(onePriceInCart);
            return onePriceInCart;
        }
    }
}
