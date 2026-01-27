using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using Reqnroll.BoDi;
using TestProjectAssignment.pages;

namespace TestProjectAssignment.hooks
{
    [Binding]
    public sealed class WebDriverHooks
    {

        private readonly IObjectContainer _objectContainer;

        public WebDriverHooks(IObjectContainer objectContainer)
        {
            _objectContainer = objectContainer;
        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOptions.AddUserProfilePreference("profile.password_manager_leak_detection", false);
            IWebDriver driver = new ChromeDriver(chromeOptions);
            _objectContainer.RegisterInstanceAs<IWebDriver>(driver);
            _objectContainer.RegisterTypeAs<LoginPage, LoginPage>();
            _objectContainer.RegisterTypeAs<InventoryPage, InventoryPage>();
            _objectContainer.RegisterTypeAs<CartPage, CartPage>();
            _objectContainer.RegisterTypeAs<CheckoutCompletePage, CheckoutCompletePage>();
            _objectContainer.RegisterTypeAs<CheckoutStepOnePage, CheckoutStepOnePage>();
            _objectContainer.RegisterTypeAs<CheckoutStepTwoPage, CheckoutStepTwoPage>();
            driver.Manage().Window.Maximize();
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            string url = config["AppSettings:AppUrl"] ?? String.Empty;
            driver.Navigate().GoToUrl(url);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            IWebDriver driver = _objectContainer.Resolve<IWebDriver>();
            driver.Quit();
        }

    }

}