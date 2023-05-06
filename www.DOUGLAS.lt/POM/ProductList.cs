using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace www.DOUGLAS.lt.POM
{
    internal class ProductList
    {
        IWebDriver driver;
        GeneralMethods generalMethods;

        string productsGeneralCategorie = "//div[@class='flex-block']//span[contains(text(),'Visos prekės')]";
        string productsSortingByPrice = "//span[contains(text(),'pagal kainą')]";
        string productElement = "//div[@class='product_element']";
        string checkProductListIntegrity = "//div[@class='sort_info']//span[@class='filter-option pull-left']";
        string pricesXPath = "//span[contains(@class,'now')]";

        public ProductList(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
        }

        public void SelectedProductClick(string productNumber) 
        {
            string productXpath = "(//div[@class='product_element'])[" + productNumber + "]";
            generalMethods.HoverAndCLickWithTry(productXpath);
        }

        public void OpenAllProductsMenu()
        {
            generalMethods.HoverMenu(productsGeneralCategorie);
        }

        public void ProductSortingByPrice()
        {
            generalMethods.ClickByJavaScriptWait(productsSortingByPrice);            
        }

        public bool CheckProductsSortingByPriceBool()
        {
            By pricesText = By.XPath(pricesXPath);
            //driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            Thread.Sleep(2000);  // - be jo neveikia, nes nesuspėja užsikrauti puslapis su kainų rūšiavimu

            List<double> priceList = new List<double>();
            foreach (IWebElement el in driver.FindElements(pricesText))
            {
                string price = el.Text;
                string[] strings = price.Split('€');
                string prices = strings[0];
                double pricesDouble = double.Parse(prices.TrimStart("nuo ".ToCharArray()));
                priceList.Add(pricesDouble);
            }
            for (int i = 0; i < priceList.Count - 1; i++)
            {
                Console.WriteLine(priceList[i]);
                if (priceList[i] > priceList[i + 1])
                {
                    return false;                    
                }
            }
            return true;
        }

        public void CheckProductListIntegrity()  
        {
            By allProducts = By.XPath(productElement);
            int allProductsCount = driver.FindElements(allProducts).Count();

            if (allProductsCount < 1)
            {
                Assert.Fail();
            }

            int paginationCount = -1;
            By paginationXPath = By.XPath(checkProductListIntegrity);
            paginationCount = int.Parse(driver.FindElement(paginationXPath).Text.Split(' ')[2]);

            if (allProductsCount != paginationCount)
            {
                Assert.Fail(allProductsCount + " != " + paginationCount + " Bet gal yra TOP10?");
            }
        }
    }
}
