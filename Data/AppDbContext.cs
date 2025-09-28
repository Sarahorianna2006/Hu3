using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using veterinaria_sanmiguel.Models;//se pone para que encuentre la clase cliente

namespace veterinaria_sanmiguel.Data;

public class AppDbContext : DbContext//aqui le damos poderes a la clase heredando de entity
{
    //Este constructor es necesario para la configuracion en Program.cs
    // public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    // {

    // }

    //aqui le decimos a entity que clases se convierten en tablas   
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Mascota> Mascotas { get; set; }
    public DbSet<Veterinario> Veterinarios { get; set; }
    
    public DbSet<AtencionMedica> AtencionesMedicas { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseMySql
            (
                "server=localhost;database=database_sanmiguel;user=root;password=123456",
                new MySqlServerVersion(new Version(8, 0, 36))
            );
        }
    }

}