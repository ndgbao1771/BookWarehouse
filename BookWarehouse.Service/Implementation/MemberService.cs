﻿using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookWarehouse.DTO.Entities;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;
using BookWarehouse.Service.Interfaces;

namespace BookWarehouse.Service.Implementation
{
    public class MemberService : IMemberService
    {
        #region Contructor

        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        #endregion Contructor

        #region Method

        public MemberDTO Add(MemberDTO memberDTO)
        {
            _memberRepository.Add(_mapper.Map<MemberDTO, Member>(memberDTO));
            _memberRepository.Commit();
            return memberDTO;
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
                _memberRepository.Remove(id);
                _memberRepository.Commit();
            }
        }

        public List<MemberDTO> GetAll(MemberFilter filter)
        {
            var query = _memberRepository.GetAllByViewSql();
            query = query.Where(x => string.IsNullOrEmpty(filter.Name) || x.MemberName == filter.Name);
            return query.ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).ToList();
        }

        public MemberDTO GetById(int id)
        {
            return _mapper.Map<Member, MemberDTO>(_memberRepository.FindById(id));
        }

        public void Update(MemberDTO memberDTO)
        {
            var result = GetById(memberDTO.Id);
            if (result == null)
            {
                return;
            }
            else
            {
                _memberRepository.Updated(_mapper.Map<MemberDTO, Member>(memberDTO));
                _memberRepository.Commit();
            }
        }

        #endregion Method
    }
}