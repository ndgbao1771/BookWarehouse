using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityDTOs;
using BookWarehouse.DTO.Filters;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
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

        public List<LibrarianDTO> GetAll()
        {
            return _librarianRepository.FindAll().ProjectTo<LibrarianDTO>(_mapper.ConfigurationProvider).ToList();
        }

        public List<LibrarianDTO> GetByFilter(LibrarianFilter filter)
        {
            var query = _librarianRepository.GetQueryable();
            query = query.Where(x => filter.Id == null || x.Id == filter.Id)
                         .Where(x => string.IsNullOrEmpty(filter.Name) || x.Name == filter.Name);
            return query.ProjectTo<LibrarianDTO>(_mapper.ConfigurationProvider).ToList();
        }

        public LibrarianDTO GetById(int id)
        {
            return _mapper.Map<Librarian, LibrarianDTO>(_librarianRepository.FindById(id));
        }

        public List<LibrarianDTO> GetByName(string name)
        {
            return _mapper.Map<List<Librarian>, List<LibrarianDTO>>(_librarianRepository.GetByName(name).ToList());
        }

        public void Update(LibrarianDTO librarianDTO)
        {
            _librarianRepository.Updated(_mapper.Map<LibrarianDTO, Librarian>(librarianDTO));
            _librarianRepository.Commit();
        }
    }
}