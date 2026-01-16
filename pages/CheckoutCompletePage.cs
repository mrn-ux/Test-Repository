using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Text;

namespace TestProjectAssignment.pages
{
    public class CheckoutCompletePage
    {

        private readonly IWebDriver _driver;
        private readonly WebDriverWait _webDriverWait;
        private readonly ILogger _logger;

        private readonly By _checkOutCompletePageTitle = By.XPath("//div[@data-test='secondary-header']/span[contains(text(),'Checkout: Complete!')]");
        private readonly By _thankYouMessage = By.XPath("//h2[@data-test='complete-header']");
        private readonly By _backHomeBtn = By.Id("back-to-products");


        public CheckoutCompletePage(IWebDriver driver)
        {
            _driver = driver;
            _webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = factory.CreateLogger<CheckoutCompletePage>();
        }

        public bool IsCheckoutCompletePageTitleDisplayed()
        {
            bool value = false;
            try
            {
                IWebElement checkoutCompletePageTitleElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_checkOutCompletePageTitle));
                value = _driver.FindElement(_checkOutCompletePageTitle).Displayed;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting checkout complete page title: {ex.StackTrace}");
            }
            return value;
        }

        public String GetThankYouMessageInCheckOutCompletion()
        {
            try
            {
                IWebElement thanksMessageElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_thankYouMessage));
                return thanksMessageElement.Text;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting thanks message text: {ex.StackTrace}");
                return String.Empty;
            }
        }

        public void ClickBackToHomeButton()
        {
            try
            {
                IWebElement backToHomeBtnElement = _webDriverWait.Until(ExpectedConditions.ElementToBeClickable(_backHomeBtn));
                backToHomeBtnElement.Click();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while clicking back to home button: {ex.StackTrace}");
            }
        }

    }

}
