using AutoMapper;
using GlobalPayment.Integration.Api.V1.Models.dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalPayment.Integration.Api.MappingProfiles
{
    public class MerchantProfile : Profile
    {
        public MerchantProfile()
        {
            this.CreateMap<GlobalPayments.Api.Entities.Reporting.MerchantSummary, MerchantSummaryDto>()
                .ForMember(dest => dest.Status, input => input.MapFrom(i => i.Status.ToString()))
                .ForMember(dest => dest.Name, input => input.MapFrom(i => i.Name))
                .ForMember(dest => dest.Id, input => input.MapFrom(i => i.Id));
                


        }
    }
}
