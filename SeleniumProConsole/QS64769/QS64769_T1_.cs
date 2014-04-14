using System;
using System.Text;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace SeleniumProConsole.QS64769
{
    [TestFixture]
    public class QS64769T1 : SeleniumProConsole.SelenuimAutoBase
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost:32369";
            verificationErrors = new StringBuilder();
        }

        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }

        [Test]
        public void TheQS64769T1Test()
        {
            driver.Navigate().GoToUrl(baseURL + "/p/flap-happy-original-8-flap-hat-cottons-upf-50-x17-flap-100-pure-aloe-vera-gel-23485");
            base.Wait(driver, (d) => { return d.Title.IndexOf("Flap Happy") >= 0; });

            driver.FindElement(By.Id("topColorPane10")).Click();
            driver.FindElement(By.Id("FP_075SizeButton")).Click();
            Wait(500);
            try
            {
                Assert.AreEqual("Whitebow'<>&\"", driver.FindElement(By.Id("firstValueSpan")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
            driver.FindElement(By.LinkText("ship-to-zip")).Click();

            Wait(driver, d => { return driver.FindElement(By.CssSelector("div.quidsiHtmlContent > div.pdpZipCodeHopup")).Text.IndexOf("To ensure") >= 0; });
            driver.FindElement(By.CssSelector("div.quidsiHtmlContent > div.pdpZipCodeHopup > div.specialZipBg > #zipCodeInput")).Clear();
            driver.FindElement(By.CssSelector("div.quidsiHtmlContent > div.pdpZipCodeHopup > div.specialZipBg > #zipCodeInput")).SendKeys("10001");
            driver.FindElement(By.CssSelector("div.quidsiHtmlContent > div.pdpZipCodeHopup > div.specialZipBg > span.btnsBox > #InputZipCodeButton")).Click();

            Wait(driver, d => { return driver.FindElement(By.CssSelector("span.orderBy")).Text.IndexOf("ORDER BY:") >= 0; });

            try
            {
                Assert.AreEqual("Whitebow'<>&\"", driver.FindElement(By.Id("firstValueSpan")).Text);
            }
            catch (AssertionException e)
            {
                verificationErrors.Append(e.Message);
            }
        }

        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}
