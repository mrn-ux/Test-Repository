using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProjectAssignment.pages
{
    public class CartPage
    {

        private readonly IWebDriver _driver;
        private readonly WebDriverWait _webDriverWait;
        private readonly ILogger _logger;

        //Locators

        private readonly By _cartPageTitle = By.XPath("//div[@data-test='secondary-header']/span[contains(text(),'Your Cart')]");
        private readonly By _itemListInCart = By.XPath("//div[@data-test='cart-list']");
        private readonly By _allCartItems = By.XPath("./child::*");
        private readonly By _cartItemLink = By.XPath("./div[@data-test='inventory-item']/following::a");
        private readonly By _lblQuantity = By.XPath("./div[@data-test='item-quantity']");
        private readonly By _removeFromCartBtn = By.XPath("./div[@data-test='inventory-item']//button[contains(@id,'remove-')]");
        private readonly By _checkoutBtn = By.Id("checkout");
        private readonly By _continueShoppingBtn = By.Id("continue-shopping");


        public CartPage(IWebDriver driver)
        {
            _driver = driver;
            _webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = factory.CreateLogger<CartPage>();
        }

        public bool isCartPageTitleDisplayed()
        {
            bool value = false;
            try
            {
                IWebElement cardPageTitleElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_cartPageTitle));
                value = _driver.FindElement(_cartPageTitle).Displayed;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting cart page title: {ex.StackTrace}");
            }
            return value;
        }

        public bool IsSelectedItemDisplayedInCart(String itemName)
        {
            bool value = false;
            try
            {
                IWebElement itemListElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_itemListInCart));
                IList<IWebElement> allChildItems = itemListElement.FindElements(_allCartItems);
                for (int i=2; i<allChildItems.Count; i++ )
                {
                    IWebElement itemLink = allChildItems[i].FindElement(_cartItemLink);
                    if (itemLink.Text.Equals(itemName))
                        value = true;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while checking if selected item is displayed in cart: {ex.StackTrace}");
                return value;
            }
            return value;
        }

        public void ClickRemoveButtonInItem(String itemName)
        {
            try
            {
                IWebElement itemListElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_allCartItems));
                IList<IWebElement> allChildItems = itemListElement.FindElements(_allCartItems);
                foreach (IWebElement item in allChildItems)
                {
                    IWebElement itemLink = item.FindElement(_cartItemLink);
                    IWebElement removeFromCartBtn;
                    if (itemLink.Text.Equals(itemName))
                        removeFromCartBtn = item.FindElement(_removeFromCartBtn);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while clicking remove from cart button: {ex.StackTrace}");

            }
        }

        public void ClickCheckoutButton()
        {
            try
            {
                IWebElement checkoutBtnElement = _webDriverWait.Until(ExpectedConditions.ElementToBeClickable(_checkoutBtn));
                checkoutBtnElement.Click();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while clicking checkout button: {ex.StackTrace}");
            }

        }

        public void ClickContinueShoppingButton()
        {
            try
            {
                IWebElement checkoutBtnElement = _webDriverWait.Until(ExpectedConditions.ElementToBeClickable(_continueShoppingBtn));
                checkoutBtnElement.Click();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while clicking continue shopping button: {ex.StackTrace}");
            }

        }

        public String GetQuantityOfItem(String itemName)
        {
            String quantity = "";
            try
            {
                IWebElement itemListElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_itemListInCart));
                IList<IWebElement> allChildItems = itemListElement.FindElements(_allCartItems);
                foreach (IWebElement item in allChildItems)
                {
                    IWebElement itemLink = item.FindElement(_cartItemLink);
                    if (itemLink.Text.Equals(itemName))
                    {
                        IWebElement quantityLabel = item.FindElement(_lblQuantity);
                        quantity = quantityLabel.Text;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting quantity of item: {ex.StackTrace}");
            }
            return quantity;
        }

    }
}
