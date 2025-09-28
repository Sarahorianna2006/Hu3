using Microsoft.EntityFrameworkCore;
using veterinaria_sanmiguel.Data;
using veterinaria_sanmiguel.Models;

namespace veterinaria_sanmiguel.Services;

public class AtencionMedicaService
{
    private readonly AppDbContext _context;

    public AtencionMedicaService(AppDbContext context)
    {
        _context = context;
    }

    public void Registrar(AtencionMedica atencion)
    {
        _context.AtencionesMedicas.Add(atencion);
        _context.SaveChanges();
        Console.WriteLine("Atención médica registrada con éxito.");
    }

    public void Listar()
    {
        var atenciones = _context.AtencionesMedicas
            .Include(a => a.Mascota)
            .Include(a => a.Veterinario)
            .ToList();

        Console.WriteLine("\nLista de Atenciones Médicas:");
        foreach (var a in atenciones)
        {
            Console.WriteLine($"ID de la atencion medica: {a.Id} \n Mascota: {a.Mascota.Nombre} \n " +
                              $"Vet: {a.Veterinario.nombre} \n Fecha: {a.Fecha}");
            Console.WriteLine("--------------------------------------");
        }
    }

    public void Eliminar(int id)
    {
        var atencion = _context.AtencionesMedicas.Find(id);
        if (atencion != null)
        {
            _context.AtencionesMedicas.Remove(atencion);
            _context.SaveChanges();
            Console.WriteLine("Atención médica eliminada.");
        }
        else
        {
            Console.WriteLine("Atención medica no encontrada.");
        }
    }

    //submenú de gestión de atenciones médicas
    public void gestionarAtenciones()
    {
        string opcion = "";
        while (opcion != "0")
        {
            Console.Clear();
            Console.WriteLine("=== Gestión de Atenciones Médicas ===");
            Console.WriteLine("1. Registrar atención medica");
            Console.WriteLine("2. Listar atenciones medicas");
            Console.WriteLine("3. Eliminar atención medica");
            Console.WriteLine("5. Volver al menu anterior");
            Console.Write("Seleccione una opción: ");
            opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    Console.Write("ID de la mascota: ");
                    int mascotaId = int.Parse(Console.ReadLine());
                    Console.Write("Id del veterinarioId: ");
                    int vetId = int.Parse(Console.ReadLine());
                    Console.Write("Diagnóstico de la mascota: ");
                    string diag = Console.ReadLine();
                    Console.Write("Tratamiento para la mascota: ");
                    string trat = Console.ReadLine();
                    Console.Write("Precio de la atencion medica: ");
                    decimal precio = decimal.Parse(Console.ReadLine());

                    var atencion = new AtencionMedica
                    {
                        MascotaId = mascotaId,
                        VeterinarioId = vetId,
                        Fecha = DateTime.Now,
                        Diagnostico = diag,
                        Tratamiento = trat,
                        Precio = precio
                    };
                    Registrar(atencion);
                    break;

                case "2":
                    Listar();
                    break;

                case "3":
                    Console.Write("Ingrese ID de la atención a eliminar: ");
                    int idEliminar = int.Parse(Console.ReadLine());
                    Eliminar(idEliminar);
                    break;

                case "5":
                    return;

                default:
                    Console.WriteLine("Opción inválida.");
                    break;
            }
            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
