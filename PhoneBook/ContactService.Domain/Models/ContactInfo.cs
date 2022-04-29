using ContactService.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain.Models
{
    public class ContactInfo
    {
        [Key]
        public Guid Id { get; set; } 
        public Guid ContactId { get; set; }
        public ContactInfoType Type { get; set; }
        public string Content { get; set; }
        public Contact Contact { get; set; }
    }
}
