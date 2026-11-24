using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRental.Api.Models;

[Table("Alquiler")]
public class Alquiler
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    [Required]
    [Column("IdUsuario")]
    public int IdUsuario { get; set; }

    [Column("Usuario")]
    public Usuario Usuario { get; set; }

    [Required]
    [Column("IdAuto")]
    public int IdAuto { get; set; }

    [Column("Auto")]
    public Auto Auto { get; set; }

    [Column("FechaInicio")]
    public DateTime FechaInicio { get; set; }

    [Column("FechaFin")]
    public DateTime FechaFin { get; set; }
}

public class Estudiante
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    public ICollection<Curso> Cursos {get; set;}
}

public class Curso
{
    [Key]
    [Column("id")]
    public int Id { get; set; }

    public ICollection<Estudiante> Estudiantes {get; set;}
}