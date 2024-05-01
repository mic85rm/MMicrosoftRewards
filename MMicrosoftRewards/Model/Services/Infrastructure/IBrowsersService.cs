using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMicrosoftRewards.Model.Services.Infrastructure
{
    public interface IBrowsersService
    {
        public IWebDriver InitializeBrowser(bool ismobile, string sito, ILogger logger);

        


    }
}
