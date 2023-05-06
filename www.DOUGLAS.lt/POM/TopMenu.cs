using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace www.DOUGLAS.lt.POM
{
    internal class TopMenu
    {
        IWebDriver driver;
        GeneralMethods generalMethods;

        string logoImageXpath = "//img[@alt='Douglas']";
        string registrationIcon = "//a[@class='menu_icon need2login']";
        string searchFieldXpath = "//input[@class='form-control sn-suggest-input']";
        string searchButtonXpath = "//i[@class='ico ico-search']";
        string favoritesIcon = "//a[@class='menu_icon my_favorites need2login']";
        string cartIcon = "//a[@class='cart_icon menu_icon']";
        string searchItem = "//div[contains(small,'Nieko neradome pagal')]";

        public TopMenu(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
        }

        public void ClickRegistrationIcon()
        {
            generalMethods.ClickByJavaScriptWait(registrationIcon);
        }

        public void SearchByText(string text)
        {
            generalMethods.EnterTextByWait(searchFieldXpath, text);
        }

        public void ClickSearchButton()
        {
            generalMethods.ClickByJavaScriptWait(searchButtonXpath);           
        }

        public void CheckTopMenuLayout()
        {
            generalMethods.CheckElementExistsWithDriverAndXpath(driver, logoImageXpath);  
            generalMethods.CheckElementExistsWithDriverAndXpath(driver, searchFieldXpath);
            generalMethods.CheckElementExistsWithDriverAndXpath(driver, registrationIcon);
            generalMethods.CheckElementExistsWithDriverAndXpath(driver, favoritesIcon);
            generalMethods.CheckElementExistsWithDriverAndXpath(driver, cartIcon);
        }

        public void ClickCartIcon()
        {
            generalMethods.ClickByJavaScriptWait(cartIcon);
        }     
    }
}
