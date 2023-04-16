
using AutoMapper;

namespace Mvc.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Category, CategoryDTO>().ReverseMap();
            CreateMap<Book, BookDTO>().ReverseMap();
            CreateMap<Book,BookForInsertDTO>().ReverseMap();
            CreateMap<User, UserForInsertDTO>().ReverseMap();
            CreateMap<User, UserForUpdateDTO>().ReverseMap();
            CreateMap<Order, OrderForInsertDTO>().PreserveReferences();
            CreateMap<OrderForInsertDTO, Order>().PreserveReferences();
            CreateMap<User, UserResultDTO>().ReverseMap();
            //CreateMap<List<Order>, List<OrderForInsertDTO>>().ReverseMap();
        }
    }
}