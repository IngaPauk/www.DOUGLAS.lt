using NUnit.Framework.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using www.DOUGLAS.lt.POM;
using System.Windows.Forms;
using System.Threading;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;
using OpenQA.Selenium.Internal;

namespace www.DOUGLAS.lt.Tests
{
    internal class OtherCases
    {
        static IWebDriver driver;

        GeneralMethods generalMethods;
        Registration registration;
        Navigation navigation;
        ProductCart productCart;
        ProductList productList;
        TopMenu topMenu;

        [SetUp]
        public void SETUP()
        {
            driver = GeneralMethods.CreateDriverWithoutNotification();

            generalMethods = new GeneralMethods(driver);
            topMenu = new TopMenu(driver);
            navigation = new Navigation(driver);
            registration = new Registration(driver);
            productList = new ProductList(driver);
            productCart = new ProductCart(driver);

            driver.Manage().Window.Maximize();
            driver.Url = "https://www.douglas.lt";

            //cookiai
            string cookies = "//div[contains(@class,'pull-right')]";
            generalMethods.WaitElement(cookies, driver).Click();
        }

        [TearDown]
        public static void TearDown() 
        {
            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var name =
                    $"{TestContext.CurrentContext.Test.MethodName}" +
                    $" Error at " +
                    $"{DateTime.Now.ToString().Replace(":", "_")}";

                GeneralMethods.CaptureScreenShot(driver, name);

                File.WriteAllText(
                    $"Screenshots\\{name}.txt",
                    TestContext.CurrentContext.Result.Message);
            }
            //driver.Close();
            //driver.Quit(); 
        }

        [Test]
        public void RegistrationTest()
        {
            topMenu.ClickRegistrationIcon();
            registration.RegistrationButton();
            registration.GetEmail();
            registration.Password();
            registration.Password2();
            registration.AgreeRules();
            registration.FinallyRegistration();
            registration.FinallyRegistrationExist();
        }

        [Test]
        public void CheckIfPricesAreSorting()   
        {
            //galite pasirinkti vieną grupę iš trijų siūlomų

            //string firstCategorieProduct = "Makiažui";  //-- yra nuo
            //string secondCategorieProduct = "Priemonės akims";
            //string thirdCategorieProduct = "Tušai";

            string firstCategorieProduct = "Plaukams"; //-- yra nuo
            string secondCategorieProduct = "Vyrams";
            string thirdCategorieProduct = "Formavimas";

            //string firstCategorieProduct = "Namams ir stiliui";
            //string secondCategorieProduct = "Automobiliui";
            //string thirdCategorieProduct = "Automobilio kvapai";

            navigation.NavigateToProductList(firstCategorieProduct, secondCategorieProduct, thirdCategorieProduct);
            productList.ProductSortingByPrice();
            Assert.IsTrue(productList.CheckProductsSortingByPriceBool(), "Products prices aren't in order");
        }

        [Test]
        public void CheckOrProductPricesAreSame() 
        {
            // ne visą laiką suveikia iš pirmo karto
            // kodas nėra universalus, nes kai kurios prekės
            // turi tūrio kelias opcijas
            // pvz. 30, 50, 90 ml dar yra ir 40, 60 ml

            //pasirinkite kurį žodį norite įvesti į paieškos lauką
            string searchText = "Gucci";           // - 2 prekės ne ta select
            //string searchText = "SAPIENS";       // - šiuo metu nėra 10 prekės
            //string searchText = "madara";        // - šiuo metu nėra 5 prekės

            topMenu.SearchByText(searchText);
            topMenu.ClickSearchButton();                                     

            //galite pasirinkti produkto eilės numerį sąraše (nuo 1 iki 40)
            string selectProductNumberFrom1To40 = "4";   
            productList.SelectedProductClick(selectProductNumberFrom1To40);

            productCart.CheckOrExistProductItems(); 

            productCart.CklickButtonToCart();
            string productPrice = productCart.ProductPrice();

            topMenu.ClickCartIcon();
            string cartSum = productCart.CheckCartSum();
            try
            {
                productCart.NoProduct();
                Console.WriteLine("Šiuo metu neturime šios prekės");
            }
            catch
            {
                Assert.AreEqual(productPrice, cartSum, "Products priceses aren't same");
            }
        }


        [Test]
        public void CheckBreadCrumbsCount() 
        {
            //pasirinkite kurį žodį norite įvesti į paieškos lauką
            //string searchText = "Gucci";       // - turi 5 
            string searchText = "SAPIENS";    // - turi 4
            //string searchText = "madara";     // - turi 5

            topMenu.SearchByText(searchText);
            topMenu.ClickSearchButton();

            //galite pasirinkti produkto eilės numerį sąraše (nuo 1 iki 40)
            string selectProductNumberFrom1To40 = "5";
            productList.SelectedProductClick(selectProductNumberFrom1To40);

            Assert.IsTrue(productCart.CheckBreadCrumbExists());
            productCart.CheckBreadCrumbsCount();
        }

        [Test]
        public void NavigationTest()
        {
            topMenu.CheckTopMenuLayout();
            productList.OpenAllProductsMenu();

            //galite pasirinkti vieną iš jums siūlomų ketegorijų
            navigation.NavigateFromMainPage("Odai", "Veidui");      
            //navigation.NavigateFromMainPage("Plaukams", "Vaikams"); 
            //navigation.NavigateFromMainPage("Kvepalai", "Moterims");  // - atsiranda 50!=40 Bet gal yra TOP10?

            productList.CheckProductListIntegrity();
            Assert.IsTrue(productCart.CheckBreadCrumbExists());
        }

        [Test]
        public void OrProductsTitlesNamesAreSame()  
        {
            //galite pasirinkti vieną grupę iš trijų siūlomų

            string firstCategorieProduct = "Makiažui";
            string secondCategorieProduct = "Priemonės akims";
            string thirdCategorieProduct = "Tušai";

            //string firstCategorieProduct = "Kvepalai";
            //string secondCategorieProduct = "Moterims";
            //string thirdCategorieProduct = "Rinkiniai moterims";

            //string firstCategorieProduct = "Plaukams";
            //string secondCategorieProduct = "Vyrams";
            //string thirdCategorieProduct = "Formavimas";

            navigation.NavigateToProductList(firstCategorieProduct, secondCategorieProduct, thirdCategorieProduct);

            //antraščių pavadinimų lyginimas su įvestu žodžiu (P.S. daug kur antraštės nesutampa)
            string actualTitle2 = navigation.NavigateToTitle(); 
            Console.WriteLine(actualTitle2);
            Console.WriteLine(thirdCategorieProduct);
            Assert.AreEqual(actualTitle2, thirdCategorieProduct, "Products names aren't same");
        }
    }
}
