using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudyPlanner.Services
{
    public interface IShareData
    {
        Task<bool> Share(List<string> filePaths, string subject, string text);
    }
}
