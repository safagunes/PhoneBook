using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Core.ResponseBases
{
    public abstract class PagedQuery
    {
        public abstract string OrderBy { get; set; }
        public string SortDirection { get; set; } = "asc";
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
