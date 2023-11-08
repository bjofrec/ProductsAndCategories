#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ProductsAndCategories.Models;
public class Product
{    
    [Key]
    public int ProductId { get; set; } 
    [Required]   
    public string Name { get; set; } 
    [Required]    
    public string Description { get; set; }  
    [Required]   
    public double Price { get; set; }  

    public DateTime Fecha_Creacion { get; set; } = DateTime.Now;
    public DateTime Fecha_Actualizacion { get; set; } = DateTime.Now;

    public List<Association> Associat { get; set; } = new List<Association>();
}