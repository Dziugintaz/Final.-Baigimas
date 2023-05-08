using System;
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
using System.Xml.Linq;

namespace Baigiamasis.POM
{
    public class Cart
    {
        IWebDriver driver;
     
        By firstItemCard = By.XPath("//div[contains(@class, 'mainInfo')][1]");
        By firstItem = By.XPath("(//img[contains(@class,'lazyloaded')][1])");
        By plusButton = By.XPath("//div[@class='col-md-10']//div[@class='product-increment-icon']");
        By submitButton = By.XPath("(//div[@class='col-md-10']//button[@type='submit'][contains(text(),'Į krepšelį')])");
        By basketIcon = By.XPath("//div[@class='headerCart-amountWrapper']//*[name()='svg']");
        By countInBasket = By.XPath("//div[contains(@class, 'headerCart-amount ')]");
       


        public Cart(IWebDriver driver)

        {
            this.driver = driver;
        }
        public void NavigationFromMainPage(string parent, string child)
        {
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.Timeout = TimeSpan.FromSeconds(20);

            By ParentCategory = By.XPath("//a[contains(text(),'" + parent + "')]");
            Actions action = new Actions(driver);
            IWebElement ParentCatObj = driver.FindElement(ParentCategory);

            action.MoveToElement(ParentCatObj).Perform();

            By innerCategory = By.XPath("//span[contains(text(),'" + child + "')]//parent::a");
            driver.FindElement(innerCategory).Click();

        }
        public void NavigationToFirstProduct()
        {
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.Timeout = TimeSpan.FromSeconds(20);

            //scrolinam iki pirmo produkto
            IWebElement firstproductinfo = wait.Until(x => x.FindElement(firstItemCard));
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].scrollIntoView(true);", firstproductinfo);

            wait.Timeout = TimeSpan.FromSeconds(10);

            //paspaudziam ant pirmo produkto
            IWebElement firstProduct = wait.Until(x => x.FindElement(firstItem));
            firstProduct.Click();
         

        }
        public void pressPlus()
        {
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.Timeout = TimeSpan.FromSeconds(20);

            //paspaudziam ant pliusuko
            IWebElement pressplus = wait.Until(x => x.FindElement(plusButton));
            pressplus.Click();
        }

        public void AddToCart()
        {
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.Timeout = TimeSpan.FromSeconds(20);

            //dedam i krepseli
            IWebElement addCart = wait.Until(x => x.FindElement(submitButton));
            addCart.Click();
        }

        public void CountinBasket()
        {
             DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
             wait.Timeout = TimeSpan.FromSeconds(20);

            //palyginam ar tikrai paspaudus pliusa isidejo 2 prekes i krepseli
            string count = wait.Until(x => x.FindElement(countInBasket)).Text;
            Assert.AreEqual("2", count);

        }


        
    }

}