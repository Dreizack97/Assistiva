using OpenQA.Selenium;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.UI;

namespace TestProject
{
    public class UserSystemTest
    {
        private IWebDriver _driver;
        private string _urlBase = "http://localhost:5225"; // Replace with your base URL

        [SetUp]
        public void Setup()
        {
            _driver = new SafariDriver();
        }

        [Test]
        public void UserCanSignIn_ValidCredentials()
        {
            _driver.Navigate().GoToUrl($"{_urlBase}/SignIn/Index");

            _driver.FindElement(By.Id("Username")).SendKeys("JSilva000001");
            _driver.FindElement(By.Id("Password")).SendKeys("O$62p?5B");
            _driver.FindElement(By.CssSelector("button[type='submit']")).Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => driver.FindElement(By.Id("WelcomeTag")).Displayed);

            string welcomeText = _driver.FindElement(By.Id("WelcomeTag")).Text;
            Assert.That(welcomeText.Contains("Welcome"), Is.True);
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Dispose();
        }
    }
}