using System.ComponentModel.DataAnnotations;

namespace CarRental.Api.Dto;

public class AutoDto
{
    public int Id { get; set; }
    [Required]
    public string Marca { get; set; }
    [Required]
    public string Modelo { get; set; }
    [Required]
    public decimal Precio { get; set; }
    [Required]
    public int Anio { get; set; }
}