using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Core.ResponseBases
{
    public abstract class PagedQuery
    {
        public abstract string OrderBy { get; set; }
        public bool IsAscending { get; set; } = true;
        public int? PageIndex { get; set; } = 0;
        public int? PageSize { get; set; } = 1000;
    }
}
