using AutoMapper;
using DutchTreat.Data.Entities;
using DutchTreat.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DutchTreat.Data
{
    public class DutchMappingProfile : Profile
    {
        public DutchMappingProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(x => x.OrderId, ex => ex.MapFrom(o => o.Id)).ReverseMap();
            //Reverse is use for reverse mapping.
            CreateMap<OrderItem, OrderItemViewModel>().ReverseMap();
        }

    }
}
