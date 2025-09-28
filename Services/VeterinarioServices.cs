// Services/VeterinarioService.cs

// estos son los using que necesitamos para que todo funcione
using System;
using System.Linq; // necesitamos este para usar any
using veterinaria_sanmiguel.Data;
using veterinaria_sanmiguel.Models;

namespace veterinaria_sanmiguel.Services
{
    public class VeterinarioService
    {
        // esta variable nos conecta a la base de datos
        private readonly AppDbContext _context;

        // este es el constructor que pide la conexion a la base de datos
        public VeterinarioService(AppDbContext context)
        {
            _context = context;
        }

        // este metodo es el menu principal de los veterinarios
        public void gestionarVeterinarios()
        {
            // con este while hacemos que el menu se repita siempre
            while (true)
            {
                Console.Clear();
                Console.WriteLine("-- Gestion de Veterinarios --\n");
                Console.WriteLine("1. Registrar Veterinario");
                Console.WriteLine("2. Listar Veterinarios");
                Console.WriteLine("3. Editar Veterinario");
                Console.WriteLine("4. Eliminar Veterinario");
                Console.WriteLine("5. Volver al menu principal");

                Console.Write("\nSeleccione una opcion: ");
                string opcion = Console.ReadLine();

                // el switch es para ver que opcion eligio el usuario
                switch (opcion)
                {
                    case "1":
                        registrarVeterinario();
                        break;
                    case "2":
                        listarVeterinarios();
                        break;
                    case "3":
                        editarVeterinario();
                        break;
                    case "4":
                        eliminarVeterinario();
                        break;
                    case "5":
                        return; // con return salimos de este menu
                    default:
                        Console.WriteLine("Opcion no valida");
                        break;
                }

                Console.WriteLine("\nPresione una tecla para continuar");
                Console.ReadKey();
            }
        }

        // metodo para crear un veterinario nuevo
        public void registrarVeterinario()
        {
            Console.Clear();
            Console.WriteLine("-- Registro de Nuevo Veterinario --\n");

            // aqui le pedimos al usuario que escriba los datos
            Console.Write("Nombre del veterinario: ");
            string nombre = Console.ReadLine();

            Console.Write("Apellido del veterinario: ");
            string apellido = Console.ReadLine();

            Console.Write("Celular del veterinario: ");
            string celular = Console.ReadLine();

            Console.Write("Especialidad del veterinario: ");
            string especialidad = Console.ReadLine();

            // creamos el objeto nuevo con los datos que nos dieron
            var nuevoVeterinario = new Veterinario
            {
                nombre = nombre,
                apellido = apellido,
                celular = celular,
                Especialidad = especialidad
            };

            // aqui lo agregamos a la base de datos y guardamos
            _context.Veterinarios.Add(nuevoVeterinario);
            _context.SaveChanges();

            Console.WriteLine("\nVeterinario registrado exitosamente");
        }

        // metodo para mostrar todos los veterinarios que hay
        public void listarVeterinarios()
        {
            Console.Clear();
            Console.WriteLine("-- Lista de Veterinarios --\n");

            // buscamos todos los veterinarios en la base de datos
            var listaDeVeterinarios = _context.Veterinarios.ToList();

            // si no hay ninguno mostramos un mensaje
            if (listaDeVeterinarios.Count == 0)
            {
                Console.WriteLine("No hay veterinarios registrados");
                return;
            }

            // usamos un foreach para mostrar cada uno
            foreach (var veterinario in listaDeVeterinarios)
            {
                Console.WriteLine($"ID: {veterinario.Id}");
                Console.WriteLine($"Nombre: {veterinario.nombre} {veterinario.apellido}");
                Console.WriteLine($"Celular: {veterinario.celular}");
                Console.WriteLine($"Especialidad: {veterinario.Especialidad}");
                Console.WriteLine("---------------------------------");
            }
        }

        // metodo para cambiar los datos de un veterinario
        public void editarVeterinario()
        {
            Console.Clear();
            Console.WriteLine("-- Editar Veterinario --");

            // primero mostramos la lista para que el usuario elija
            listarVeterinarios();

            // revisamos si hay algo que editar
            if (!_context.Veterinarios.Any())
            {
                return;
            }

            Console.Write("\nIngresa el ID del veterinario a editar: ");
            int.TryParse(Console.ReadLine(), out int id);

            // buscamos el veterinario por su id
            var veterinarioAEditar = _context.Veterinarios.Find(id);

            // si lo encontramos procedemos a editar
            if (veterinarioAEditar != null)
            {
                Console.WriteLine($"\nEditando a {veterinarioAEditar.nombre}");
                Console.WriteLine("Si no quieres cambiar un campo solo presiona Enter");

                Console.Write($"Nuevo nombre ({veterinarioAEditar.nombre}): ");
                string nuevoNombre = Console.ReadLine();
                if (!string.IsNullOrEmpty(nuevoNombre))
                {
                    veterinarioAEditar.nombre = nuevoNombre;
                }

                Console.Write($"Nuevo apellido ({veterinarioAEditar.apellido}): ");
                string nuevoApellido = Console.ReadLine();
                if (!string.IsNullOrEmpty(nuevoApellido))
                {
                    veterinarioAEditar.apellido = nuevoApellido;
                }

                Console.Write($"Nuevo celular ({veterinarioAEditar.celular}): ");
                string nuevoCelular = Console.ReadLine();
                if (!string.IsNullOrEmpty(nuevoCelular))
                {
                    veterinarioAEditar.celular = nuevoCelular;
                }

                Console.Write($"Nueva especialidad ({veterinarioAEditar.Especialidad}): ");
                string nuevaEspecialidad = Console.ReadLine();
                if (!string.IsNullOrEmpty(nuevaEspecialidad))
                {
                    veterinarioAEditar.Especialidad = nuevaEspecialidad;
                }

                // guardamos los cambios en la base de datos
                _context.SaveChanges();
                Console.WriteLine("\nVeterinario editado exitosamente");
            }
            else
            {
                Console.WriteLine("ID de veterinario no encontrado");
            }
        }

        // metodo para borrar un veterinario
        public void eliminarVeterinario()
        {
            Console.Clear();
            Console.WriteLine("-- Eliminar Veterinario --");

            listarVeterinarios();

            if (!_context.Veterinarios.Any())
            {
                return;
            }

            Console.Write("\nIngresa el ID del veterinario a eliminar: ");
            int.TryParse(Console.ReadLine(), out int id);

            // buscamos al veterinario que vamos a borrar
            var veterinarioAEliminar = _context.Veterinarios.Find(id);

            // si lo encontramos pedimos confirmacion
            if (veterinarioAEliminar != null)
            {
                Console.WriteLine($"\nHas seleccionado a {veterinarioAEliminar.nombre}");
                Console.Write("Estas seguro de que quieres eliminarlo (s/n): ");
                string confirmacion = Console.ReadLine().ToLower();

                // si el usuario escribe s lo borramos
                if (confirmacion == "s")
                {
                    _context.Veterinarios.Remove(veterinarioAEliminar);
                    _context.SaveChanges();
                    Console.WriteLine("\nVeterinario eliminado exitosamente");
                }
                else
                {
                    Console.WriteLine("\nOperacion cancelada");
                }
            }
            else
            {
                Console.WriteLine("\nID de veterinario no encontrado");
            }
        }

        public void veterinarioTop()
        {
            var veterinario = _context.Veterinarios
                .Select(v => new
                {
                    v.nombre,
                    TotalAtenciones = v.Atenciones.Count()
                }).OrderByDescending(v => v.TotalAtenciones)
                .FirstOrDefault();
            if (veterinario != null)
            {
                Console.WriteLine($"\nEl veterinario con mas atenciones es {veterinario.nombre} con {veterinario.TotalAtenciones} atenciones\n");
            }
            else
            {
                Console.WriteLine("\nNo se encontro ningun veterinario\n");
            }
        }
    }
}