using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Configuration.Conventions;
using Library.API.DTO;
using Library.Models;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Library.API.Config
{
    public class BookProfile : Profile
    {
        /// <summary>
        /// Конструктор публичного экземпляра.
        /// </summary>
        public BookProfile()
        {

            {
                CreateMap<BookDTO, Book>()
                   .ForMember(dto => dto.Id, o2 => o2.MapFrom(o3 => Guid.NewGuid()))
                   .ForMember(dto => dto.Name, o2 => o2.MapFrom(o3 => o3.Name))
                   .ForMember(dto => dto.NumberOfPages, o2 => o2.MapFrom(o3 => o3.NumberOfPages))
                   .ForMember(dto => dto.Authors, o2 => o2.MapFrom(x => x.AuthorsId.Select(y => new Author { Id = y })))
                   .ForMember(dto => dto.Publisher, o2 => o2.MapFrom(o3 => new Publisher { Id = o3.PublisherId }));
            }
        }
    }
}
