using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Library.Models;

namespace Library.Map
{
    public class BookProfile : Profile
    {
        /// <summary>
        /// Конструктор публичного экземпляра.
        /// </summary>
        public BookProfile()
        {
            /*  CreateMap<Book, BookDetailsDto>()
                     .ForMember(
                     destinationMember: dto => dto.BookFullInfo,
                     memberOptions: o2 => o2.MapFrom(o3 => string.Join(
                          " ",
                          "Book =",
                         $"Id = {o3.Id},",
                         $"Name = {o3.Name},",
                         $"NumberOfPages = {o3.NumberOfPages},"
                          )))
               
                     
                  .ForMember(
                      destinationMember: dto => dto.PublisherFullInfo,
                      memberOptions: o2 => o2.MapFrom(o3 => string.Join(
                          " ",
                          "Publisher =",
                          $"Id = {o3.Publisher.Id}",
                          $"Name = {o3.Publisher.Name},",
                          $"Sity = {o3.Publisher.Sity}")));*/
        }
    }
}
