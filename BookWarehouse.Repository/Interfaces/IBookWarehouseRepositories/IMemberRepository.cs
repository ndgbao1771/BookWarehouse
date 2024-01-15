using BookWarehouse.DTO.Entities;
using BookWarehouse.Repository.Interfaces.Shared;

namespace BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories
{
    public interface IMemberRepository : IRepository<Member, int>
    {
        IQueryable<Member> GetByName(string name);
    }
}