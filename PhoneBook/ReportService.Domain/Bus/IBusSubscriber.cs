using ReportService.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Bus
{
    public interface IBusSubscriber
    {
        Task SubscribeAsync<T>() where T : class, new();
    }
}
