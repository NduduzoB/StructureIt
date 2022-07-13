using AutomationPractical.UI;
using NUnit.Framework;
using OpenQA.Selenium;

namespace AutomationPractical.Tests
{
    [TestFixture]
    public abstract class TestBase
    {
        protected static BrowserType BrowserType;
        protected static IWebDriver Driver;

        protected TestBase(BrowserType browserType)
        {
            BrowserType = browserType;
        }

        [OneTimeSetUp]
        public static void Initialize()
        {
            Driver = new DriverContext().CreateDriverInstance(BrowserType);
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            Driver.Quit();
        }
    }
}