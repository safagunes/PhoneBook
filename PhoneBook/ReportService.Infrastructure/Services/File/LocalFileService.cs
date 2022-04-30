using ReportService.Domain.Core.Exceptions;
using ReportService.Domain.Services.File;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Infrastructure.Services.File
{
    public class LocalFileService : IFileService
    {
        public async Task<byte[]> GetAsync(string fileName, string extention)
        {
            try
            {
                return await System.IO.File.ReadAllBytesAsync(fileName + extention);
            }
            catch (Exception ex)
            {
                throw new ServiceException(500, "FileService Get Error", ex);
            }
            
        }

        public async Task SaveAsync(string fileName, string extention, Stream content)
        {
            try
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/files", fileName + extention);
                using var stream = new FileStream(path, FileMode.Create);
                await stream.CopyToAsync(stream);
            }
            catch (Exception ex)
            {
                throw new ServiceException(500, "FileService Save Error", ex);
            }

        }
    }
}
