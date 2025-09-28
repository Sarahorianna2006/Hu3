using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using veterinaria_sanmiguel.Services;
using veterinaria_sanmiguel.Data;
using System;


namespace veterinaria_sanmiguel;

public class Program
{
    public static void Main(string[] args)
    {
        //Creamos el contexto de la base de datos
        using var DbContext = new AppDbContext();

        //Creamos el servicio de clientes y le "inyectamos" la conexión a la BD.
        var ClienteServices = new ClienteServices(DbContext);
        //servicio de mascotas
        var MascotaServices = new MascotaService(DbContext);
        //servicio de veternario
        var veterinarioService = new VeterinarioService(DbContext);
        //servicio atencion medica
        var atencionMedicaService = new AtencionMedicaService(DbContext);
        //servicio para historial medico
        var historialMedicoService = new HistorialMedicoService(DbContext);
        
        
        while (true)
        {
            Console.Clear();
            Console.WriteLine("--Bienvenido a Veterinaria SanMiguel--\n");
            Console.WriteLine("1. Gestión de clientes");
            Console.WriteLine("2. Gestión de mascotas");
            Console.WriteLine("3. Gestión de veterinarios");
            Console.WriteLine("4. Gestión de atencione medicas");
            Console.WriteLine("5. Historial medico");
            Console.WriteLine("6. Salir");
            Console.WriteLine("----------------------------------------");
            Console.WriteLine("Por favor seleccione una opción");

            string opcionMenuPrincipal = Console.ReadLine();

            switch (opcionMenuPrincipal)
            {
                case "1":
                    ClienteServices.gestionarClientes();//llamamos el metodo
                    break;
                case "2":
                    MascotaServices.gestionarMascotas();
                    break;
                case "3":
                    veterinarioService.gestionarVeterinarios();
                    break;
                case "4":
                    atencionMedicaService.gestionarAtenciones();
                    break;
                case "5":
                    historialMedicoService.gestionarHistorial();
                    break;
                case "6":
                    Console.WriteLine("saliendo del sistema...");
                    return; //return termina la ejecucion del metodo Main y cierra el programa                                                                               
                default:
                    Console.WriteLine("Seleccione una opción valida");
                    break;
            }

            Console.WriteLine("\nPresiona una tecla para continuar...");
            Console.ReadKey();
        }
    }
}
