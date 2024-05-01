// See https://aka.ms/new-console-template for more information
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MicrosoftRewards.Model.Services.Application;
using MicrosoftRewards.Model.Services.Infrastructure;
using MMicrosoftRewards.Model;
using MMicrosoftRewards.Model.Services.Application;
using MMicrosoftRewards.Model.Services.Infrastructure;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
internal class Program
{
  private static void Main(string[] args)
  {

    Console.WriteLine("Enter username:");
    string userName = Console.ReadLine() ?? "";
    Console.WriteLine("Enter password:");
    string passWord = Console.ReadLine() ?? "";
    if (!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(passWord))
    {
      var services = CreateServices();
      Application app = services.GetRequiredService<Application>();
      app.MyLogic(userName, passWord);
    }
    else
    {
      Console.WriteLine("Username e Password obbligatori");
    }
    Console.ReadKey();
  }
  private static ServiceProvider CreateServices()
  {
    var serviceProvider = new ServiceCollection()
      .AddLogging(options =>
      {
        options.ClearProviders();
        options.AddConsole();
      })
      .AddSingleton<Application>()
        .AddTransient<IWordsService, AdoNetWordsService>()
        .AddTransient<IDatabaseAccessor, SQLiteAccessor>()
        .AddTransient<ITXTFileAccessor, TXTFileAccessor>()
        .AddTransient<IFindFiles, FindFileAccessor>()
        .AddSingleton<IBrowsersService, ChromeBrowsersService>()
        .AddSingleton<IMicrosoftRewardsService, MicrosoftRewardsService>()
          .BuildServiceProvider();
    return serviceProvider;
  }
}
public class Application
{
  private readonly ILogger<Application> logger;
  private readonly IDatabaseAccessor databaseAccessor;
  private readonly IWordsService wordsService;
  private readonly IBrowsersService browserService;
  private readonly IMicrosoftRewardsService microsoftRewardsService;
  public Application(ILogger<Application> _logger, IDatabaseAccessor _databaseAccessor
     , IWordsService _wordsService, IBrowsersService _browserService
    , IMicrosoftRewardsService microsoftRewardsService)
  {
    logger = _logger;
    databaseAccessor = _databaseAccessor;
    wordsService = _wordsService;
    this.browserService = _browserService;
    this.microsoftRewardsService = microsoftRewardsService;
  }
  public void MyLogic(string username, string password)
  {
    int puntimaxpc = 30;
    int puntimaxmobile = 60;
    //
    try
    {
      if (databaseAccessor.DBIsEmpty())
      {
        wordsService.PutWords();
      }
      //;
      IWebDriver InitializedBrowser = browserService.InitializeBrowser(false, "https://rewards.bing.com/", logger);
      microsoftRewardsService.LogInAsUser(InitializedBrowser, false, false, logger, username, password);
      PuntiReward puntidivisi = microsoftRewardsService.GetPoints(InitializedBrowser);
      string stringalog = string.Empty;
      stringalog = $"PuntiPC={puntidivisi.PuntiPc} e puntimobile={puntidivisi.PuntiMobile} e puntitotali={puntidivisi.puntiattuali}";
      logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, stringalog);
      InitializedBrowser?.Quit();
      if (puntidivisi.PuntiPc < puntimaxpc)
      {
        InitializedBrowser = browserService.InitializeBrowser(false, "https://www.bing.com/", logger);
        microsoftRewardsService.LogInAsUser(InitializedBrowser, false, true, logger, username, password);
        List<string> strings = wordsService.GetWords().GetAwaiter().GetResult();
        microsoftRewardsService.Ricerca(strings, puntidivisi.PuntiPc, puntimaxpc, InitializedBrowser, false, puntidivisi.puntiattuali, logger);
      }
      else
      {
        InitializedBrowser?.Quit();
      }
      if (puntidivisi.PuntiMobile < puntimaxmobile)
      {
        InitializedBrowser = browserService.InitializeBrowser(true, "https://www.bing.com/", logger);
        microsoftRewardsService.LogInAsUser(InitializedBrowser, true, true, logger, username, password);
        List<string> strings = wordsService.GetWords().GetAwaiter().GetResult();
        microsoftRewardsService.Ricerca(strings, puntidivisi.PuntiPc, puntimaxmobile, InitializedBrowser, true, puntidivisi.puntiattuali, logger);
      }
      InitializedBrowser?.Quit();
    }
    catch (Exception ex)
    {
      logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, ex.Message);
    }
  }
}
