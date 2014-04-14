using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumProConsole
{
    public class SelenuimAutoBase
    {
        protected void Wait(IWebDriver driver, Func<IWebDriver, bool> conditon, int seconds)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(seconds));
            wait.Until(conditon);
        }
        protected void Wait(IWebDriver driver, Func<IWebDriver, bool> conditon)
        {
            Wait(driver, conditon, 10);
        }
        protected void Wait(int millonSecondes)
        {
            System.Threading.Thread.Sleep(millonSecondes);   
        }
    }
}
