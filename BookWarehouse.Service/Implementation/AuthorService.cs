using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookWarehouse.DTO.Entities;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;
using BookWarehouse.Service.Interfaces;

namespace BookWarehouse.Service.Implementation
{
    public class AuthorService : IAuthorService
    {
        #region Contructor

        private readonly IAuthorRepository _authorRepository;
        private readonly IMapper _mapper;

        public AuthorService(IAuthorRepository authorRepository, IMapper mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }

        #endregion Contructor

        #region Method

        public List<AuthorDTO> GetByFilter(AuthorFilter filter)
        {
            var query = _authorRepository.GetQueryable();
            query = query.Where(x => string.IsNullOrEmpty(filter.Name) || x.AuthorName == filter.Name);
            return query.ProjectTo<AuthorDTO>(_mapper.ConfigurationProvider).ToList();
        }

        public AuthorDTO GetById(int id)
        {
            return _mapper.Map<Author, AuthorDTO>(_authorRepository.FindById(id));
        }

        public AuthorDTO Add(AuthorDTO authorDTO)
        {
            _authorRepository.Add(_mapper.Map<AuthorDTO, Author>(authorDTO));
            _authorRepository.Commit();
            return authorDTO;
        }

        public void Delete(int id)
        {
            var result = GetById(id);
            if (result == null)
            {
                return;
            }
            else
            {
                _authorRepository.Remove(id);
                _authorRepository.Commit();
            }
        }

        public void Update(AuthorDTO authorDTO)
        {
            var result = GetById(authorDTO.Id);
            if (result == null)
            {
                return;
            }
            else
            {
                _authorRepository.Updated(_mapper.Map<AuthorDTO, Author>(authorDTO));
                _authorRepository.Commit();
            }
        }

        #endregion Method
    }
}