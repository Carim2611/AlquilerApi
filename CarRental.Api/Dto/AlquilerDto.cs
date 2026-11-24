using System.ComponentModel.DataAnnotations;

namespace CarRental.Api.Dto;

public class AlquilerDto
{
    [Required]
    public int IdUsuario { get; set; }
    [Required]
    public int IdAuto { get; set; }
    [Required]
    public DateTime FechaInicio { get; set; }
    [Required]
    public DateTime FechaFin { get; set; }
}
