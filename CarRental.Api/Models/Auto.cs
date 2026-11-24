using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Api.Models;

[Table("Auto")]
public class Auto
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("Marca")]
    [MaxLength(30)]
    public string Marca { get; set; }

    [Required]
    [Column("Modelo")]
    [MaxLength(30)]
    public string Modelo { get; set; }

    [Column("Precio", TypeName = "decimal(6,2)")]
    public decimal Precio { get; set; }

    [Required]
    [Column("Anio")]
    public int Anio { get; set; }

    public ICollection<Alquiler> Alquileres {get; set;}
}