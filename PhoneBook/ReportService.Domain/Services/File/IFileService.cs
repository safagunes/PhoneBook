using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Services.File
{
    public interface IFileService
    {
        Task SaveAsync(string fileName,string extention, Stream content);
        Task<byte[]> GetAsync(string fileName, string extention);
    }
}
