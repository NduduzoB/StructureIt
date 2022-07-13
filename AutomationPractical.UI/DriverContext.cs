using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace AutomationPractical.UI
{
    public class DriverContext
    {
       

        public IWebDriver CreateDriverInstance(BrowserType browserType)
        {
            IWebDriver driver;
            var driverManager = new DriverManager();
            switch (browserType)
            {
                case BrowserType.Chrome:
                    driverManager.SetUpDriver(new ChromeConfig());
                    driver = new ChromeDriver();
                    break;

                case BrowserType.Firefox:
                    driverManager.SetUpDriver(new FirefoxConfig());
                    driver = new FirefoxDriver();
                    break;

                case BrowserType.Edge:
                    driverManager.SetUpDriver(new EdgeConfig());
                    driver = new EdgeDriver();
                    break;

                default:
                    driverManager.SetUpDriver(new EdgeConfig());
                    driver = new EdgeDriver();
                    break;
            }
            driver.Manage().Window.Maximize();
            return driver;
        }
    }
}