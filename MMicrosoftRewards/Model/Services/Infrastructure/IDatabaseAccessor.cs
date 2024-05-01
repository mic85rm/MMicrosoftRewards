using MMicrosoftRewards.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftRewards.Model.Services.Infrastructure
{
  public interface IDatabaseAccessor
  {
    Task<DataSet> FillDataSetAsync(string query);

    int InsertData(string query,string parameterValue);

    bool DBIsEmpty();

  }
}
