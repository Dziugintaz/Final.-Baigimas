using NUnit.Framework;
using Baigiamasis.POM;
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
using System.Reflection.Metadata;
using System.IO;
using OpenQA.Selenium.DevTools;
using System.Security.Claims;

namespace Baigiamasis.POM
{
    public class Sorting
    {
        IWebDriver driver;
        private string itemNameXpath = "//div[@class='title']";
        By itemCard = By.XPath("//div[@class = 'productCardContent']");
        By discountPrice = (By.XPath(".//div[contains(@class,'productPrice')]/div"));
        By price = (By.XPath(".//div[contains(@class,'productPrice')]"));
        By priceInCart = By.XPath("//div[@class = 'cartUnitPrice']");
        By Sortingbutton = By.XPath("//div[normalize-space()='Rikiavimas']");
        By SelectFromSmallest = By.XPath("//div[@id='filter_list_Pigiausios viršuje']//div[1]");

        public Sorting(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void NavigationToSorting(string parent, string child)
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

        public void pressSorting()
        {
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.Timeout = TimeSpan.FromSeconds(30);

            //paspaudziam ant rusiavimo
            IWebElement pressSorting = wait.Until(x => x.FindElement(Sortingbutton));
            pressSorting.Click();

            //paspaudziam nuo maziausios
            IWebElement dropListSorting = wait.Until(x => x.FindElement(SelectFromSmallest));
            dropListSorting.Click();
        }

        public void CheckPriceSortingFromSmallest()
        {
            List<double> prices = new List<double>();

            foreach (IWebElement card in driver.FindElements(itemCard))
            {

                if (card.FindElements(discountPrice).Count > 0)

                {
                    //su discountu
                    IWebElement discountPriceElement = card.FindElements(discountPrice)[1];
                    string onePrice = discountPriceElement.Text;
                    double priceDouble = Double.Parse(onePrice.Replace(" €", "").Replace(".", ","));
                    prices.Add(priceDouble);
                }
                else
                {
                    //be discounto
                    string onePrice = card.FindElement(price).Text;
                    double priceDouble = Double.Parse(onePrice.Replace(" €", "").Replace(".", ","));
                    prices.Add(priceDouble);
                }
            }

            for (int i = 0; i < prices.Count - 1; i++)
            {
                Console.WriteLine(prices[i]);
                if (prices[i] > prices[i + 1])
                {
                    Assert.Fail("Price sorting from smallest not working");
                }
            }

            Console.WriteLine("Product Prices:");
            foreach (decimal price in prices)
            {
                Console.WriteLine(price);
            }

        }

    }
}

