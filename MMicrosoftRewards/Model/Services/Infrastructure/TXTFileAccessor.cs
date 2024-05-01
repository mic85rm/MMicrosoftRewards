using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MMicrosoftRewards.Model.Services.Infrastructure
{
  public class TXTFileAccessor:ITXTFileAccessor
  {
    public async Task<List<string>> FillList(string textFile)
    {List<string> list = new List<string>();  
      using (StreamReader file = new StreamReader(textFile))
      {
        string? line;

        while ((line = await file.ReadLineAsync().ConfigureAwait(false)) != null)
        {
          list.Add(line); 
        }
        return list;
      }
    }
  }
}
