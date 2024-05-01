using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicrosoftRewards.Model.Services.Application
{
  public interface IWordsService
  {
    Task<List<string>> GetWords();

    void PutWords();

  }
}
