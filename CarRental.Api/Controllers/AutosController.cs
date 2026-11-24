using CarRental.Api.Data;
using CarRental.Api.Dto;
using CarRental.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CarRental.Api.Controllers;
[ApiController]
[Route("api/[controller]")]
public class AutosController : ControllerBase
{
    private readonly AppDbContext _context;
    public AutosController(AppDbContext context)
    {
        _context = context;
    }

    private bool EsAnioValido(int anio)
    {
        int anioactual = DateTime.Now.Year;
        return anio >= anioactual - 5;
    }

    private AutoDto ToDto(Auto auto) => new AutoDto
    {
        Id = auto.Id,
        Marca = auto.Marca,
        Modelo = auto.Modelo,
        Precio = auto.Precio,
        Anio = auto.Anio
    };

    private Auto ToEntity(AutoDto dto) => new Auto
    {
        Id = dto.Id,
        Marca = dto.Marca,
        Modelo = dto.Modelo,
        Precio = dto.Precio,
        Anio = dto.Anio
    };

    [HttpGet]
    public async Task<ActionResult<IEnumerable<AutoDto>>> ObtenerAutos()
    {
        var autos = await _context.Autos.ToListAsync();
        return autos.Select(ToDto).ToList();
    }

    // GET: api/autos/5
    [HttpGet("{id}")]
    public async Task<ActionResult<AutoDto>> ObtenerAuto(int id)
    {
        var auto = await _context.Autos.FindAsync(id);
        if (auto == null)
            return NotFound();

        return ToDto(auto);
    }

    // POST: api/autos
    [HttpPost]
    public async Task<ActionResult<AutoDto>> CrearAuto(AutoDto autoDto)
    {
        if (!EsAnioValido(autoDto.Anio))
            return BadRequest("El auto no debe tener más de 5 años de antigüedad.");

        var auto = ToEntity(autoDto);
        _context.Autos.Add(auto);
        await _context.SaveChangesAsync();

        autoDto.Id = auto.Id;
        return CreatedAtAction(nameof(ObtenerAuto), new { id = auto.Id }, autoDto);
    }

    // PUT: api/autos/5
    [HttpPut("{id}")]
    public async Task<ActionResult> ActualizarAuto(int id, AutoDto autoDto)
    {
        if (id != autoDto.Id)
            return BadRequest();

        if (!EsAnioValido(autoDto.Anio))
            return BadRequest("El auto no debe tener más de 5 años de antigüedad.");

        var auto = await _context.Autos.FindAsync(id);
        if (auto == null)
            return NotFound();
        
        auto.Marca = autoDto.Marca;
        auto.Modelo = autoDto.Modelo;
        auto.Precio = autoDto.Precio;
        auto.Anio = autoDto.Anio;
        
        await _context.SaveChangesAsync();
        return NoContent();
    }

    // DELETE: api/autos/5
    [HttpDelete("{id}")]
    public async Task<ActionResult> EliminarAuto(int id)
    {
        var auto = await _context.Autos.FindAsync(id);
        if (auto == null)
            return NotFound();
        
        _context.Autos.Remove(auto);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}