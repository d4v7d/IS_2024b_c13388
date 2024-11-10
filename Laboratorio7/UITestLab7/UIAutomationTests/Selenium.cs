using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace UIAutomationTests
{
    internal class Selenium
    {
        IWebDriver _driver;

        [SetUp]
        public void Setup()
        {
            _driver = new ChromeDriver();
            _driver.Manage().Window.Maximize();
        }

        [Test]
        public void Enter_To_List_Of_Countries_Test()
        {
            var URL = "http://localhost:8080/";

            _driver.Navigate().GoToUrl(URL);

            IWebElement pageTitle = _driver.FindElement(By.XPath("//h1[contains(text(), 'Lista de Paises')]"));
            Assert.IsNotNull(pageTitle, "The page title 'Lista de Paises' was not found.");
        }

        [Test]
        public void Navigate_To_Create_Country_Form_Test()
        {
            var URL = "http://localhost:8080/";
            _driver.Navigate().GoToUrl(URL);

            IWebElement agregarPaisLink = _driver.FindElement(By.XPath("//a[@href='/pais']"));
            agregarPaisLink.Click();

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
            wait.Until(driver => driver.FindElement(By.Id("name")).Displayed);

            IWebElement nombreField = _driver.FindElement(By.Id("name"));
            IWebElement continenteField = _driver.FindElement(By.Id("continente"));
            IWebElement idiomaField = _driver.FindElement(By.Id("idioma"));

            Assert.IsTrue(nombreField.Displayed, "Nombre field is not displayed.");
            Assert.IsTrue(continenteField.Displayed, "Continente field is not displayed.");
            Assert.IsTrue(idiomaField.Displayed, "Idioma field is not displayed.");
        }

        [Test]
        public void Create_New_Country_Test()
        {
            var URL = "http://localhost:8080/";
            _driver.Navigate().GoToUrl(URL);

            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));

            IWebElement agregarPaisLink = _driver.FindElement(By.XPath("//a[@href='/pais']"));
            agregarPaisLink.Click();

            wait.Until(driver => driver.FindElement(By.Id("name")).Displayed);

            string countryName = "TestCountry" + DateTime.Now.Ticks;
            _driver.FindElement(By.Id("name")).SendKeys(countryName);

            IWebElement continenteSelectElement = _driver.FindElement(By.Id("continente"));
            SelectElement continenteSelect = new SelectElement(continenteSelectElement);
            continenteSelect.SelectByText("Europa");

            _driver.FindElement(By.Id("idioma")).SendKeys("TestIdioma");

            _driver.FindElement(By.XPath("//button[contains(text(),'Guardar')]")).Click();

            wait.Until(driver => driver.Url == URL);

            bool isCountryPresent = _driver.FindElements(By.XPath($"//td[contains(text(),'{countryName}')]")).Count > 0;
            Assert.IsTrue(isCountryPresent, "The new country was not added to the list.");
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}
