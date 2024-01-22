﻿using BookWarehouse.DTO.EntityDTOs;
using BookWarehouse.DTO.Filters;

namespace BookWarehouse.Service.Interfaces
{
    public interface ILibrarianService
    {
        List<LibrarianDTO> GetAllByViewSql();
        List<LibrarianDTO> GetAll();

        LibrarianDTO GetById(int id);

        List<LibrarianDTO> GetByName(string name);

        List<LibrarianDTO> GetByFilter(LibrarianFilter filter);

        LibrarianDTO Add(LibrarianDTO librarianDTO);

        void Update(LibrarianDTO librarianDTO);

        void Delete(int id);
    }
}