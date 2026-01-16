using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Text;


namespace TestProjectAssignment.pages
{
    public class CheckoutStepTwoPage
    {

        private readonly IWebDriver _driver;
        private readonly WebDriverWait _webDriverWait;
        private readonly ILogger _logger;

        private readonly By _checkOutOverviewPageTitle = By.XPath("//div[@data-test='secondary-header']/span[contains(text(),'Checkout: Overview')]");
        private readonly By _itemListInCheckoutOverview = By.XPath("//div[@data-test='cart-list']");
        private readonly By _allItemsInCheckoutOverview = By.XPath("./child::*");
        private readonly By _checkoutItemLink = By.XPath("./following::a");
        private readonly By _lblQuantityOfItemInCheckoutOverview = By.XPath("//div[@data-test='item-quantity']");
        private readonly By _lblPriceOfItemInCheckoutOverview = By.XPath("//div[@data-test='inventory-item-price']");
        private readonly By _lblItemSubTotal = By.XPath("//div[@data-test='subtotal-label']");
        private readonly By _lblItemTax = By.XPath("//div[@data-test='tax-label']");
        private readonly By _lblItemTotal = By.XPath("//div[@data-test='total-label']");
        private readonly By _finishBtn = By.Id("finish");
        private readonly By _cancelBtn = By.Id("cancel");

        public CheckoutStepTwoPage(IWebDriver driver)
        {
            _driver = driver;
            _webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = factory.CreateLogger<CheckoutStepTwoPage>();
        }

        public bool IsCheckoutOverviewPageTitleDisplayed()
        {
            bool value = false;
            try
            {
                IWebElement checkoutOverviewPageTitleElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_checkOutOverviewPageTitle));
                value = _driver.FindElement(_checkOutOverviewPageTitle).Displayed;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting checkout overview page title: {ex.StackTrace}");
            }
            return value;
        }

        public bool IsSelectedItemDisplayedInCheckoutOverview(String itemName)
        {
            bool value = false;
            try
            {
                IWebElement itemListElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_itemListInCheckoutOverview));
                IList<IWebElement> allChildItems = itemListElement.FindElements(_allItemsInCheckoutOverview);
                for (int i = 0; i < allChildItems.Count; i++)
                {
                    IWebElement itemLink = allChildItems[i].FindElement(_checkoutItemLink);
                    _logger.LogInformation($"Item found in checkout overview: {itemLink.Text}");
                    if (itemLink.Text.Equals(itemName))
                    {
                        value = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while checking if selected item is displayed in checkout overview : {ex.StackTrace}");
                return value;
            }
            return value;
        }

        public int GetQuantityOfItemInCheckOutOverview(String itemName)
        {
            int quantity = 0;
            try
            {
                IWebElement itemListElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_itemListInCheckoutOverview));
                IList<IWebElement> allChildItems = itemListElement.FindElements(_allItemsInCheckoutOverview);
                for (int i = 0; i < allChildItems.Count; i++)
                {
                    IWebElement itemLink = allChildItems[i].FindElement(_checkoutItemLink);
                    IWebElement quantityLabel = allChildItems[i].FindElement(_lblQuantityOfItemInCheckoutOverview);
                    _logger.LogInformation($"Item found in checkout overview: {itemLink.Text}");
                    if (itemLink.Text.Equals(itemName))
                    {
                        quantity = Convert.ToInt16(quantityLabel.Text);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting quantity of item in check out overview : {ex.StackTrace}");
            }
            return quantity;
        }

        public double GetPriceOfItem(String itemName)
        {
            double price = 0.0;
            try
            {
                IWebElement itemListElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_itemListInCheckoutOverview));
                IList<IWebElement> allChildItems = itemListElement.FindElements(_allItemsInCheckoutOverview);
                for (int i = 0; i < allChildItems.Count; i++)
                {
                    IWebElement itemLink = allChildItems[i].FindElement(_checkoutItemLink);
                    IWebElement priceLabel = allChildItems[i].FindElement(_lblPriceOfItemInCheckoutOverview);
                    _logger.LogInformation($"Item found in checkout overview: {itemLink.Text}");
                    if (itemLink.Text.Equals(itemName))
                    {
                        String priceWithDollarSign = priceLabel.Text;
                        price = Convert.ToDouble(priceWithDollarSign.Split('$')[1]);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting price of item: {ex.StackTrace}");
            }
            return price;
        }

        public double GetTotalPriceOfItemsWithoutTaxInCheckOutOverview()
        {
            double totalPriceWithoutTax = 0.0;
            try
            {
                IWebElement itemPriceWithoutTaxElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_lblItemSubTotal));
                String priceWithDollarSign = itemPriceWithoutTaxElement.Text;
                totalPriceWithoutTax = Convert.ToDouble(priceWithDollarSign.Split('$')[1]);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting total price of items without tax: {ex.StackTrace}");
            }
            return totalPriceWithoutTax;
        }

        public double getTaxAmountOfItemsInCheckOutOverview()
        {
            double totalTax = 0.0;
            try
            {
                IWebElement taxElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_lblItemTax));
                String taxWithDollarSign = taxElement.Text;
                totalTax = Convert.ToDouble(taxWithDollarSign.Split('$')[1]);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting total tax: {ex.StackTrace}");
            }
            return totalTax;
        }

        public double getTotalPriceOfItemsWithTaxInCheckOutOverview()
        {
            double totalPrice = 0.0;
            try
            {
                IWebElement totalPriceElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_lblItemTotal));
                String totalPriceWithDollarSign = totalPriceElement.Text;
                totalPrice = Convert.ToDouble(totalPriceWithDollarSign.Split('$')[1]);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting total price: {ex.StackTrace}");
            }
            return totalPrice;
        }

        public void ClickFinishButton()
        {
            try
            {
                IWebElement finishBtnElement = _webDriverWait.Until(ExpectedConditions.ElementToBeClickable(_finishBtn));
                finishBtnElement.Click();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while clicking finish button: {ex.StackTrace}");
            }
        }

        public void ClickCancelButton()
        {
            try
            {
                IWebElement cancelBtnElement = _webDriverWait.Until(ExpectedConditions.ElementToBeClickable(_cancelBtn));
                cancelBtnElement.Click();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while clicking cancel button: {ex.StackTrace}");
            }
        }

    }

}
