using System.Data.SQLite;
using System.Data;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Data.SqlClient;
using System;
using System.Security.Policy;
using MMicrosoftRewards.Model.Services.Application;
namespace MicrosoftRewards.Model.Services.Infrastructure
{
  public class SQLiteAccessor : IDatabaseAccessor
  {


    public int InsertData(string query,string parameterValue)
    {
      using ( SQLiteConnection sqlite_conn = new SQLiteConnection(@$"data source={GetCurrentAppDirectoryService.GetCurrentAppDirectory()}"))
      {
        sqlite_conn.Open();
        using (var cmd = new SQLiteCommand(query, sqlite_conn))
        {
          cmd.Parameters.Clear();
          cmd.Parameters.AddWithValue("@Word",parameterValue);
          return cmd.ExecuteNonQuery();
          
           
        }
      }
    }

   


    public async Task<DataSet> FillDataSetAsync(string query)
    {

      using (SQLiteConnection sqlite_conn = new SQLiteConnection(@$"data source={GetCurrentAppDirectoryService.GetCurrentAppDirectory()}"))
      {
        await sqlite_conn.OpenAsync();
        using (var cmd = new SQLiteCommand(query, sqlite_conn))
        {
          using (SQLiteDataReader sqliteDataReader =(SQLiteDataReader) await cmd.ExecuteReaderAsync())
          {
            DataTable dt = new DataTable();
            DataSet dataSet = new DataSet();
            dataSet.Tables.Add(dt);
            dt.Load(sqliteDataReader);
            return dataSet;
          }
        }
      }
    }

    public  bool DBIsEmpty()
    {

      using (SQLiteConnection sqlite_conn = new SQLiteConnection(@$"data source={GetCurrentAppDirectoryService.GetCurrentAppDirectory()}"))
      {
         sqlite_conn.Open();
        using (var cmd = new SQLiteCommand("SELECT COUNT(*) FROM parole", sqlite_conn))
        {
          cmd.Parameters.Clear();
         return Convert.ToBoolean(cmd.ExecuteScalar());

          
        }
      }
    }
  }
}
