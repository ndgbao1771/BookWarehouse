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

            CreateMap<Order, StatisticsDTO>().ForMember(dest => dest.BorrowerName, otp => otp.MapFrom(x => x.member.Name))
                                             .ForMember(dest => dest.LibrarianName, otp => otp.MapFrom(x => x.librarian.Name))
                                             .ForMember(dest => dest.Status, otp => otp.MapFrom(x => x.Status))
                                             .ForMember(dest => dest.BookName, otp => otp.MapFrom(x => x.orderDetails.Select(x => x.book.Name).FirstOrDefault()))
                                             .ForMember(dest => dest.ExpectedReturnDate, otp => otp.MapFrom(x => x.orderDetails.Select(x => x.DateGiveExpect).FirstOrDefault()))
                                             .ForMember(dest => dest.ActualReturnDate, otp => otp.MapFrom(x => x.orderDetails.Select(x => x.DateGiveCurrent).FirstOrDefault()));

            CreateMap<OrderDetail, OrderDetailDTO>();

            CreateMap<Librarian, LibrarianDTO>();
        }
    }
}