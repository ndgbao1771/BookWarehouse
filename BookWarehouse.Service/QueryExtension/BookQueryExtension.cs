using BookWarehouse.DTO.Entities;
using BookWarehouse.Service.Filters;
using System.Linq.Expressions;

namespace BookWarehouse.Service.QueryExtension
{
    public class BookQueryExtension
    {
        public static Expression<Func<Book, bool>> BookNameFilter(BookFilter filter)
        {
            Expression<Func<Book, bool>> query = string.IsNullOrEmpty(filter.Name) ? (Expression<Func<Book, bool>>)(v => true) : (v => v.Name.Contains(filter.Name));
            return query;
        }

        public static Expression<Func<Book, bool>> AuthorNameFilter(BookFilter filter)
        {
            Expression<Func<Book, bool>> query = string.IsNullOrEmpty(filter.Author) ? (Expression<Func<Book, bool>>)(v => true) : (v => v.author.Name.Contains(filter.Author));
            return query;
        }
    }
}