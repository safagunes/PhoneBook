using AutoMapper;
using ReportService.Domain.Dtos;
using ReportService.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportService.Domain
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<ReportDto, Report>();
            CreateMap<Report, ReportDto>();

            CreateMap<ReportDetailDto, ReportDetail>();
            CreateMap<ReportDetail, ReportDetailDto>();
        }
    }
}
