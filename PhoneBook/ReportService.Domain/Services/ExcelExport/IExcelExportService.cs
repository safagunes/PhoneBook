using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Services.ExcelExport
{
    public interface IExcelExportService
    {
        void Export(DataTable table, string fileName);
    }
}
