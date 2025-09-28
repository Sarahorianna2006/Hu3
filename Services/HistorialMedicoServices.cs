using Microsoft.EntityFrameworkCore;
using veterinaria_sanmiguel.Data;

namespace veterinaria_sanmiguel.Services;

public class HistorialMedicoService
{
    private readonly AppDbContext _context;

    public HistorialMedicoService(AppDbContext context)
    {
        _context = context;
    }

    public void MostrarHistorialPorMascota(int mascotaId)
    {
        var atenciones = _context.AtencionesMedicas
            .Include(a => a.Mascota)
            .ThenInclude(m => m.Cliente)
            .Include(a => a.Veterinario)
            .Where(a => a.MascotaId == mascotaId)
            .ToList();

        if (!atenciones.Any())
        {
            Console.WriteLine("No se encontró historial médico para esta mascota.");
            return;
        }

        Console.WriteLine($"\n=== Historial médico de {atenciones.First().Mascota.Nombre} ===\n");
        foreach (var atencion in atenciones)
        {
            Console.WriteLine($"Fecha: {atencion.Fecha}");
            Console.WriteLine($"Dueño: {atencion.Mascota.Cliente.nombre} {atencion.Mascota.Cliente.apellido}");
            Console.WriteLine($"Veterinario: {atencion.Veterinario.nombre}");
            Console.WriteLine($"Diagnóstico: {atencion.Diagnostico}");
            Console.WriteLine($"Tratamiento: {atencion.Tratamiento}");
            Console.WriteLine($"Precio: {atencion.Precio}");
        }
    }

    //submenú de historial medico
    
    public void gestionarHistorial()
    {
        string opcion = "";
        while (opcion != "0")
        {
            Console.Clear();
            Console.WriteLine("=== Historial Médico ===");
            Console.WriteLine("1. Ver historial de una mascota");
            Console.WriteLine("2. Volver");
            Console.Write("Seleccione una opción: ");
            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("Ingrese ID de la mascota: ");
                    if (int.TryParse(Console.ReadLine(), out int idMascota))
                    {
                        MostrarHistorialPorMascota(idMascota);
                        Console.WriteLine("--------------------------------------");
                        Console.WriteLine("\nPresione una tecla para continuar...");
                    }
                    else
                    {
                        Console.WriteLine("ID inválido.");
                        Console.WriteLine("\nPresione una tecla para continuar...");
                    }
                    break;

                case "2":
                    Console.WriteLine("Volviendo al menú principal...");
                    
                    return;
                    

                default:
                    Console.WriteLine("Opción inválida.");
                    Console.WriteLine("\nPresione una tecla para continuar...");
                    break;
            }

            Console.ReadKey();
        }
    }
}
