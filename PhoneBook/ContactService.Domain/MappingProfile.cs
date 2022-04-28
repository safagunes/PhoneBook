using AutoMapper;
using ContactService.Domain.Dtos;
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

            CreateMap<CreateContactInfo, ContactInfo>();
            CreateMap<ContactInfo, CreateContactInfo>();

            CreateMap<ContactDetailDto, Contact>();
            CreateMap<Contact, ContactDetailDto>();

            CreateMap<ContactDto, Contact>();
            CreateMap<Contact, ContactDto>();

            CreateMap<ContactInfoDto, ContactInfo>();
            CreateMap<ContactInfo, ContactInfoDto>();


        }
    }
}
