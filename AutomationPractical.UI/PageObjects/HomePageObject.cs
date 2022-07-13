using AutomationPractical.UI.Util;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Text.RegularExpressions;
using System.Threading;

namespace AutomationPractical.UI.PageObjects
{
    public class HomePageObject
    {
        private readonly IWebDriver _driver;
        private const string HomePageUrl = "http://automationpractice.com/index.php";
        private readonly By _signInLink = By.XPath("//a[normalize-space()='Sign in']");
        private readonly By _signInButton = By.Id("SubmitLogin");
        private readonly By _email = By.Id("email");
        private readonly By _password = By.Id("passwd");
        private readonly By _search = By.Id("search_query_top");
        private readonly By _searchButton = By.Name("submit_search");
        private readonly By _headingCounter = By.ClassName("heading-counter");
        private readonly By _proceedToCheckoutButton = By.XPath("//span[normalize-space()='Proceed to checkout']");
        private readonly By _addToCartButton = By.XPath("//span[normalize-space()='Add to cart']");
        private readonly By _cartQuantity = By.XPath("(//input[@name='quantity_2_7_0_0'])");
        private readonly By _unitPrice = By.Id("product_price_2_7_0");
        private readonly By _cartTotal = By.Id("total_product_price_2_7_0");
        private readonly By _tshirtsNavigationMenu = By.XPath("(//a[contains(text(),'T-shirts')])[2]");
        private readonly By _eveningDressesNavigationMenu = By.XPath("(//a[contains(text(),'Evening Dresses')])[2]");
        private readonly By _summerDressesNavigationMenu = By.XPath("(//a[contains(text(),'Summer Dresses')])[2]");
        private readonly By _casualDressesNavigationMenu = By.XPath("(//a[contains(text(),'Casual Dresses')])[2]");
        private readonly By _topsNavigationMenu = By.XPath("//a[contains(text(),'Tops')]");
        private readonly By _categoryName = By.ClassName("category-name");
        private readonly WebDriverWait _wait;
        private string _userEmail;
        private string _userPassword;
        private string _searchCriteriaItem;

        public HomePageObject(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
            _wait.IgnoreExceptionTypes(typeof(NoSuchElementException));
        }

        public void NavigateToPage()
        {
            _driver.Navigate().GoToUrl(new Uri(HomePageUrl));
        }

        public void SignIn(string email, string password)
        {
            _userEmail = email;
            _userPassword = password;
            ClickSignInLink();
            SetEmail();
            SetPassword();
            ClickSignInButton();
        }

        private void SetEmail()
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(_email)).SendKeys(_userEmail);
        }

        private void ClickSignInLink()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(_signInLink)).Click();
        }

        private void ClickSignInButton()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(_signInButton)).Click();
        }

        private void SetPassword()
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(_password)).SendKeys(_userPassword);
        }

        public void SearchFor(string searchItem)
        {
            _searchCriteriaItem = searchItem;
            var searchElement = _driver.FindElement(_search);
            searchElement.Clear();
            searchElement.SendKeys(searchItem);
            _wait.Until(ExpectedConditions.ElementToBeClickable(_searchButton)).Click();
        }

        public bool IsSearchResultItemAMatch(string searchItem)
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//img[@title='{searchItem}']"))).Displayed;
        }

        public int GetSearchResultsHeadingCounter()
        {
            return int.Parse(Regex.Match(_wait.Until(ExpectedConditions.ElementIsVisible(_headingCounter)).Text, @"\d+").Value);
        }

        public void AddItemToCart()
        {
            var searchResultItem = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath($"//img[@title='{_searchCriteriaItem}']")));
            var actions = new Actions(_driver);
            actions.MoveToElement(searchResultItem).Perform();
            ClickAddToCartButton();
        }

        private void ClickAddToCartButton()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(_addToCartButton)).Click();
        }

        public void ProceedToCheckoutButton()
        {
            _wait.Until(ExpectedConditions.ElementToBeClickable(_proceedToCheckoutButton)).Click();
        }

        public void SetQuantity(int quantity)
        {
            _wait.Until(ExpectedConditions.ElementIsVisible(_cartQuantity)).SendKeys(quantity.ToString());
            _wait.Until(ExpectedConditions.ElementIsVisible(_cartQuantity)).SendKeys(Keys.Enter);
        }

        public long GetQuantity()
        {
            return long.Parse(_wait.Until(ExpectedConditions.ElementIsVisible(_cartQuantity)).GetAttribute("value"));
        }

        public double GetUnitPrice()
        {
            return double.Parse(_wait.Until(ExpectedConditions.ElementIsVisible(_unitPrice)).Text.Remove(0, 1));
        }

        public double GetTotal()
        {
            Thread.Sleep(TimeSpan.FromSeconds(5));
            var total = _wait.Until(ExpectedConditions.ElementIsVisible(_cartTotal)).Text.Remove(0, 1);
            return double.Parse(total);
        }

        public void GoToCategory(string category)
        {
            var xpathToFind = XPathFinder.FindXPath(category);
            var navigationMenu = _wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xpathToFind)));
            var actions = new Actions(_driver);
            actions.MoveToElement(navigationMenu).Perform();
            ClickCategoryMenu(category);
        }

        public void ClickCategoryMenu(string menu)
        {
            switch (menu)
            {
                case Constants.Tops:
                    _wait.Until(ExpectedConditions.ElementIsVisible(_topsNavigationMenu)).Click();
                    break;

                case Constants.CasualDresses:
                    _wait.Until(ExpectedConditions.ElementIsVisible(_casualDressesNavigationMenu)).Click();
                    break;

                case Constants.EveningDresses:
                    _wait.Until(ExpectedConditions.ElementIsVisible(_eveningDressesNavigationMenu)).Click();
                    break;

                case Constants.SummerDresses:
                    _wait.Until(ExpectedConditions.ElementIsVisible(_summerDressesNavigationMenu)).Click();
                    break;

                default:
                    _wait.Until(ExpectedConditions.ElementIsVisible(_tshirtsNavigationMenu)).Click();
                    break;
            }
        }

        public string GetCategoryName()
        {
            return _wait.Until(ExpectedConditions.ElementIsVisible(_categoryName)).Text;
        }
    }
}