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
    public class Search
    {
        IWebDriver driver;

        By SearchBoxField = By.XPath("//input[contains(@class, 'sn-suggest-input headerSearchInput tt-input')]");
        By SearchBoxButton = By.XPath("//button[contains(@class, 'headerSearchButton')]");
        By SearchInput = By.XPath("//h1[contains(text(),'Paieškos rezultatai ieškant \"gripas\"')]");

        public Search(IWebDriver driver)
        {
            this.driver = driver;
        }
        public void SearchBox(string searchword)
        {
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.Timeout = TimeSpan.FromSeconds(20);

            IWebElement searchBox = wait.Until(x => x.FindElement(SearchBoxField));
            searchBox.SendKeys(searchword);


            IWebElement searchButton = wait.Until(x => x.FindElement(SearchBoxButton));
            searchButton.Click();


            IWebElement searchResults = wait.Until(x => x.FindElement(SearchInput));

            if (searchResults.Displayed)
            {
                Console.WriteLine("Search results are displayed");
            }
            else
            {
                Console.WriteLine("Search results are not displayed");
            }
        }

    }
}
            

         


      
    


