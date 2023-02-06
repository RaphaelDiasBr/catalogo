using Microsoft.EntityFrameworkCore;
using Oportunity.Mapping;
using Oportunity.Models;

namespace Oportunity.Context;

public class ProductContext: DbContext
{
    public ProductContext(DbContextOptions<ProductContext> options) : base(options) { }

    public DbSet<CategoryViewModel> Categories { get; set; }
    public DbSet<ProductViewModel> Products { get; set; }

    // Fluent API (Substitui o DataAnnotation)
    protected override void OnModelCreating(ModelBuilder mb)
    {
        mb.ApplyConfiguration(new CategoryMap());
        mb.ApplyConfiguration(new ProductMap());
    }
}