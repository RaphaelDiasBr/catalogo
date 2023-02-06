using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Oportunity.Models;

namespace Oportunity.Mapping;

public class CategoryMap: IEntityTypeConfiguration<CategoryViewModel>
{
    public void Configure(EntityTypeBuilder<CategoryViewModel> builder)
    {
        builder.ToTable("tbl_Category");

        builder.HasKey(c => c.CategoryId);
        builder.Property(c => c.Name).HasColumnType("VARCHAR(100)").IsRequired();
        builder.HasMany(p => p.Products).WithOne(c => c.Category).IsRequired().OnDelete(DeleteBehavior.Cascade);

        builder.HasData(
            new CategoryViewModel
            {
                CategoryId = 1,
                Name = "Smartphones"
            },
            new CategoryViewModel
            {
                CategoryId = 2,
                Name = "Veiculos"
            }
        );
    }
}