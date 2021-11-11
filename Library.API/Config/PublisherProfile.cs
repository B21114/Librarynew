using AutoMapper;
using Library.API.DTO;
using Library.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.API.Config
{
    public class PublisherProfile : Profile
    {
        public PublisherProfile()
        {
            CreateMap<PublisherDTO, Publisher>()
               .ForMember(dto => dto.Id, o2 => o2.MapFrom(o3 => Guid.NewGuid()))
               .ForMember(dto => dto.Name, o2 => o2.MapFrom(o3 => o3.Name))
               .ForMember(dto => dto.City, o2 => o2.MapFrom(o3 => o3.City));
        }
    }
}
