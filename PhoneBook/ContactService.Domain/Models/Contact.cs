using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Models
{
    public class Contact
    {
        public Contact()
        {
            ContactInfos = new HashSet<ContactInfo>();
        }
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Company { get; set; }
        public ICollection<ContactInfo> ContactInfos { get; set; }
    }
}
