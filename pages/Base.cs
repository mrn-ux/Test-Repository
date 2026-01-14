using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Input;
using OpenQA.Selenium.Chrome;
using Reqnroll;
using Reqnroll.BoDi;

namespace TestProjectAssignment.pages
{
    [Binding]
    public sealed class Base
    {

        private readonly IObjectContainer _objectContainer;
        private String url;

        public Base(IObjectContainer objectContainer)
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

            // Initialize the WebDriver with the configured options
            IWebDriver driver = new ChromeDriver(chromeOptions);
            _objectContainer.RegisterInstanceAs<IWebDriver>(driver);
            driver.Manage().Window.Maximize();
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            url = config["AppSettings:AppUrl"] ?? String.Empty;
            if (!string.IsNullOrEmpty(url))
            {
                Environment.SetEnvironmentVariable("APP_URL", url, EnvironmentVariableTarget.Process);

            }
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