using ClosedXML.Excel;
using ReportService.Domain.Services.ExcelExport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Infrastructure.Services.ExcelExport
{
    public class ClosedXMLExcelExportService : IExcelExportService
    {
        public Stream Export(DataTable table)
        {
            using var ms = new MemoryStream();
            var wb = new XLWorkbook();
            var ds = new DataSet();
            ds.Tables.Add(table);
            wb.Worksheets.Add(ds);
            wb.SaveAs(ms);
            return ms;
        }
    }
}
