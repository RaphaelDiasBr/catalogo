using System.ComponentModel.DataAnnotations;

namespace Oportunity.Models;

public class ProductViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "O nome do produto é obrigatório")]
    [Display(Name = "Nome")]
    [MaxLength(150)]
    [MinLength(3)]
    public string? Name { get; set; }

    [Required]
    [Display(Name = "Preço")]
    [DisplayFormat(DataFormatString = "{0:N2}")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "A descrição do produto é obrigatória")]
    [Display(Name = "Descrição")]
    [MaxLength(250)]
    [MinLength(3)]
    public string? Description { get; set; }

    [Required]
    [Display(Name = "Estoque")]
    public long Stock { get; set; }

    public string? ImageURL { get; set; }

    [Display(Name = "Categorias")]
    public int CategoryId { get; set; }
    public CategoryViewModel? Category { get; set; }
}
