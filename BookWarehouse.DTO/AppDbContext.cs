using BookWarehouse.DTO.Configuration;
using BookWarehouse.DTO.Configurations.ViewSQL;
using BookWarehouse.DTO.Entities;
using BookWarehouse.DTO.EntityViewSQL;
using Microsoft.EntityFrameworkCore;

namespace BookWarehouse.DTO
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Entity

            modelBuilder.ApplyConfiguration(new AuthorConfiguration());
            modelBuilder.ApplyConfiguration(new BookConfiguration());
            modelBuilder.ApplyConfiguration(new BookCategoryConfiguration());
            modelBuilder.ApplyConfiguration(new BookDetailConfiguration());
            modelBuilder.ApplyConfiguration(new LibrarianConfiguration());
            modelBuilder.ApplyConfiguration(new MemberConfiguration());
            modelBuilder.ApplyConfiguration(new OrderConfiguration());
            modelBuilder.ApplyConfiguration(new OrderDetailConfiguration());

            #endregion Entity

            #region Entity ViewSQL

            modelBuilder.ApplyConfiguration(new AuthorViewSQLConfiguration());
            modelBuilder.ApplyConfiguration(new BookViewSQLConfiguration());
            modelBuilder.ApplyConfiguration(new CategoryViewSQLCOnfiguration());
            modelBuilder.ApplyConfiguration(new LibratianViewSQLConfiguration());
            modelBuilder.ApplyConfiguration(new MemberViewSQLConfiguration());
            modelBuilder.ApplyConfiguration(new OrderViewSQLConfiguration());

            #endregion Entity ViewSQL

            base.OnModelCreating(modelBuilder);
        }

        #region DbSet<> Entity

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BookCategory> BookCategories { get; set; }
        public DbSet<BookDetail> BookDetails { get; set; }
        public DbSet<Librarian> Librarians { get; set; }
        public DbSet<Member> Members { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        #endregion DbSet<> Entity

        #region DbSet<> Entity ViewSQL

        public DbSet<AuthorViewSQL> authorViewSQLs { get; set; }
        public DbSet<BookViewSQL> bookViewSQLs { get; set; }
        public DbSet<CategoryViewSQL> categoryViewSQLs { get; set; }
        public DbSet<LibratianViewSQL> libratianViewSQLs { get; set; }
        public DbSet<MemberViewSQL> memberViewSQLs { get; set; }
        public DbSet<OrderViewSQL> orderViewSQLs { get; set; }

        #endregion DbSet<> Entity ViewSQL
    }
}