using BookWarehouse.DTO.EntityViewSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookWarehouse.DTO.Configurations.ViewSQL
{
    public class BookViewSQLConfiguration : IEntityTypeConfiguration<BookViewSQL>
    {
        public void Configure(EntityTypeBuilder<BookViewSQL> builder)
        {
            builder.HasNoKey().ToView("BookViewSQL");
            builder.Property(x => x.BookName).HasColumnName("BookName");
            builder.Property(x => x.AuthorName).HasColumnName("AuthorName");
            builder.Property(x => x.BookSeri).HasColumnName("BookSeri");
            builder.Property(x => x.Category).HasColumnName("Category");
            builder.Property(x => x.DateCreated).HasColumnName("DateCreated");
            builder.Property(x => x.Creater).HasColumnName("Creater");
            builder.Property(x => x.DateUpdated).HasColumnName("DateUpdated");
            builder.Property(x => x.Updater).HasColumnName("Updater");
        }
    }
}