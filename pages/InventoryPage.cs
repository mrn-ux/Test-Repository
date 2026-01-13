using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;


namespace TestProjectAssignment.pages
{

    public class InventoryPage
    {
        private readonly IWebDriver _driver;
        private readonly WebDriverWait _webDriverWait;
        private readonly ILogger _logger;

        //Locators

        private readonly By _applicationTitle = By.Id("menu_button_container");
        private readonly By _inventoryPageTitle = By.XPath("//div[@data-test='secondary-header']/span[contains(text(),'Products')]");
        private readonly By _inventoryList = By.XPath("//div[@data-test='inventory-list']");
        private readonly By _allInventoryItems = By.XPath("./child::*");
        private readonly By _inventoryItemLink = By.XPath("./following::a[2]");
        private readonly By _addToCartBtn = By.XPath("./following::button");
        private readonly By _removeFromCartBtn = By.XPath("./button[contains(text(),'Remove')]");
        private readonly By _shoppingCartBtn = By.Id("shopping_cart_container");

        public InventoryPage(IWebDriver driver)
        {
            _driver = driver;
            _webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = factory.CreateLogger<InventoryPage>();
        }

        public String GetApplicationTitle()
        {
            try
            {
                IWebElement applicationTitleElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_applicationTitle));
                return applicationTitleElement.Text;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting application title: {ex.StackTrace}");
                return String.Empty;
            }
        }

        public bool IsInventoryPageTitleDisplayed()
        {
            bool value = false;
            try
            {
                IWebElement inventoryPageTitleElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_inventoryPageTitle));
                value = _driver.FindElement(_inventoryPageTitle).Displayed;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting checkout inventory page title: {ex.StackTrace}");
            }
            return value;
        }

        public bool IsItemDisplayed(String itemName)
        {
            bool value = false;
            try
            {
                IWebElement itemListElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_inventoryList));
                IList<IWebElement> allChildItems = itemListElement.FindElements(_allInventoryItems);
                foreach (IWebElement item in allChildItems)
                {
                    IWebElement itemLink = item.FindElement(_inventoryItemLink);
                    if (itemLink.Text.Equals(itemName))
                    {
                        value = true;
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while checking if item is displayed: {ex.StackTrace}");
                return value;
            }
            return value;
        }

        public void ClickItem(String itemName)
        {
            try
            {
                IWebElement itemListElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_inventoryList));
                IList<IWebElement> allChildItems = itemListElement.FindElements(_allInventoryItems);
                foreach (IWebElement item in allChildItems)
                {
                    IWebElement itemLink = item.FindElement(_inventoryItemLink);
                    if (itemLink.Text.Equals(itemName))
                    {
                        itemLink.Click();
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while clicking item title: {ex.StackTrace}");

            }
        }

        public void ClickAddToCartButtonInItem(String itemName)
        {
            try
            {
                IWebElement itemListElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_inventoryList));
                IList<IWebElement> allChildItems = itemListElement.FindElements(_allInventoryItems);
                foreach (IWebElement item in allChildItems)
                {
                    IWebElement itemLink = item.FindElement(_inventoryItemLink);
                    IWebElement addToCartBtn;
                    if (itemLink.Text.Equals(itemName))
                    {
                        addToCartBtn = item.FindElement(_addToCartBtn);
                        addToCartBtn.Click();
                        break;
                    }

                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while clicking add to cart button: {ex.StackTrace}");

            }
        }


        public void ClickRemoveFromCartButtonInItem(String itemName)
        {
            try
            {
                IWebElement itemListElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_inventoryList));
                IList<IWebElement> allChildItems = itemListElement.FindElements(_allInventoryItems);
                foreach (IWebElement item in allChildItems)
                {
                    IWebElement itemLink = item.FindElement(_inventoryItemLink);
                    IWebElement removeFromCartBtn;
                    if (itemLink.Text.Equals(itemName))
                    {
                        removeFromCartBtn = item.FindElement(_removeFromCartBtn);
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while clicking remove from cart button: {ex.StackTrace}");

            }
        }

        public void ClickShoppingCartButton()
        {
            try
            {
                IWebElement shoppingCartBtnElement = _webDriverWait.Until(ExpectedConditions.ElementToBeClickable(_shoppingCartBtn));
                shoppingCartBtnElement.Click();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while clicking shopping cart button: {ex.StackTrace}");
            }

        }

    }
}
