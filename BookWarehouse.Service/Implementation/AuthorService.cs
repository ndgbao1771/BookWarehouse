﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityDTOs;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Service.Interfaces;

namespace BookWarehouse.Service.Implementation
{
    public class AuthorService : IAuthorService
    {
        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        public AuthorDTO Add(AuthorDTO authorDTO)
        {
            _authorRepository.Add(_mapper.Map<AuthorDTO, Author>(authorDTO));
            _authorRepository.Commit();
            return authorDTO;
        }

        public void Delete(int id)
        {
            _authorRepository.Remove(id);
            _authorRepository.Commit();
        }

        public List<AuthorDTO> GetAll()
        {
            return _authorRepository.FindAll().ProjectTo<AuthorDTO>(_mapper.ConfigurationProvider).ToList();
        }

        public AuthorDTO GetById(int id)
        {
            return _mapper.Map<Author, AuthorDTO>(_authorRepository.FindById(id));
        }

        public List<AuthorDTO> GetByName(string name)
        {
            return _mapper.Map<List<Author>, List<AuthorDTO>>(_authorRepository.GetByName(name).ToList());
        }

        public void Update(AuthorDTO authorDTO)
        {
            _authorRepository.Updated(_mapper.Map<AuthorDTO, Author>(authorDTO));
            _authorRepository.Commit();
        }
    }
}