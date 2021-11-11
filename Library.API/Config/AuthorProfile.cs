using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library.API.DTO;
using Library.Models;

namespace Library.API.Config
{
    public class AuthorProfile : Profile
    {
        /// <summary>
        /// Конструктор публичного экземпляра.
        /// </summary>
        public AuthorProfile()
        {
            CreateMap<AuthorDTO, Author>()
                   .ForMember(dto => dto.Id, o2 => o2.MapFrom(o3 => Guid.NewGuid()))
                   .ForMember(dto => dto.Lastname, o2 => o2.MapFrom(o3 => o3.Lastname))
                   .ForMember(dto => dto.Firstname, o2 => o2.MapFrom(o3 => o3.Firstname))
                   .ForMember(dto => dto.Patronymic, o2 => o2.MapFrom(o3 => o3.Patronymic))
                   .ForMember(dto => dto.Activity, o2 => o2.MapFrom(o3 => o3.Activity));                               
            }
    }
}
