using MicrosoftRewards.Model.Services.Infrastructure;
using MMicrosoftRewards.Model.Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace MicrosoftRewards.Model.Services.Application
{
  public class AdoNetWordsService : IWordsService
  {
    private readonly IDatabaseAccessor db;
    private readonly ITXTFileAccessor TXTFileAccessor;
    private readonly IFindFiles findFiles;

    public AdoNetWordsService(IDatabaseAccessor _db,ITXTFileAccessor _TXTFileAccessor,IFindFiles _findFiles)
    {
      this.db = _db;
      this.TXTFileAccessor = _TXTFileAccessor;
      this.findFiles = _findFiles;
    }
    public async Task<List<string>> GetWords()
    {
      string query = "select * from Parole";
      DataSet dataSet =await db.FillDataSetAsync(query);
      List<string> words = new List<string>();
      DataTable dataTable = dataSet.Tables[0];
      foreach (DataRow row in dataTable.Rows)
      {
        words.Add(Convert.ToString(row["Parola"]) ?? "");
      }
      return words;
    }

    public async void PutWords()
    {
      
      List<string>? files = findFiles.FillListFile(@"c:\","txt");
      if (files != null) {
        foreach (string fileName in files)
        {
          List<string>? listOfWords =await TXTFileAccessor.FillList(fileName);
          foreach (string word in listOfWords)
         db.InsertData("INSERT INTO PAROLE(id,parola) VALUES (null,@Word)", word);
        }
        
      }
    }
  }
}
