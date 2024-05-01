using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMicrosoftRewards.Model.Services.Infrastructure
{
  public class FindFileAccessor : IFindFiles
  {
    public List<string>? FillListFile(string directoryName,string extensionFile)
    {
      //directory name andrebbe "sanizzato"
      //idem estensione file

      
      try
      {
       

        DirectoryInfo dinfo = new DirectoryInfo(directoryName);
        FileInfo[] Files = dinfo.GetFiles($"*.{extensionFile}");
        if (Files == null || Files.Length == 0) { return null; }
        
      
        
        return Files.Select(x=>x.FullName).ToList<string>();
        
      }
      catch (Exception ex)
      {

      return null;
      }
    }

    
  }
}
