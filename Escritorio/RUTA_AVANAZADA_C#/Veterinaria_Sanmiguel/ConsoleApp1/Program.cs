using veterinaria_sanmiguel.Services;

namespace veterinaria_sanmiguel;

public class Program
{
    public static void Main(string[] args)
    {
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
                    Console.WriteLine("Ha ingresado a gestion de clientes");
                    ClienteServices clienteService1 = new ClienteServices(); //construimos el objeto
                    clienteService1.gestionarClientes();//llamamos el metodo
                    break;
                case "2":
                    Console.WriteLine("Ha ingresado a gestion de mascotas");
                    break;
                case "3":
                    Console.WriteLine("Ha ingresado a gestion de veterinarios");
                    break;
                case "4":
                    Console.WriteLine("Ha ingresado a gestion de atenciones medicas");
                    break;
                case "5":
                    Console.WriteLine("Ha ingresado a historial medico");
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
