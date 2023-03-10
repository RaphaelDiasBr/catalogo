namespace Oportunity.Models;

public class CategoryViewModel
{
    public int CategoryId { get; set; }
    public string? Name { get; set; }
    public ICollection<ProductViewModel>? Products { get; set; }
}