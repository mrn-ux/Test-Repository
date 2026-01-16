using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using System;
using System.Text;

namespace TestProjectAssignment.pages
{
    public class CheckoutStepOnePage
    {

        private readonly IWebDriver _driver;
        private readonly WebDriverWait _webDriverWait;
        private readonly ILogger _logger;

        private readonly By _checkOutPageTitle = By.XPath("//div[@data-test='secondary-header']/span[contains(text(),'Checkout: Your Information')]");
        private readonly By _firstNameTxtBox = By.Id("first-name");
        private readonly By _lastNameTxtBox = By.Id("last-name");
        private readonly By _zipPostalCodeTxtBox = By.Id("postal-code");
        private readonly By _continueBtn = By.Id("continue");
        private readonly By _cancelBtn = By.Id("cancel");

        public CheckoutStepOnePage(IWebDriver driver)
        {
            _driver = driver;
            _webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(20));
            ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = factory.CreateLogger<CheckoutStepOnePage>();
        }

        public bool IsCheckoutPageTitleDisplayed()
        {
            bool value = false;
            try
            {
                IWebElement checkoutPageTitleElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_checkOutPageTitle));
                value = _driver.FindElement(_checkOutPageTitle).Displayed;
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while getting checkout page title: {ex.StackTrace}");
            }
            return value;
        }

        public void EnterFirstName(String firstName)
        {
            try
            {
                _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_checkOutPageTitle));
                IWebElement firstNameElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_firstNameTxtBox));
                firstNameElement.SendKeys(firstName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while entering First Name: {ex.StackTrace}");
            }
        }

        public void EnterLastName(String lastName)
        {
            try
            {
                IWebElement lastNameElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_lastNameTxtBox));
                lastNameElement.SendKeys(lastName);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while entering Last Name: {ex.StackTrace}");
            }
        }

        public void EnterZipPostalCode(String zipPostalCode)
        {
            try
            {
                IWebElement zipPostalCodeElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_zipPostalCodeTxtBox));
                zipPostalCodeElement.SendKeys(zipPostalCode);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while entering Zip Postal Code: {ex.StackTrace}");
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

        public void ClickContinueButton()
        {
            try
            {
                IWebElement continueBtnElement = _webDriverWait.Until(ExpectedConditions.ElementToBeClickable(_continueBtn));
                continueBtnElement.Click();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while clicking continue button: {ex.StackTrace}");
            }
        }

    }

}
