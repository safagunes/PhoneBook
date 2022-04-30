using MediatR;
using ReportService.Domain.Core.ResponseBases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain.Services.Contact.Requests
{
    public class GetContacts : PagedQuery
    {
        public override string OrderBy { get; set; } = "Id";
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Company { get; set; }
    }
}
