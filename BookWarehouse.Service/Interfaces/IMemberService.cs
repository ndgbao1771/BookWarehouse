using BookWarehouse.Service.EntityDTOs;
using BookWarehouse.Service.Filters;


namespace BookWarehouse.Service.Interfaces
{
    public interface IMemberService
    {
        List<MemberDTO> GetAll();

        List<MemberDTO> GetByName(string name);

        List<MemberDTO> GetByFilter(MemberFilter filter);

        MemberDTO GetById(int id);

        MemberDTO Add(MemberDTO memberDTO);

        void Update(MemberDTO memberDTO);

        void Delete(int id);
    }
}