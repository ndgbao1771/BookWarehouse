﻿using AutoMapper;
using BookWarehouse.DTO.Entities;
using BookWarehouse.Service.EntityDTOs;

namespace BookWarehouse.Service.AutoMappers
{
    public class DTOsToDomain : Profile
    {
        public DTOsToDomain()
        {
            CreateMap<BookDTO, Book>()
                .ConstructUsing(c => new Book(c.Name, c.CreatedAt, c.CreatedBy, c.UpdatedAt, c.UpdatedBy));

            CreateMap<BookUpdateDTO, Book>()
                .ConstructUsing(c => new Book(c.Name, c.CreatedAt, c.CreatedBy, c.UpdatedAt, c.UpdatedBy, c.AuthorId, c.BookCategoryId))
                .ForMember(dest => dest.bookDetails, opt => opt.MapFrom(src => new List<BookDetail>
                                                                                {
                                                                                    new BookDetail
                                                                                    {
                                                                                        Seri = src.Seri,
                                                                                        BookId = src.Id
                                                                                    }
                                                                                 }));

            CreateMap<AuthorDTO, Author>()
                .ConstructUsing(c => new Author(c.Name, c.CreatedAt, c.CreatedBy, c.UpdatedAt, c.UpdatedBy));

            CreateMap<BookCategoryDTO, BookCategory>()
                .ConstructUsing(c => new BookCategory(c.Name, c.CreatedAt, c.CreatedBy, c.UpdatedAt, c.UpdatedBy));

            CreateMap<BookDetailDTO, BookDetail>()
                .ConstructUsing(c => new BookDetail(c.BookId, c.Seri));

            CreateMap<MemberDTO, Member>()
                .ConstructUsing(c => new Member(c.Name, c.CreatedAt, c.CreatedBy, c.UpdatedAt, c.UpdatedBy));

            CreateMap<OrderAddDTO, Order>()
                .ConstructUsing(c => new Order(c.MemberId, c.LibrarianId, c.DateCreated, c.Status))
                .ForMember(dest => dest.orderDetails, otp => otp.MapFrom(x => new List<OrderDetail>
                                                                               {
                                                                                   new OrderDetail
                                                                                   {
                                                                                       OrderId = x.Id,
                                                                                       BookId = x.BookId,
                                                                                       DateGiveExpect = x.DateGiveExpect,
                                                                                       DateGiveCurrent = x.DateGiveCurent
                                                                                   }
                                                                                }));
            CreateMap<OrderUpdateDTO, Order>()
                .ForMember(x => x.LibrarianId, opt => opt.Ignore())
                .ForMember(x => x.MemberId, opt => opt.Ignore())
                .ConstructUsing(x => new Order(x.Status));

            CreateMap<LibrarianDTO, Librarian>()
               .ConstructUsing(c => new Librarian(c.Name, c.CreatedAt, c.CreatedBy, c.UpdatedAt, c.UpdatedBy));
        }
    }
}