using AutoMapper;
using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityDTOs;

namespace BookWarehouse.Service.AutoMappers
{
    public class DomainToDTOs : Profile
    {
        public DomainToDTOs()
        {
            CreateMap<Book, BookDTO>().ForMember(dest => dest.Seri, opt => opt.MapFrom(src => src.bookDetails != null 
                                                                     && src.bookDetails.Any() ? src.bookDetails.First().Seri : null))
                                      .ForMember(dest => dest.Author, opt => opt.MapFrom(x => x.author.Name))
                                      .ForMember(dest => dest.BookCategory, opt => opt.MapFrom(x => x.bookCategory.Name));
            CreateMap<Book, BookUpdateDTO>();

            CreateMap<Author, AuthorDTO>();

            CreateMap<BookCategory, BookCategoryDTO>();

            CreateMap<BookDetail, BookDetailDTO>();

            CreateMap<Member, MemberDTO>();

            CreateMap<Order, OrderDTO>().ForMember(dest => dest.MemberName, otp => otp.MapFrom(x => x.member.Name))
                                        .ForMember(dest => dest.LibrarianName, otp => otp.MapFrom(x => x.librarian.Name))
                                        .ForMember(dest => dest.orderDetails, otp => otp.MapFrom(x => x.orderDetails));
            CreateMap<OrderDetail, OrderDetailDTO>();

            CreateMap<Librarian, LibrarianDTO>();
        }
    }
}