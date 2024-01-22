using BookWarehouse.DTO.EntityViewSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookWarehouse.DTO.Configurations.ViewSQL
{
    public class LibratianViewSQLConfiguration : IEntityTypeConfiguration<LibratianViewSQL>
    {
        public void Configure(EntityTypeBuilder<LibratianViewSQL> builder)
        {
            builder.ToView("LibrarianViewSQL").HasNoKey();
            builder.Property(x => x.LibrarianName).HasColumnName("LibrarianName");
            builder.Property(x => x.DateCreated).HasColumnName("DateCreated");
            builder.Property(x => x.Creater).HasColumnName("Creater");
            builder.Property(x => x.DateUpdated).HasColumnName("DateUpdated");
            builder.Property(x => x.Updater).HasColumnName("Updater");
        }
    }
}