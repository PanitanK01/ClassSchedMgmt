using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudyPlanner.Services
{
    public interface ITesting
    {
        Task Share(string text, List<string> filesPath);
    }
}
