using Ajmera_Api.Models;
using Ajmera_Core.Domain;
using AutoMapper;

namespace Ajmera_Api.Mapper
{
    public class BookMapper : Profile
    {
        public BookMapper()
        {
            CreateMap<Book_DTO, Book>().IgnoreAllPropertiesWithAnInaccessibleSetter();
            CreateMap<Book, Book_DTO>().IgnoreAllPropertiesWithAnInaccessibleSetter(); ;
        }
    }
}
