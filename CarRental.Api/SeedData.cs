using CarRental.Api.Data;
using CarRental.Api.Models;

namespace CarRental.Api;
public static class SeedData
{
    public static void Initialize(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        if (context.Usuarios.Any())
            return; // Ya hay usuarios, no hace falta seed

        var usuarios = new List<Usuario>
        {
            new Usuario { Nombre = "alice", Correo = "alice@example.com" },
            new Usuario { Nombre = "bob", Correo = "bob@example.com" }
        };

        context.Usuarios.AddRange(usuarios);
        context.SaveChanges();
    }
}