using AutoMapper;
using AutoMapper.QueryableExtensions;
using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityDTOs;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookWarehouse.Service.Implementation
{
    public class MemberService : IMemberService
    {
        private readonly IMemberRepository _memberRepository;
        private readonly IMapper _mapper;

        public MemberService(IMemberRepository memberRepository, IMapper mapper)
        {
            _memberRepository = memberRepository;
            _mapper = mapper;
        }

        public MemberDTO Add(MemberDTO memberDTO)
        {
            _memberRepository.Add(_mapper.Map<MemberDTO, Member>(memberDTO));
            _memberRepository.Commit();
            return memberDTO;
        }

        public void Delete(int id)
        {
            _memberRepository.Remove(id);
            _memberRepository.Commit();
        }

        public List<MemberDTO> GetAll()
        {
            return _memberRepository.FindAll().ProjectTo<MemberDTO>(_mapper.ConfigurationProvider).ToList();
        }

        public MemberDTO GetById(int id)
        {
            return _mapper.Map<Member, MemberDTO>(_memberRepository.FindById(id));
        }

        public List<MemberDTO> GetByName(string name)
        {
            return _mapper.Map<List<Member>, List<MemberDTO>>(_memberRepository.GetByName(name).ToList());
        }

        public void Update(MemberDTO memberDTO)
        {
            _memberRepository.Updated(_mapper.Map<MemberDTO, Member>(memberDTO));
            _memberRepository.Commit();
        }
    }
}
