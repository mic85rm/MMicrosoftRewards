using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace MMicrosoftRewards.Model.Services.Infrastructure
{
    public class ChromeBrowsersService : IBrowsersService
    {


        public IWebDriver InitializeBrowser(bool ismobile, string sito, ILogger logger)
        {
            IWebDriver m_driver;
            try
            {
                if (!ismobile)
                {
                    m_driver = new ChromeDriver("");
                }
                else
                {
                    ChromeOptions chromeOptions = new ChromeOptions();
                    chromeOptions.EnableMobileEmulation("Pixel 2");
                    chromeOptions.AddUserProfilePreference("safebrowsing.enabled", true);
                    chromeOptions.AddUserProfilePreference("credentials_enable_service", false);
                    chromeOptions.AddUserProfilePreference("profile.password_manager_enabled", false);
                    m_driver = new ChromeDriver("", chromeOptions);
                }
                m_driver.Url = sito;
                m_driver.Manage().Window.Maximize();
                return m_driver;
            }
            catch (Exception ex)
            {

                logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, ex.Message);
                return null;
            }
        }


        


    }
}
