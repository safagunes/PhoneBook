using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Core.ResponseBases
{
    public class PagedData<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
    }
}
