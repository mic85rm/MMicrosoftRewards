using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMicrosoftRewards.Model.Services.Application
{
  public interface IMicrosoftRewardsService
  {
    [return: MaybeNull]
    public IWebDriver LogInAsUser(IWebDriver Driver, bool mobile, bool notReward, ILogger logger,string user,string password);

    public PuntiReward GetPoints(IWebDriver Driver);

    public void Ricerca(List<string> testo, int puntiinizio, int puntiarrivo, IWebDriver Driver, bool ismobile, int puntiattuali, ILogger logger);
  }
}
