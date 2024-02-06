using AutoMapper;
using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityViewSQL;
using BookWarehouse.Service.EntityDTOs;


namespace BookWarehouse.Service.AutoMappers
{
    public class DomainToDTOs : Profile
    {
        public DomainToDTOs()
        {
            CreateMap<AppUser, AppUserDTO>();
            CreateMap<Book, BookDTO>().ForMember(dest => dest.Seri, opt => opt.MapFrom(src => src.bookDetails != null
                                                                     && src.bookDetails.Any() ? src.bookDetails.First().Seri : null))
                                      .ForMember(dest => dest.Author, opt => opt.MapFrom(x => x.author.Name))
                                      .ForMember(dest => dest.BookCategory, opt => opt.MapFrom(x => x.bookCategory.Name));
            
            CreateMap<BookViewSQL, BookDTO>().ForMember(dest => dest.Name, otp => otp.MapFrom(x => x.BookName))
                                             .ForMember(dest => dest.Author, otp => otp.MapFrom(x => x.AuthorName))
                                             .ForMember(dest => dest.Seri, otp => otp.MapFrom(x => x.BookSeri))
                                             .ForMember(dest => dest.BookCategory, otp => otp.MapFrom(x => x.Category))
                                             .ForMember(dest => dest.CreatedAt, otp => otp.MapFrom(x => x.DateCreated))
                                             .ForMember(dest => dest.CreatedBy, otp => otp.MapFrom(x => x.Creater))
                                             .ForMember(dest => dest.UpdatedAt, otp => otp.MapFrom(x => x.DateUpdated))
                                             .ForMember(dest => dest.UpdatedBy, otp => otp.MapFrom(x => x.Updater));
            CreateMap<Book, BookUpdateDTO>();

            CreateMap<Author, AuthorDTO>();
            CreateMap<AuthorViewSQL, AuthorDTO>().ForMember(dest => dest.Name, otp => otp.MapFrom(x => x.AuthorName))
                                                 .ForMember(dest => dest.CreatedAt, otp => otp.MapFrom(x => x.DateCreated))
                                                 .ForMember(dest => dest.CreatedBy, otp => otp.MapFrom(x => x.Creater))
                                                 .ForMember(dest => dest.UpdatedAt, otp => otp.MapFrom(x => x.DateUpdated))
                                                 .ForMember(dest => dest.UpdatedBy, otp => otp.MapFrom(x => x.Updater));

            CreateMap<BookCategory, BookCategoryDTO>();
            CreateMap<CategoryViewSQL, BookCategoryDTO>().ForMember(dest => dest.Name, otp => otp.MapFrom(x => x.CategoryName))
                                                      .ForMember(dest => dest.CreatedAt, otp => otp.MapFrom(x => x.DateCreated))
                                                      .ForMember(dest => dest.CreatedBy, otp => otp.MapFrom(x => x.Creater))
                                                      .ForMember(dest => dest.UpdatedAt, otp => otp.MapFrom(x => x.DateUpdated))
                                                      .ForMember(dest => dest.UpdatedBy, otp => otp.MapFrom(x => x.Updater));

            CreateMap<BookDetail, BookDetailDTO>();

            CreateMap<Member, MemberDTO>();
            CreateMap<MemberViewSQL, MemberDTO>().ForMember(dest => dest.Name, otp => otp.MapFrom(x => x.MemberName))
                                                      .ForMember(dest => dest.CreatedAt, otp => otp.MapFrom(x => x.DateCreated))
                                                      .ForMember(dest => dest.CreatedBy, otp => otp.MapFrom(x => x.Creater))
                                                      .ForMember(dest => dest.UpdatedAt, otp => otp.MapFrom(x => x.DateUpdated))
                                                      .ForMember(dest => dest.UpdatedBy, otp => otp.MapFrom(x => x.Updater));

            CreateMap<Order, OrderDTO>().ForMember(dest => dest.MemberName, otp => otp.MapFrom(x => x.member.Name))
                                        .ForMember(dest => dest.LibrarianName, otp => otp.MapFrom(x => x.librarian.Name))
                                        .ForMember(dest => dest.orderDetails, otp => otp.MapFrom(x => x.orderDetails));
            CreateMap<OrderViewSQL, OrderDTO>().ForMember(dest => dest.LibrarianName, otp => otp.MapFrom(x => x.LibratianName))
                                               /*.ForMember(dest => dest.MemberName, otp => otp.MapFrom(x => x.MemberName))*/
                                               .ForMember(dest => dest.orderDetails, opt => opt.MapFrom(src => new List<OrderDetail>()));
                                               /*.ForMember(dest => dest.DateCreated, otp => otp.MapFrom(x => x.DateCreated))
                                               .ForMember(dest => dest.DateGiveExpect, otp => otp.MapFrom(x => x.ExpectedReturnDate))
                                               .ForMember(dest => dest.DateGiveCurent, otp => otp.MapFrom(x => x.ActualReturnDate))
                                               .ForMember(dest => dest.Status, otp => otp.MapFrom(x => x.OrderStatus));*/

            CreateMap<Order, StatisticsDTO>().ForMember(dest => dest.BorrowerName, otp => otp.MapFrom(x => x.member.Name))
                                             .ForMember(dest => dest.LibrarianName, otp => otp.MapFrom(x => x.librarian.Name))
                                             .ForMember(dest => dest.Status, otp => otp.MapFrom(x => x.Status))
                                             .ForMember(dest => dest.BookName, otp => otp.MapFrom(x => x.orderDetails.Select(x => x.book.Name).FirstOrDefault()))
                                             .ForMember(dest => dest.ExpectedReturnDate, otp => otp.MapFrom(x => x.orderDetails.Select(x => x.DateGiveExpect).FirstOrDefault()))
                                             .ForMember(dest => dest.ActualReturnDate, otp => otp.MapFrom(x => x.orderDetails.Select(x => x.DateGiveCurrent).FirstOrDefault()));

            CreateMap<OrderDetail, OrderDetailDTO>();

            CreateMap<Librarian, LibrarianDTO>();
            CreateMap<LibratianViewSQL, LibrarianDTO>().ForMember(dest => dest.Name, otp => otp.MapFrom(x => x.LibrarianName))
                                                       .ForMember(dest => dest.CreatedAt, otp => otp.MapFrom(x => x.DateCreated))
                                                       .ForMember(dest => dest.CreatedBy, otp => otp.MapFrom(x => x.Creater))
                                                       .ForMember(dest => dest.UpdatedAt, otp => otp.MapFrom(x => x.DateUpdated))
                                                       .ForMember(dest => dest.UpdatedBy, otp => otp.MapFrom(x => x.Updater));
        }
    }
}