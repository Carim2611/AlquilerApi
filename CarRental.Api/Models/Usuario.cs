using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Api.Models;

[Table("Usuario")]
public class Usuario
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("Nombre")]
    [MaxLength(20)]
    public string Nombre { get; set; }

    [Required]
    [Column("Correo")]
    [MaxLength(40)]
    public string Correo { get; set; }

    public ICollection<Alquiler> Alquileres {get; set;}
}