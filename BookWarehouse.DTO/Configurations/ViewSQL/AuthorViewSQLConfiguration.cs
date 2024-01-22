using BookWarehouse.DTO.EntityViewSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookWarehouse.DTO.Configurations.ViewSQL
{
    public class AuthorViewSQLConfiguration : IEntityTypeConfiguration<AuthorViewSQL>
    {
        public void Configure(EntityTypeBuilder<AuthorViewSQL> builder)
        {
            builder.HasNoKey().ToView("AuthorViewSQL");
            builder.Property(x => x.AuthorName).HasColumnName("AuthorName");
            builder.Property(x => x.Creater).HasColumnName("Creater");
            builder.Property(x => x.Updater).HasColumnName("Updater");
            builder.Property(x => x.DateCreated).HasColumnName("DateCreated");
            builder.Property(x => x.DateUpdated).HasColumnName("DateUpdated");
        }
    }
}