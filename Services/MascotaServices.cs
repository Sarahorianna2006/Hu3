using System;
using Microsoft.EntityFrameworkCore;
using veterinaria_sanmiguel.Data;
using veterinaria_sanmiguel.Models;

namespace veterinaria_sanmiguel.Services;

public class MascotaService
{
    private readonly AppDbContext _context;

    // Constructor para recibir la conexión a la base de datos
    public MascotaService(AppDbContext context)
    {
        _context = context;
    }

    // Menú principal para la gestión de mascotas
    public void gestionarMascotas()
    {
        while (true)
        {
            Console.Clear();
            Console.WriteLine("-- Gestión de Mascotas --\n");
            Console.WriteLine("1. Registrar Mascota");
            Console.WriteLine("2. Listar Mascotas");
            Console.WriteLine("3. Editar Mascota");
            Console.WriteLine("4. Eliminar Mascota");
            Console.WriteLine("5. Volver al menú principal");

            Console.Write("\nSeleccione una opción: ");
            string opcion = Console.ReadLine();

            switch (opcion)
            {
                case "1":
                    registrarMascota();
                    break;
                case "2":
                    listarMascotas();
                    break;
                case "3":
                    editarMascota();
                    break;
                case "4":
                    eliminarMascota();
                    break;
                case "5":
                    return; // Vuelve al menú principal
                default:
                    Console.WriteLine("Opción no válida.");
                    break;
            }

            Console.WriteLine("\nPresione una tecla para continuar...");
            Console.ReadKey();
        }
    }

    //metodos para el crud

    public void registrarMascota()
    {
        Console.Clear();
        Console.WriteLine("--Registro de mascota--");

        if (!_context.Clientes.Any())//Si no hay clientes
        {
            Console.WriteLine("No hay clientes registrados para asociar a una mascota");
            return;
        }

        Console.WriteLine("Por favor seleccione el dueño de la mascota");
        var listaDeCLientes = _context.Clientes.ToList();

        foreach (var cliente in listaDeCLientes)
        {
            Console.WriteLine($"Id: {cliente.Id} Nombre: {cliente.nombre} {cliente.apellido}");
        }

        Console.Write("\nIngrese el ID del cliente dueño: ");

        int.TryParse(Console.ReadLine(), out int clienteIdSeleccionado);

        var dueño = _context.Clientes.Find(clienteIdSeleccionado);

        if (dueño == null)
        {
            Console.WriteLine("ID de cliente no válido o no encontrado. Operación cancelada.");
            return; // Salimos si el ID no es válido.
        }
        //si el dueño es valido pedimos datos de la mascota

        Console.WriteLine($"\nRegistrando una nueva mascota para: {dueño.nombre} {dueño.apellido}");

        Console.Write("Nombre de la mascota: ");
        string nombreMascota = Console.ReadLine();

        Console.Write("Especie (Ej: Perro, Gato): ");
        string especie = Console.ReadLine();

        Console.Write("Raza: ");
        string raza = Console.ReadLine();

        Console.Write("Edad: ");
        int.TryParse(Console.ReadLine(), out int edad);

        //Crear el nuevo objeto Mascota y guardarlo.
        var nuevaMascota = new Mascota
        {
            Nombre = nombreMascota,
            Especie = especie,
            Raza = raza,
            Edad = edad,
            ClienteId = dueño.Id //Este es el paso clave que crea la relacion
        };

        _context.Mascotas.Add(nuevaMascota);
        _context.SaveChanges();

        Console.WriteLine("\n¡Mascota registrada exitosamente!");

    }

    public void listarMascotas()
    {
        Console.Clear();
        Console.WriteLine("-- Lista de Mascotas Registradas --\n");

        // usamos .Include() para traer los datos del Cliente relacionado.
        var mascotas = _context.Mascotas
            .Include(mascota => mascota.Cliente) //aqui se relaciona
            .ToList();
        

        //Verificamos si la lista está vacía.
        if (mascotas.Count == 0)
        {
            Console.WriteLine("No hay mascotas registradas para mostrar.");
            return;
        }

        // Recorremos la lista y mostramos la información.
        foreach (var mascota in mascotas)
        {
            Console.WriteLine($"ID Mascota: {mascota.IdMascota}");
            Console.WriteLine($"Nombre: {mascota.Nombre}");
            Console.WriteLine($"Especie: {mascota.Especie}");
            Console.WriteLine($"Raza: {mascota.Raza}");
            // datos del dueño(cliente)
            Console.WriteLine($"Dueño: {mascota.Cliente.nombre} {mascota.Cliente.apellido}");
            Console.WriteLine("--------------------------------------");
        }
    }

    public void editarMascota()
    {
        Console.Clear();
        Console.WriteLine("-- Editar una Mascota --");

        //mostramos todas las mascotas
        listarMascotas();

        // verificamos si hay mascotas para editar.
        if (!_context.Mascotas.Any())
        {
            return; //salimos si no hay mascotas.
        }

        Console.Write("\nIngresa el ID de la mascota que deseas editar: ");
        int.TryParse(Console.ReadLine(), out int id);

        // Buscamos la mascota y su dueño con .Include() para mostrar la info completa.
        var mascotaAEditar = _context.Mascotas
            .Include(m => m.Cliente)
            .FirstOrDefault(m => m.IdMascota == id); // Usamos FirstOrDefault para buscar por ID.

        if (mascotaAEditar != null)
        {
            Console.WriteLine($"\nEditando a '{mascotaAEditar.Nombre}',su dueño es {mascotaAEditar.Cliente.nombre}");
            Console.WriteLine("Si no deseas cambiar un campo, solo presiona Enter");

            // Pedimos los nuevos datos
            Console.Write($"Nuevo nombre para ({mascotaAEditar.Nombre}): ");
            string nuevoNombre = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevoNombre))
            {
                mascotaAEditar.Nombre = nuevoNombre;
            }

            Console.Write($"Nueva especie para ({mascotaAEditar.Especie}): ");
            string nuevaEspecie = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevaEspecie))
            {
                mascotaAEditar.Especie = nuevaEspecie;
            }

            Console.Write($"Nueva raza para({mascotaAEditar.Raza}): ");
            string nuevaRaza = Console.ReadLine();
            if (!string.IsNullOrEmpty(nuevaRaza))
            {
                mascotaAEditar.Raza = nuevaRaza;
            }

            Console.Write($"Nueva edad para({mascotaAEditar.Edad}): ");
            string nuevaEdadInput = Console.ReadLine();
            if (int.TryParse(nuevaEdadInput, out int nuevaEdad))
            {
                mascotaAEditar.Edad = nuevaEdad;
            }

            // Guardamos los cambios en la base de datos
            _context.SaveChanges();
            Console.WriteLine("\nMascota editada exitosamente");
        }
        else
        {
            Console.WriteLine("ID de mascota no válido o no encontrado.");
        }

    }

    public void eliminarMascota()
    {
        Console.Clear();
        Console.WriteLine("-- Eliminar una Mascota --");

        listarMascotas();

        if (!_context.Mascotas.Any())
        {
            return; // Salimos si no hay mascotas que eliminar.
        }

        Console.Write("\nIngresa el ID de la mascota que deseas eliminar: ");
        int.TryParse(Console.ReadLine(), out int id);

        // Buscamos la mascota por su ID.
        var mascotaAEliminar = _context.Mascotas.Find(id);

        if (mascotaAEliminar != null)
        {
            Console.WriteLine($"\nHas seleccionado a '{mascotaAEliminar.Nombre}'.");
            Console.Write("Estás seguro de que deseas eliminarla? (s/n): ");
            string confirmacion = Console.ReadLine().ToLower();

            if (confirmacion == "s")
            {
                _context.Mascotas.Remove(mascotaAEliminar);
                _context.SaveChanges();
                Console.WriteLine("\nMascota eliminada exitosamente");
            }
            else
            {
                Console.WriteLine("\nOperación cancelada");
            }
        }
        else
        {
            Console.WriteLine("\nID de mascota no válido o no encontrado.");
        }
    }
}