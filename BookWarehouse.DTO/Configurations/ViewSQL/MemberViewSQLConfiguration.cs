using BookWarehouse.DTO.EntityViewSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookWarehouse.DTO.Configurations.ViewSQL
{
    public class MemberViewSQLConfiguration : IEntityTypeConfiguration<MemberViewSQL>
    {
        public void Configure(EntityTypeBuilder<MemberViewSQL> builder)
        {
            builder.ToView("MemberViewSQL").HasNoKey();
            builder.Property(x => x.MemberName).HasColumnName("MemberName");
            builder.Property(x => x.DateCreated).HasColumnName("DateCreated");
            builder.Property(x => x.Creater).HasColumnName("Creater");
            builder.Property(x => x.DateUpdated).HasColumnName("DateUpdated");
            builder.Property(x => x.Updater).HasColumnName("Updater");
        }
    }
}