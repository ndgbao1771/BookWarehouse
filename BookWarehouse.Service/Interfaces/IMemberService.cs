using BookWarehouse.DTO.EntityDTOs;

namespace BookWarehouse.Service.Interfaces
{
    public interface IMemberService
    {
        List<MemberDTO> GetAll();

        List<MemberDTO> GetByName(string name);

        MemberDTO GetById(int id);

        MemberDTO Add(MemberDTO memberDTO);

        void Update(MemberDTO memberDTO);

        void Delete(int id);
    }
}