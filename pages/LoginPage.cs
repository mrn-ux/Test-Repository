using Microsoft.Extensions.Logging;
using SeleniumExtras.WaitHelpers;

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace TestProjectAssignment.pages
{
    public class LoginPage
    {

        private readonly IWebDriver _driver;
        private readonly WebDriverWait _webDriverWait;
        private readonly ILogger _logger;

        private readonly By _usernameTxtBox = By.Id("user-name");
        private readonly By _passwordTxtBox = By.Id("password");
        private readonly By _loginBtn = By.Id("login-button");

        public LoginPage(IWebDriver driver)
        {
            _driver = driver;
            _webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            _logger = factory.CreateLogger<LoginPage>();
        }

        public void EnterUserName(String username)
        {
            try
            {
                IWebElement userNameElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_usernameTxtBox));
                userNameElement.SendKeys(username);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while entering userName: {ex.StackTrace}");
            }
        }

        public void EnterPassword(String password)
        {
            try
            {
                IWebElement passwordElement = _webDriverWait.Until(ExpectedConditions.ElementIsVisible(_passwordTxtBox));
                passwordElement.SendKeys(password);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while entering password: {ex.StackTrace}");
            }
        }

        public void clickLoginButton()
        {
            try
            {
                IWebElement loginButtonElement = _webDriverWait.Until(ExpectedConditions.ElementToBeClickable(_loginBtn));
                loginButtonElement.Click();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error while clicking login button: {ex.StackTrace}");
            }
        }

    }

}
