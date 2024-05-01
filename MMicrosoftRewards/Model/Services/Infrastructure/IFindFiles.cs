using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMicrosoftRewards.Model.Services.Infrastructure
{
  public interface IFindFiles
  {
    List<string>? FillListFile(string directoryName, string extensionFile);
  }
}
