using BookWarehouse.DTO.EntityViewSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BookWarehouse.DTO.Configurations.ViewSQL
{
    public class OrderViewSQLConfiguration : IEntityTypeConfiguration<OrderViewSQL>
    {
        public void Configure(EntityTypeBuilder<OrderViewSQL> builder)
        {
            builder.ToView("OrderViewSQL").HasNoKey();
            builder.Property(x => x.LibratianName).HasColumnName("LibratianName");
            builder.Property(x => x.MemberName).HasColumnName("MemberName");
            builder.Property(x => x.BookName).HasColumnName("BookName");
            builder.Property(x => x.DateCreated).HasColumnName("DateCreated");
            builder.Property(x => x.ExpectedReturnDate).HasColumnName("ExpectedReturnDate");
            builder.Property(x => x.ActualReturnDate).HasColumnName("ActualReturnDate");
            builder.Property(x => x.OrderStatus).HasColumnName("OrderStatus");
        }
    }
}