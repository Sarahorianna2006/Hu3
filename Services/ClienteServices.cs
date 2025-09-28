using System;
using veterinaria_sanmiguel.Data;
using veterinaria_sanmiguel.Models;

namespace veterinaria_sanmiguel.Services;

public class ClienteServices
{
    private readonly AppDbContext _context;

    //El constructor que exige el AppDbContext
    public ClienteServices(AppDbContext context)
    {
        _context = context;
    }

    private List<Cliente> _listaDeCLientes = new List<Cliente>();
    public void gestionarClientes()
    {
        while (true)//bucle para mantenernos en el menu
        {
            Console.Clear();
            Console.WriteLine("--Bienvenido a gestión de clientes--\n");
            Console.WriteLine("1. Registrar Cliente");
            Console.WriteLine("2. Listar clientes");
            Console.WriteLine("3. Editar clientes");
            Console.WriteLine("4. ELiminar cliente");
            Console.WriteLine("5. Volver al menu principal");

            string opcionMenuCliente = Console.ReadLine();
            switch (opcionMenuCliente)
            {
                case "1":
                    registrarCliente();
                    break;
                case "2":
                    listarCLiente();
                    break;
                case "3":
                    editarCliente();
                    break;
                case "4":
                    eliminarCliente();
                    break;
                case "5":
                    return;//finaliza la ejecucion del metodo actual y devuelve al menu principal
                default:
                    Console.WriteLine("Opción no valida");
                    break;
            }

            Console.WriteLine("Presione una tecla para continuar");
            Console.ReadKey();
        }
    }

    //nuevo metodo para registrar cliente
    public void registrarCliente()
    {
        Console.Clear();
        Console.WriteLine("--Registro de nuevo cliente--\n");

        Console.WriteLine("Ingresa el nombre del cliente:");
        string nombre = Console.ReadLine();

        Console.WriteLine("Ingresa el apellido del cliente:");
        string apellido = Console.ReadLine();

        Console.WriteLine("Ingresa la dirección del cliente:");
        string direccion = Console.ReadLine();

        Console.WriteLine("Ingresa el celular del cliente:");
        string celular = Console.ReadLine();

        var nuevoCliente = new Cliente(nombre, apellido, direccion, celular);

        _context.Add(nuevoCliente);

        _context.SaveChanges();

        Console.WriteLine("Cliente registrado exitosamente");
    }

    //nuevo metodo para listar clientes
    public void listarCLiente()
    {

        var listaDataBase = _context.Clientes.ToList(); //Aquí se pide a _context que vaya a la tabla Clientes de la base de datos, traiga todos los registros que encuentre y los convierta en una List<Cliente>

        Console.Clear();
        Console.WriteLine("-- Lista de Clientes Registrados --\n");

        if (listaDataBase.Count == 0)
        {
            Console.WriteLine("No hay clientes para mostrar");
            return;
        }
        foreach (var cliente in listaDataBase)
        {
            Console.WriteLine($"ID del cliente: {cliente.Id}");
            Console.WriteLine($"Nombre completo del cliente: {cliente.nombre} {cliente.apellido}");
            Console.WriteLine($"Dirección del cliente: {cliente.direccion}");
            Console.WriteLine($"Celular del cliente: {cliente.celular}");
            Console.WriteLine("--------------------------------------");
        }


    }

    //metodo para editar cliente
    public void editarCliente()
    {
        Console.Clear();
        Console.WriteLine("--Editar un cliente--");

        listarCLiente();

        if (!_context.Clientes.Any())// pregunta si hay algun cliente. El ! lo niega.
        {
            return;//Si no hay ningun cliente salimos del metodo.
        }

        Console.WriteLine("Ingrese el ID del cliente que desea editar");

        int.TryParse(Console.ReadLine(), out int id);//intenta convertir el dato a int y si es exitoso lo guarda en la variable id

        var clienteAEditar = _context.Clientes.Find(id);//buscamos el cliente por su id y lo almacenamos en variable

        if (clienteAEditar != null)//si el cliente es diferente de null(o sea si e encuentra)
        {
            Console.WriteLine($"Editando a {clienteAEditar.nombre} {clienteAEditar.apellido}");
            Console.WriteLine("Nota: Si no desea cambiar el dato presione enter");

            Console.WriteLine($"Ingrese nuevo nombre para {clienteAEditar.nombre}");
            string nuevoNombre = Console.ReadLine();

            if (!string.IsNullOrEmpty(nuevoNombre))//si esta vacio o es nulo el nuevo nombre
            {
                clienteAEditar.nombre = nuevoNombre;//se deja el mismo nombre para nuevo nombre
            }

            Console.WriteLine($"Ingrese el nuevo apellido para {clienteAEditar.apellido}");
            string nuevoApellido = Console.ReadLine();

            if (!string.IsNullOrEmpty(nuevoApellido))
            {
                clienteAEditar.apellido = nuevoApellido;
            }

            Console.WriteLine($"Ingrese nueva direccion para {clienteAEditar.direccion}");
            string nuevaDireccion = Console.ReadLine();

            if (!string.IsNullOrEmpty(nuevaDireccion))
            {
                clienteAEditar.apellido = nuevaDireccion;
            }

            Console.WriteLine($"Ingrese nuevo celular para {clienteAEditar.celular}");
            string nuevoCelular = Console.ReadLine();

            if (!string.IsNullOrEmpty(nuevoCelular))
            {
                clienteAEditar.celular = nuevoCelular;
            }

            _context.SaveChanges();
            Console.WriteLine("Cliente guardado exitosamente");

        }
        else
        {
            Console.WriteLine("Por favor ingresa un ID valido");
        }

    }

    //metodo para eliminar cliente
    public void eliminarCliente()
    {
        Console.Clear();
        Console.WriteLine("--Eliminar un cliente--");

        listarCLiente();

        if (!_context.Clientes.Any())
        {
            return;
        }

        Console.WriteLine("Ingresa el ID del cliente que deseas eliminar");
        int.TryParse(Console.ReadLine(), out int id);

        var clienteAEliminar = _context.Clientes.Find(id);

        if (clienteAEliminar != null)
        {
            Console.WriteLine($"Has seleccionado a {clienteAEliminar.nombre} {clienteAEliminar.apellido}");
            Console.WriteLine("Estas seguro que deseas eliminarlo? (s/n)");

            string confirmacionEliminar = Console.ReadLine().ToLower();

            if (confirmacionEliminar == "s")
            {
                _context.Clientes.Remove(clienteAEliminar);
                _context.SaveChanges();
                Console.WriteLine("El cliente fue eliminado exitosamente");
            }
            else
            {
                Console.WriteLine("Operación cancelada");

            }
        }
        else
        {
            Console.WriteLine("Por favor ingrese un ID valido");
        }

        


    }
}