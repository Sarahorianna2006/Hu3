using System;
using veterinaria_sanmiguel.Models;

namespace veterinaria_sanmiguel.Services;

public class ClienteServices
{
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
                    Console.WriteLine("Ha ingresado a eliminar un cliente");
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

        Console.WriteLine("Ingresa el apellidp del cliente:");
        string apellido = Console.ReadLine();

        Console.WriteLine("Ingresa la dirección del cliente:");
        string direccion = Console.ReadLine();

        Console.WriteLine("Ingresa el celular del cliente:");
        string celular = Console.ReadLine();

        var nuevoCliente = new Cliente(nombre, apellido, direccion, celular);

        _listaDeCLientes.Add(nuevoCliente);

        Console.WriteLine("Cliente registrado exitosamente");
    }

    //nuevo metodo para listar clientes
    public void listarCLiente()
    {
        Console.Clear();
        Console.WriteLine("-- Lista de Clientes Registrados --\n");

        if (_listaDeCLientes.Count == 0)
        {
            Console.WriteLine("No hay clientes para mostrar");
            return;
        }
        foreach (var cliente in _listaDeCLientes)
        {
            Console.WriteLine($"ID del cliente: {_listaDeCLientes.IndexOf(cliente) + 1}");//IndexOf nos da la posición del cliente en la lista (que empieza en 0), y le sumamos 1 para que los IDs se muestren desde el 1
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

        if (_listaDeCLientes.Count == 0)
        {
            return; // Si no hay clientes, ListarClientes ya mostró el mensaje, así que solo salimos.
        }

        Console.Write("\nIngresa el ID del cliente que deseas editar: ");
        string idSeleccionado = Console.ReadLine();

        //esta primera parte pregunta: "¿Se puede convertir la entrada del usuario a un número entero? Si es así, guárdalo en la variable id".
        if (int.TryParse(idSeleccionado, out int id) && id > 0 && id <= _listaDeCLientes.Count)
        {
            var clienteAEditar = _listaDeCLientes[id - 1];//-1 por que las listas empiezan en 0, para encontrar el indice que ingrese el usuario

            Console.WriteLine($"Editando a {clienteAEditar.nombre} {clienteAEditar.apellido}");
            Console.WriteLine("Si no desea cambiar algun valor presione enter");

            //pedimos los nuevos datos
            Console.WriteLine($"Nuevo nombre para {clienteAEditar.nombre}: ");
            string nuevoNombre = Console.ReadLine();

            if (!string.IsNullOrEmpty(nuevoNombre))
            {
                clienteAEditar.nombre = nuevoNombre;//si esta vacio o nulo se asigna el mismo nombre que tenía
            }

            Console.WriteLine($"Nuevo apellido para {clienteAEditar.apellido}: ");
            string nuevoApellido = Console.ReadLine();

            if (!string.IsNullOrEmpty(nuevoApellido))
            {
                clienteAEditar.apellido = nuevoApellido;//si esta vacio o nulo se asigna el mismo nombre que tenía
            }

            Console.WriteLine($"Nueva direccion para {clienteAEditar.direccion}: ");
            string nuevaDireccion = Console.ReadLine();

            if (!string.IsNullOrEmpty(nuevaDireccion))
            {
                clienteAEditar.direccion = nuevaDireccion;//si esta vacio o nulo se asigna el mismo nombre que tenía
            }

            Console.WriteLine($"Nuevo celular para {clienteAEditar.nombre}: ");
            string nuevoCelular = Console.ReadLine();

            if (!string.IsNullOrEmpty(nuevoCelular))
            {
                clienteAEditar.celular = nuevoCelular;//si esta vacio o nulo se asigna el mismo nombre que tenía
            }
        }
        else
        {
            Console.WriteLine("ID no válido. Por favor, intenta de nuevo.");
        }
    }

    //metodo para eliminar cliente
    public void eliminarCliente()
    {
        Console.Clear();
        Console.WriteLine("--Eliminar un cliente--");

        listarCLiente();

        if (_listaDeCLientes.Count == 0)
        {
            return;
        }

        Console.WriteLine("Ingresa el ID del cliente que deseas eliminar: ");
        string idSeleccionado = Console.ReadLine();

        if (int.TryParse(idSeleccionado, out int id) && id > 0 && id <= _listaDeCLientes.Count)
        {
            var clienteAEliminar = _listaDeCLientes[id - 1];

            Console.WriteLine($"Has seleccionado a {clienteAEliminar.nombre} {clienteAEliminar.apellido}");

            Console.WriteLine("Esta seguro que desea eliminar a este cliente?");
            string confirmacion = Console.ReadLine().ToLower();

            if (confirmacion == "s")
            {
                //El método RemoveAt() elimina el elemento en el índice especificado.
                _listaDeCLientes.RemoveAt(id - 1);
                Console.WriteLine("Cliente eliminado exitosamente");
            }
            else
            {
                Console.WriteLine("Operación cancelada");
            }
        }
         else
        {
            Console.WriteLine("ID no válido. Por favor, intenta de nuevo.");
        }


    }
}