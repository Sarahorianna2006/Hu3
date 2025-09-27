//using para habilitar las anotaciones(key,required,maxlenght,etc...)
using System.ComponentModel.DataAnnotations;

namespace veterinaria_sanmiguel.Models;

public class Cliente : Persona
{

    [Required]
    [MaxLength(200)]
    public string direccion { get; set; }

    //relacion del cliente con la mascota
    public virtual ICollection<Mascota> Mascotas { get; set; } = new List<Mascota>();

    public Cliente(string nombre, string apellido, string direccion, string celular)
    {
        this.nombre = nombre;
        this.apellido = apellido;
        this.direccion = direccion;
        this.celular = celular;
    }    

    public Cliente() {}
    
}

