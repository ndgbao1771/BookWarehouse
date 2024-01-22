using BookWarehouse.DTO;
using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityViewSQL;
using BookWarehouse.Repository.Interfaces.IBookWarehouseRepositories;
using BookWarehouse.Repository.Repositories.Shared;

namespace BookWarehouse.Repository.Repositories.BookWarehouseRepositories
{
    public class MemberRepository : Repository<Member, int>, IMemberRepository
    {
        private readonly AppDbContext _context;

        public MemberRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public IQueryable<MemberViewSQL> GetAllByViewSql()
        {
            return _context.memberViewSQLs;
        }

        public IQueryable<Member> GetByName(string name)
        {
            var datas = _context.Members.Where(x => x.Name.Contains(name));
            return datas;
        }

        public IQueryable<Member> GetQueryable()
        {
            var query = _context.Members.AsQueryable();
            return query;
        }
    }
}