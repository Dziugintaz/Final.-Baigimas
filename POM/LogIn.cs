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

namespace Baigiamasis
{
	public class LogIn
	{
		 IWebDriver driver;

		public LogIn(IWebDriver driver)
		{
			this.driver = driver;
            DefaultWait<IWebDriver> wait = new DefaultWait<IWebDriver>(driver);
            wait.Timeout = TimeSpan.FromSeconds(10);

            //paspaudziam "prisijungti"
            IWebElement presslogin = wait.Until(x => x.FindElement(By.XPath("//span[contains(@class, 'headerUserName')]")));
            presslogin.Click();
        }
		public void EnterUsername(string username)
		{
			driver.FindElement(By.Id("_username")).SendKeys(username);
		}
        public void EnterPassword(string password)
        {
            driver.FindElement(By.Id("_password")).SendKeys(password);
        }
        public void PressLogin()
        {
			driver.FindElement(By.Id("authLoginButton")).Click();
        }
    }
}

