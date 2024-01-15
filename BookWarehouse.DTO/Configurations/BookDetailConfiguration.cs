using BookWarehouse.DTO.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookWarehouse.DTO.Configuration
{
    public class BookDetailConfiguration : IEntityTypeConfiguration<BookDetail>
    {
        public void Configure(EntityTypeBuilder<BookDetail> builder)
        {
            builder.ToTable("BookDetails");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Seri).HasMaxLength(15).IsRequired();
        }
    }
}