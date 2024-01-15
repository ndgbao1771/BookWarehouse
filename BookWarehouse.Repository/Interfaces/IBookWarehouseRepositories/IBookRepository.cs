﻿using BookWarehouse.DTO.Entities;
using BookWarehouse.Repository.Interfaces.Shared;

namespace BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories
{
    public interface IBookRepository : IRepository<Book, int>
    {
        Book GetBySeri(string keyword);
    }
}