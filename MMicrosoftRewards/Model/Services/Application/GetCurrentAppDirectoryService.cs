using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMicrosoftRewards.Model.Services.Application
{
  public static class GetCurrentAppDirectoryService
  {

   public static string GetCurrentAppDirectory() {
      string DBSource = AppDomain.CurrentDomain.BaseDirectory;
      DBSource = DBSource.Remove(DBSource.IndexOf("bin"));
      return DBSource += @"\Data\Database.db";
    }

    
  }
}
