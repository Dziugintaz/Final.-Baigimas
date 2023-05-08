using System;
using Baigiamasis.POM;
using NUnit.Framework;
using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;
using NUnit.Framework.Interfaces;

namespace Baigiamasis.POM
{
    public class OtherCases
    {
        IWebDriver driver;

        [SetUp]
        public void SETUP()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("--disable-notifications");
            driver = new ChromeDriver(options);

            driver.Manage().Window.Maximize();
            driver.Url = "https://www.eurovaistine.lt";

            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.Timeout = TimeSpan.FromSeconds(10);
            wait.PollingInterval = TimeSpan.FromMilliseconds(250);
            wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
            wait.Message = "Cookie accept button was not found";
            IWebElement pressX = wait.Until(x => x.FindElement(By.Id("onetrust-accept-btn-handler")));
            pressX.Click();
            CloseAd();
        }

        [TearDown]

        public void TearDown()
        {

            if (TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                var name =
                    $"{TestContext.CurrentContext.Test.MethodName}" +
                    $" Error at " +
                    $"{DateTime.Now.ToString().Replace(":", "_")}";

                ScreenshotMethods.CaptureScreenShot(driver, name);

                File.WriteAllText(
                    $"Screenshots\\{name}.txt",
                    TestContext.CurrentContext.Result.Message);
            }

           driver.Close();
           driver.Quit();
        }

        public void CloseAd()
        {
            IWebElement Explisitwait(string xpath)
            {
                WebDriverWait wait = new WebDriverWait(driver, new TimeSpan(0, 0, 20));
                wait.Until(x => x.FindElement(By.XPath(xpath)).Displayed);
                return
                driver.FindElement(By.XPath(xpath));
            }
            try
            {
                IWebElement closeAd = Explisitwait("//button[contains(@class, 'PopupCloseButton__Inner')]");
                closeAd.Click();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        [Test]
        public void LogIn()
        {

            LogIn login = new LogIn(driver);
            login.EnterUsername("testp00@yahoo.com");
            login.EnterPassword("Happy123");
            login.PressLogin();
        }

        [Test]
        public void Cart()
        {
            Cart cart = new Cart(driver);
            cart.NavigationFromMainPage("Kosmetika", "Formavimas");
            cart.NavigationToFirstProduct();
            cart.pressPlus();
            cart.AddToCart();
            cart.CountinBasket();
        }


        [Test]
        public void Search()
        {
            Search search = new Search(driver);
            search.SearchBox("gripas");
        }

        [Test]

        public void Sorting()
        {
            Sorting sorting = new Sorting(driver);
            sorting.NavigationToSorting("Sportas ir ortopedija", "Gertuvės ir šiaudeliai");
            sorting.pressSorting();
            sorting.CheckPriceSortingFromSmallest();

        }
    }   
}









