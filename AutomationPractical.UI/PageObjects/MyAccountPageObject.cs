using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;

namespace AutomationPractical.UI.PageObjects
{
    public class MyAccountPageObject
    {
        private readonly By _signOut = By.XPath("//a[@title='Log me out']");
        private readonly By _pageHeading = By.XPath("//h1[normalize-space()='My account']");
        private readonly WebDriverWait _wait;

        public MyAccountPageObject(IWebDriver driver)
        {
            _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            _wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }

        public bool IsPageHeadingVisible()
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(_pageHeading)).Displayed;
        }

        public bool IsCustomerNameVisible(string customerName)
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//span[normalize-space()='{customerName}']"))).Displayed;
        }

        public void SignOut()
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(_signOut)).Click();
        }
    }
}