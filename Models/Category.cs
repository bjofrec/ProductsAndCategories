#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProductsAndCategories.Models;
public class Category
{    
    [Key]
    public int CategoryId { get; set; }  
    [Required]  
    public string Name { get; set; } 

    public DateTime Fecha_Creacion { get; set; } = DateTime.Now;
    public DateTime Fecha_Actualizacion { get; set; } = DateTime.Now;

    public List<Association> Associations { get; set; } = new List<Association>();
}