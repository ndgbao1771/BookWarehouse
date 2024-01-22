using BookWarehouse.DTO.EntityViewSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookWarehouse.DTO.Configurations.ViewSQL
{
    public class CategoryViewSQLCOnfiguration : IEntityTypeConfiguration<CategoryViewSQL>
    {
        public void Configure(EntityTypeBuilder<CategoryViewSQL> builder)
        {
            builder.ToView("CategoryViewSQL").HasNoKey();
            builder.Property(x => x.CategoryName).HasColumnName("CategoryName");
            builder.Property(x => x.DateCreated).HasColumnName("DateCreated");
            builder.Property(x => x.Creater).HasColumnName("Creater");
            builder.Property(x => x.DateUpdated).HasColumnName("DateUpdated");
            builder.Property(x => x.Updater).HasColumnName("Updater");
        }
    }
}