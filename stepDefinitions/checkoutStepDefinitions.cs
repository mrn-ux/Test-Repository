using NUnit.Framework.Legacy;
using OpenQA.Selenium;
using Reqnroll;
using TestProjectAssignment.Data_Mapper;
using TestProjectAssignment.pages;

namespace TestProjectAssignment.stepDefinitions
{
    [Binding]
    public class CheckoutStepDefinitions
    {

        private readonly IWebDriver driver;
        private readonly ScenarioContext scenarioContext;
        private LoginPage loginPage;
        private InventoryPage inventoryPage;
        private CartPage cartPage;
        private CheckoutCompletePage checkoutCompletePage;
        private CheckoutStepOnePage checkoutStepOnePage;
        private CheckoutStepTwoPage checkoutStepTwoPage;

        public CheckoutStepDefinitions(IWebDriver driver, ScenarioContext scenarioContext)
        {
            this.driver = driver;
            this.scenarioContext = scenarioContext;
            loginPage = new LoginPage(driver);
            inventoryPage = new InventoryPage(driver);
            cartPage = new CartPage(driver);
            checkoutStepOnePage = new CheckoutStepOnePage(driver);
            checkoutStepTwoPage = new CheckoutStepTwoPage(driver);
            checkoutCompletePage = new CheckoutCompletePage(driver);
        }


        [Given(@"User is on the e-commerce platform homepage")]
        public void GivenUserIsOnTheEcommercePlatformHomepage(DataTable table)
        {
            var user = table.CreateInstance<User>();
            loginPage.EnterUserName(user.UserName);
            loginPage.EnterPassword(user.Password);
            loginPage.clickLoginButton();

        }


        [Given(@"User add the required product to the shopping cart")]
        public void GivenUserAddTheItemToShoppingCart(DataTable table)
        {
            var items = table.CreateSet<ItemData>();
            foreach (var item in items)
                inventoryPage.ClickAddToCartButtonInItem(item.ItemName);


        }


        [Given(@"User navigate to the shopping cart page")]
        public void WhenUserNavigatesToShoppingCartPage()
        {
            inventoryPage.ClickShoppingCartButton();

        }


        [When(@"User proceed to checkout")]
        public void WhenUserProceedToCheckOut()
        {
            cartPage.ClickCheckoutButton();

        }


        [When(@"User enter valid checkout information")]
        public void WhenUserEnterValidCheckoutInformation(DataTable table)
        {
            var CheckoutInformation = table.CreateInstance<CheckoutInformation>();

            checkoutStepOnePage.EnterFirstName(CheckoutInformation.FirstName);
            checkoutStepOnePage.EnterLastName(CheckoutInformation.LastName);
            checkoutStepOnePage.EnterZipPostalCode(CheckoutInformation.PostalCode);
            checkoutStepOnePage.ClickContinueButton();

        }


        [Then(@"User should see an order confirmation page")]
        public void ThenUserShouldSeeOrderConfirmationPage()
        {
            Assert.That(checkoutStepTwoPage.IsCheckoutOverviewPageTitleDisplayed(), Is.True, "User is not redirected to order confirmation page");
        }


        [Then(@"Order details and total price should be correct")]
        public void ThenOrderDetailsAndTotalPriceShouldBeCorrect(DataTable table)
        {
            var items = table.CreateSet<ItemData>();
            double totalAmount = 0.0;
            foreach (var item in items)
            {
                Assert.That(checkoutStepTwoPage.IsSelectedItemDisplayedInCheckoutOverview(item.ItemName), Is.True, "Selected item is not displayed in checkout overview page");
                Assert.That(checkoutStepTwoPage.GetQuantityOfItemInCheckOutOverview(item.ItemName), Is.EqualTo(item.Quantity), "Item quantity in checkout overview is incorrect");
                Assert.That(checkoutStepTwoPage.GetPriceOfItem(item.ItemName), Is.EqualTo(item.ItemPrice), "Item price in checkout overview is incorrect");
                totalAmount = totalAmount + item.ItemPrice;
            }
            Assert.That(checkoutStepTwoPage.GetTotalPriceOfItemsWithoutTaxInCheckOutOverview(), Is.EqualTo(totalAmount), "Total Item price without tax in checkout overview is incorrect");
            Assert.That(checkoutStepTwoPage.getTotalPriceOfItemsWithTaxInCheckOutOverview(), Is.EqualTo(totalAmount + checkoutStepTwoPage.getTaxAmountOfItemsInCheckOutOverview()), "Total item price in checkout overview is incorrect");

        }


        [When(@"User finish checkout successfully")]
        public void ThenUserShouldFinishCheckoutSuccessfully()
        {
            checkoutStepTwoPage.ClickFinishButton();
        }


        [Then(@"User should see the success thank you message")]
        public void ThenUserShouldSeeSuccessThankYouMessage()
        {

            Assert.That(checkoutCompletePage.IsCheckoutCompletePageTitleDisplayed(), Is.True, "User is not redirected to check out complete page");
            Assert.That(checkoutCompletePage.GetThankYouMessageInCheckOutCompletion(), Is.EqualTo("Thank you for your order!"), "Thank you message is invalid");
            checkoutCompletePage.ClickBackToHomeButton();

        }




    }
}
