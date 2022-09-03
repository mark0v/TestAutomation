using System;
using System.ComponentModel;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;

namespace SeleniumNUnitAllWebTechnologies
{
    [TestFixture]
    //[TestClass]
    public class TodoTest
    {
        private const int WAIT_FOR_ELEMENT_TIMEOUT = 30;
        private IWebDriver _driver;
        private WebDriverWait _webDriverWait;
        private Actions _actions;

        [SetUp]
        //[TestInitialize]
        public void TestInit()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());
            var options = new ChromeOptions();
            options.AddArguments("headless");
            _driver = new ChromeDriver(options);
            _driver.Manage().Window.Maximize();
            _webDriverWait = new WebDriverWait(_driver, TimeSpan.FromSeconds(WAIT_FOR_ELEMENT_TIMEOUT));
            _actions = new Actions(_driver);
        }

        [TearDown]
        //[TestCleanup]
        public void TestCleanup()
        {
            _driver.Quit();
        }

        //[TestMethod]
        [Test]
        public void OpenMainEShopPage()
        {
            _driver.Navigate().GoToUrl("https://test.oleksandrmarkov.tech/");


        }

        private void ValidateInnerTextIs(IWebElement resultSpan, string expectedText)
        {
            _webDriverWait.Until(ExpectedConditions.TextToBePresentInElement(resultSpan, expectedText));

        }

        private IWebElement GetItemCheckBox(string todoItem)
        {
            return WaitAndFindElement(By.XPath($"//label[text()='{todoItem}']/preceding-sibling::input"));
        }

        private IWebElement WaitAndFindElement(By locator)
        {
            return _webDriverWait.Until(ExpectedConditions.ElementExists(locator));
        }
    }
}
