using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookWarehouse.DTO.Entities;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;
using BookWarehouse.Service.Interfaces;

namespace BookWarehouse.Service.Implementation
{
    public class LibrarianService : ILibrarianService
    {
        private readonly ILibrarianRepository _librarianRepository;
        private readonly IMapper _mapper;

        public LibrarianService(ILibrarianRepository librarianRepository, IMapper mapper)
        {
            _librarianRepository = librarianRepository;
            _mapper = mapper;
        }

        public LibrarianDTO Add(LibrarianDTO librarianDTO)
        {
            _librarianRepository.Add(_mapper.Map<LibrarianDTO, Librarian>(librarianDTO));
            _librarianRepository.Commit();
            return librarianDTO;
        }

        public void Delete(int id)
        {
            _librarianRepository.Remove(id);
            _librarianRepository.Commit();
        }

        public List<LibrarianDTO> GetAll(LibrarianFilter filter)
        {
            var query = _librarianRepository.GetAllByViewSql();
            query = query.Where(x => string.IsNullOrEmpty(x.LibrarianName) || x.LibrarianName == filter.Name);
            return query.ProjectTo<LibrarianDTO>(_mapper.ConfigurationProvider).ToList();
        }

        public LibrarianDTO GetById(int id)
        {
            return _mapper.Map<Librarian, LibrarianDTO>(_librarianRepository.FindById(id));
        }

        public void Update(LibrarianDTO librarianDTO)
        {
            _librarianRepository.Updated(_mapper.Map<LibrarianDTO, Librarian>(librarianDTO));
            _librarianRepository.Commit();
        }
    }
}