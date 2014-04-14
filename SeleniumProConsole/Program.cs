using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;


namespace SeleniumProConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IWebDriver driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://www.google.com/");
            IWebElement query = driver.FindElement(By.Name("q"));
            query.SendKeys("Cheese");
            query.Submit();
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until((d) => { return d.Title.ToLower().StartsWith("cheese"); });
            System.Console.WriteLine("Page title is: " + driver.Title);
            driver.Quit();

            Console.Read();
        }
    }
}
