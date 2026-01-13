using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using Reqnroll.BoDi;

namespace TestProjectAssignment.pages
{
    [Binding]
    public sealed class Base
    {
        // For additional details on Reqnroll hooks see https://go.reqnroll.net/doc-hooks


        private readonly IObjectContainer _objectContainer;
        private readonly ScenarioContext _scenarioContext;
        private String url;
        private String userName;
        private String password;
        private String itemName;
        private double itemPrice;
        private String firstName;
        private String lastName;
        private String zipPostalCode;

        public Base(IObjectContainer objectContainer, ScenarioContext scenarioContext)
        {

            _objectContainer = objectContainer;
            _scenarioContext = scenarioContext;

        }

        [BeforeScenario]
        public void BeforeScenario()
        {
            ChromeOptions chromeOptions = new ChromeOptions();

            chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
            chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
            chromeOptions.AddUserProfilePreference("profile.password_manager_leak_detection", false);

            // Optional: Add argument to use basic password store
            chromeOptions.AddArgument("--password-store=basic");

            // Initialize the WebDriver with the configured options
            IWebDriver driver = new ChromeDriver(chromeOptions);
            _objectContainer.RegisterInstanceAs<IWebDriver>(driver);
            driver.Manage().Window.Maximize();
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            url = config["AppSettings:AppUrl"] ?? String.Empty;
            _scenarioContext.Add("AppUrl", url);
            userName = config["AppSettings:UserName"] ?? String.Empty;
            _scenarioContext.Add("UserName", userName);
            password = config["AppSettings:Password"] ?? String.Empty;
            _scenarioContext.Add("Password", password);
            itemName = config["ItemData:Name"] ?? String.Empty;
            _scenarioContext.Add("ItemName", itemName);
            itemPrice = config["ItemData:Price"] != null ? Convert.ToDouble( config["ItemData:Price"]) : 0.0;  
            _scenarioContext.Add("ItemPrice", itemPrice);
            firstName = config["CheckoutInformation:FirstName"] ?? String.Empty;
            _scenarioContext.Add("FirstName", firstName);
            lastName = config["CheckoutInformation:LastName"] ?? String.Empty;
            _scenarioContext.Add("LastName", lastName);
            zipPostalCode = config["CheckoutInformation:PostalCode"] ?? String.Empty;
            _scenarioContext.Add("PostalCode", zipPostalCode);
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