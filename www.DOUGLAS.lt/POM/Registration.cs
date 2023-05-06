using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace www.DOUGLAS.lt.POM
{
    internal class Registration
    {
        IWebDriver driver;
        GeneralMethods generalMethods;

        string registrationButton = "//a[@data-action='popup_register']";
        string getEmail = "//input[@id='register_email_input']";
        string passwordXpath = "//div[@class='param styled']//input[@name='password']";
        string password2Xpath = "//div[@class='param styled']//input[@name='password2']";
        string agreeRules = "//input[@id='rules_agree_checkbox']";
        string finallyRegistration = "//button[@class='btn btn-primary']";
        string finallyRegistrationExist = "//img[@class='logged_in']";

        public Registration(IWebDriver driver)
        {
            this.driver = driver;
            generalMethods = new GeneralMethods(driver);
        }

        string[] userdata = System.IO.File.ReadAllLines(@"LoginText.txt");

        public void RegistrationButton()
        {
            generalMethods.ClickByJavaScriptWait(registrationButton);
        }

        public void GetEmail()
        {
            DateTime tim = DateTime.Now;
            string email = "test_" + tim.ToString("yyyy_MM_dd_HH_mm_ss") + "@gmail.com";  
            generalMethods.EnterTextByWait(getEmail, email);
        }

        public void Password()
        {          
            generalMethods.EnterTextByWait(passwordXpath, userdata[1]);
        }

        public void Password2()
        {           
            generalMethods.EnterTextByWait(password2Xpath, userdata[2]);
        }

        public void AgreeRules()
        {
            generalMethods.ClickByJavaScriptWait(agreeRules);
        }

        public void FinallyRegistration()
        {
            generalMethods.ClickByJavaScriptWait(finallyRegistration);
        }

        public void FinallyRegistrationExist()
        {
            generalMethods.CheckIfElementExistsWithWait(finallyRegistrationExist);            
        }
    }
}
