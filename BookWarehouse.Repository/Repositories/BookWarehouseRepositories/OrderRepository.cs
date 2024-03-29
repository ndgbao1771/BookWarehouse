﻿using BookWarehouse.DTO;
using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityViewSQL;
using BookWarehouse.DTO.Enums;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Repository.Repositories.Shared;
using Microsoft.EntityFrameworkCore;

namespace BookWarehouse.Repository.Repositories.BookWarehouseRepositories
{
    public class OrderRepository : Repository<Order, int>, IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<Order> GetById(int id)
        {
            var datas = _context.Orders.Where(x => x.Id == id)
                                       .Include(x => x.librarian)
                                       .Include(x => x.member)
                                       .Include(x => x.orderDetails).ThenInclude(x => x.book);
            return datas;
        }

        public IQueryable<Order> GetListBookProgressOfMember(int id)
        {
            var datas = _context.Orders.Where(x => x.MemberId == id && x.Status == StatusAble.Progress)
                                       .Include(x => x.librarian)
                                       .Include(x => x.member)
                                       .Include(x => x.orderDetails).ThenInclude(x => x.book);
            return datas;
        }

        public IQueryable<Order> GetStatistics(DateTime dateStart, DateTime dateEnd)
        {
            var datas = _context.Orders.Where(x => x.DateCreated >= dateStart && x.DateCreated <= dateEnd)
                                       .Include(x => x.librarian)
                                       .Include(x => x.member)
                                       .Include(x => x.orderDetails).ThenInclude(x => x.book);
            return datas;
        }

        public IQueryable<Order> GetBooksBorrowedInMonth(DateTime DateStart, DateTime DateEnd)
        {
            var datas = _context.Orders.Where(x => x.DateCreated >= DateStart && x.DateCreated <= DateEnd);
            return datas;
        }

        public IQueryable<OrderViewSQL> GetAllByViewSql()
        {
            return _context.orderViewSQLs;
        }
    }
}