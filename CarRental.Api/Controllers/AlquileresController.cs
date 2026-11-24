using CarRental.Api.Data;
using CarRental.Api.Dto;
using CarRental.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlquileresController : ControllerBase
{
    private readonly AppDbContext _context;
    public AlquileresController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CrearAlquiler([FromBody] AlquilerDto alquilerDto)
    {
        if (alquilerDto.FechaInicio >= alquilerDto.FechaFin)
            return BadRequest("Periodo invalido para rentar.");
        
        var auto = await _context.Autos.FindAsync(alquilerDto.IdAuto);
        if (auto == null    )
            return NotFound("Auto no encontrado.");

        // Comprobar disponibilidad
        bool esAutoDisponible = !await _context.Alquileres.AnyAsync(r =>
        r.Auto.Id == alquilerDto.IdAuto &&
        (
            (alquilerDto.FechaInicio >= r.FechaInicio && alquilerDto.FechaInicio < r.FechaFin) ||
            (alquilerDto.FechaFin >= r.FechaInicio && alquilerDto.FechaFin < r.FechaFin) ||
            (alquilerDto.FechaInicio >= r.FechaInicio && alquilerDto.FechaFin < r.FechaFin)
        ));

        if (!esAutoDisponible)
            return Conflict("El auto no esta disponible para el periodo seleccionado.");
        
        var diasTotales = (alquilerDto.FechaFin - alquilerDto.FechaInicio).Days;
        if (diasTotales <= 0)
            return BadRequest("El alquiler debe ser de al menos 1 dia.");

        var precioTotal = diasTotales * auto.Precio;

        var alquiler = new Alquiler
        {
            IdUsuario = alquilerDto.IdUsuario,
            IdAuto = alquilerDto.IdAuto,
            FechaInicio = alquilerDto.FechaInicio,
            FechaFin = alquilerDto.FechaFin,
            // Precio = precioTotal
        };

        _context.Alquileres.Add(alquiler);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            alquiler.Id,
            PrecioTotal = precioTotal
        });
    }
}