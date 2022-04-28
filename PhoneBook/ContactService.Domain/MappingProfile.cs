using AutoMapper;
using ContactService.Domain.Models;
using ContactService.Domain.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactService.Domain
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<CreateContact, Contact>();
            CreateMap<Contact, CreateContact>();
        }
    }
}
