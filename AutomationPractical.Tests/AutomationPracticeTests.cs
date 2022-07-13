using AutomationPractical.UI;
using AutomationPractical.UI.Models;
using AutomationPractical.UI.PageObjects;
using AutomationPractical.UI.Util;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;

namespace AutomationPractical.Tests
{
    [TestFixture]
    [TestFixture(BrowserType.Chrome)]
    public class AutomationPracticeTests : TestBase
    {
        private HomePageObject _homePageObject;

        public AutomationPracticeTests(BrowserType browserType) : base(BrowserType)
        {
            BrowserType = browserType;
        }

        [SetUp]
        public void Init()
        {
            _homePageObject = new HomePageObject(Driver);
        }

        [Test]
        public void SingleSearch_VerifySearchedItemIsInTheSearchResults()
        {
            const string searchCriteria = Constants.Blouse;
            _homePageObject.NavigateToPage();
            _homePageObject.SearchFor(searchCriteria);
            _homePageObject.IsSearchResultItemAMatch(searchCriteria).ShouldBeTrue();
            _homePageObject.GetSearchResultsHeadingCounter().ShouldBe(1);
        }

        [Test]
        public void LoopSearch_VerifySearchedItemIsInTheSearchResults()
        {
            _homePageObject.NavigateToPage();
            var searchCriteria = new Dictionary<string, int>
            {
                {Constants.PrintedDress, 5},
                {Constants.Blouse, 1},
                {Constants.FadedShortSleeveTShirts, 1}
            }.ToList();

            searchCriteria.ForEach(searchCriteriaItem =>
            {
                _homePageObject.SearchFor(searchCriteriaItem.Key);
                _homePageObject.IsSearchResultItemAMatch(searchCriteriaItem.Key).ShouldBeTrue();
                _homePageObject.GetSearchResultsHeadingCounter().ShouldBe(searchCriteriaItem.Value);
            });
        }

        [TestCase(Constants.PrintedDress, 5)]
        [TestCase(Constants.Blouse, 1)]
        [TestCase(Constants.FadedShortSleeveTShirts, 1)]
        public void LoopSearch_VerifySearchedItemIsInTheSearchResults(string searchCriteria, int expectedSearchResultHeadingCount)
        {
            _homePageObject.NavigateToPage();
            _homePageObject.SearchFor(searchCriteria);
            _homePageObject.IsSearchResultItemAMatch(searchCriteria).ShouldBeTrue();
            _homePageObject.GetSearchResultsHeadingCounter().ShouldBe(expectedSearchResultHeadingCount);
        }

        [Test]
        public void TestDataLoopSearch_VerifySearchedItemIsInTheSearchResults()
        {
            _homePageObject.NavigateToPage();
            var searchCriteria = TestDataReader.GetTestData();

            searchCriteria.ForEach(searchCriteriaItem =>
            {
                _homePageObject.SearchFor(searchCriteriaItem.ItemName);
                _homePageObject.IsSearchResultItemAMatch(searchCriteriaItem.ItemName).ShouldBeTrue();
                _homePageObject.GetSearchResultsHeadingCounter().ShouldBe(searchCriteriaItem.Quantity);
            });
        }

        [Test]
        public void Credentials_SuccessfulSignIn()
        {
            _homePageObject.NavigateToPage();
            _homePageObject.SignIn(Credentials.Email, Credentials.Password);
            var myAccountPageObject = new MyAccountPageObject(Driver);
            myAccountPageObject.IsCustomerNameVisible("Nduduzo Bukhosini").ShouldBeTrue();
            myAccountPageObject.IsPageHeadingVisible().ShouldBeTrue();
        }

        [Test]
        public void Cart_VerifyDisplayedTotalMatchesCalculatedTotal()
        {
            const string searchCriteria = Constants.Blouse;
            _homePageObject.NavigateToPage();
            _homePageObject.SearchFor(searchCriteria);
            _homePageObject.AddItemToCart();
            _homePageObject.ProceedToCheckoutButton();
            _homePageObject.SetQuantity(2);
            var expectedTotal = _homePageObject.GetTotal();
            Calculator.CalculateTotal(_homePageObject.GetUnitPrice(), _homePageObject.GetQuantity()).ShouldBe(expectedTotal);
        }

        [TestCase(Constants.Tops)]
        [TestCase(Constants.CasualDresses)]
        [TestCase(Constants.EveningDresses)]
        [TestCase(Constants.SummerDresses)]
        [TestCase(Constants.TShirts)]
        public void NavigationMenu_VerifyCanNavigateToSubCategoryPage(string expectedCategory)
        {
            _homePageObject.NavigateToPage();
            _homePageObject.GoToCategory(expectedCategory);
            _homePageObject.GetCategoryName().ShouldBe(expectedCategory);
            _homePageObject.GoToCategory(expectedCategory);
            _homePageObject.GetCategoryName().ShouldBe(expectedCategory);
            _homePageObject.GoToCategory(expectedCategory);
            _homePageObject.GetCategoryName().ShouldBe(expectedCategory);
        }

      
    }
}