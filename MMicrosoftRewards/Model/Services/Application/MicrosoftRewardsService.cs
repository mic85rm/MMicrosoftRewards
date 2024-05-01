using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;

namespace MMicrosoftRewards.Model.Services.Application
{
  public class MicrosoftRewardsService:IMicrosoftRewardsService
  {


    public void Ricerca(List<string> testo, int puntiinizio, int puntiarrivo, IWebDriver Driver, bool ismobile, int puntiattuali, ILogger logger)
    {
      string camporicerca = string.Empty;
      try
      {
        var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 60));
        while (puntiinizio < puntiarrivo + puntiattuali)
        {
          Thread.Sleep(6000);
          Random rnd = new Random();
          int parolarandom = rnd.Next(1, testo.Count);
          string paroladacercare = Convert.ToString(testo[parolarandom]);
          if (ismobile)
          {
            camporicerca = "q";

          }
          else
          {
            camporicerca = "q";

          }
          IWebElement ricerca = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id($"sb_form_{camporicerca}")));
          ricerca.Clear();
          ricerca.SendKeys(paroladacercare);
          ricerca.Submit();
          if (!ismobile)
          {
            IWebElement punteggio = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("id_rc")));
            puntiinizio = Convert.ToInt32(punteggio.Text);

          }
          else
          {
            puntiinizio = puntiinizio + 2;
          }
        }
      }
      catch (Exception ex)
      {

        logger.Log( Microsoft.Extensions.Logging.LogLevel.Information,ex.Message);
      }

    }

    public PuntiReward GetPoints(IWebDriver DriverPunti)
    {
      var wait = new WebDriverWait(DriverPunti, new TimeSpan(0, 0, 60));
      PuntiReward puntiReward = new PuntiReward();
      Thread.Sleep(1500);
      IWebElement puntiattuali = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[1]/div[2]/main/div/ui-view/mee-rewards-dashboard/main/mee-rewards-user-status-banner/div/div/div/div/div[2]/div[1]/mee-rewards-user-status-banner-item/mee-rewards-user-status-banner-balance/div/div/div/div/div/div/p/mee-rewards-counter-animation/span")));
      //IWebElement puntiattuali = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("hp")));
      string provapuntiboh = puntiattuali.Text.Replace(".", "");
      puntiReward.puntiattuali = Convert.ToInt32(provapuntiboh);
      Thread.Sleep(1500);
      IWebElement ripartizionePunti = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.PartialLinkText("punti")));
      ripartizionePunti.Click();
      Thread.Sleep(1500);
      IWebElement PuntiPC = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[5]/div[2]/div[2]/mee-rewards-earning-report-points-breakdown/div/div/div[2]/div/div[1]/div/div[2]/mee-rewards-user-points-details/div/div/div/div/p[2]/b")));
     // IWebElement PuntiMobile = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.XPath("/html/body/div[5]/div[2]/div[2]/mee-rewards-earning-report-points-breakdown/div/div/div[2]/div/div[2]/div/div[2]/mee-rewards-user-points-details/div/div/div/div/p[2]/b")));
      puntiReward.PuntiPc = Convert.ToInt32(PuntiPC.Text);
      //puntiReward.PuntiMobile = Convert.ToInt32(PuntiMobile.Text);
      DriverPunti?.Quit();
      return puntiReward;
    }



    [return: MaybeNull]
    public IWebDriver LogInAsUser(IWebDriver Driver, bool mobile, bool notReward, ILogger logger,string user,string passwd)
    {
      int punti = -1;
      try
      {
        var wait = new WebDriverWait(Driver, new TimeSpan(0, 0, 60));
        if (notReward)
        {
          if (!mobile)
          {
            IWebElement login = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("id_s")));
            login.Click();
            login = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("id_text_signin")));
            login.Click();
          }
          else
          {
            IWebElement login = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("mHamburger")));
            login.Click();
            Thread.Sleep(1500);
            IWebElement accedi = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("hb_s")));
            accedi.Click();
          }
        }
        //Driver.FindElement(By.Name("loginfmt"));
        IWebElement username = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name("loginfmt")));
        username.Clear();
        username.SendKeys(user);
        Driver?.FindElement(By.Id("idSIButton9")).Click();

        Thread.Sleep(1500);
        IWebElement password = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Name("passwd")));
        password.Clear();
        password.SendKeys(passwd);
        Thread.Sleep(1500);
        IWebElement avanti = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("idSIButton9")));
        avanti.Click();
        if (!mobile)
        {
          Thread.Sleep(1500);
           avanti = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("acceptButton")));
          avanti.Click();
        }
        if (notReward)
        {
          Thread.Sleep(1500);
          IWebElement cookie = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("bnp_btn_accept")));
          cookie.Click();
          if (!mobile)
          {
            Thread.Sleep(1500);
            IWebElement punteggio = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("id_rc")));
            punti = Convert.ToInt32(punteggio.Text);
          }
          else
          {
            IWebElement hamburger = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("mHamburger")));
            hamburger.Click();
            Thread.Sleep(1500);
            IWebElement punteggio = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.Id("fly_id_rc")));
            punti = Convert.ToInt32(punteggio.Text);
            IWebElement chiudihamburger = wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(By.ClassName("hbic_close")));
            chiudihamburger.Click();
          }
        }
        //Console.WriteLine( punteggio.Text);
        //IWebElement ricerca = Driver.FindElement(By.Id("sb_form_q"));
        return Driver;
      }
      catch (Exception ex)
      {
        logger.Log(Microsoft.Extensions.Logging.LogLevel.Information, ex.Message);
        return null;

      }
    }
  }
}
