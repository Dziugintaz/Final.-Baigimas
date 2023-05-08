using System;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Reflection;

namespace Baigiamasis.POM
{
    public class ScreenshotMethods
    {
        IWebDriver driver;

        public ScreenshotMethods(IWebDriver driver)
        {
            this.driver = driver;
        }

        public static void CaptureScreenShot(IWebDriver driver, string fileName)
        {
            var screenshotDriver = driver as ITakesScreenshot;
            Screenshot screenshot = screenshotDriver.GetScreenshot();

            if (!Directory.Exists("Screenshots"))
            {
                Directory.CreateDirectory("Screenshots");
            }

            screenshot.SaveAsFile($"Screenshots\\{fileName}.png", ScreenshotImageFormat.Png);
        }
    }
}