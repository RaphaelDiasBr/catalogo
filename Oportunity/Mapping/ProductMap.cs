using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oportunity.Models;

namespace Oportunity.Mapping;

public class ProductMap : IEntityTypeConfiguration<ProductViewModel>
{
    public void Configure(EntityTypeBuilder<ProductViewModel> builder)
    {
        builder.ToTable("tbl_Product");
    }
}