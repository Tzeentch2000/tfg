
using AutoMapper;

namespace Mvc.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<User, UserForInsertDTO>().ReverseMap();
            CreateMap<User, UserForUpdateDTO>().ReverseMap();
        }
    }
}