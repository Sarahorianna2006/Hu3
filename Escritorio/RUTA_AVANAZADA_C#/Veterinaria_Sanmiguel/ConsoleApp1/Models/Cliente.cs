//using para habilitar las anotaciones(key,required,maxlenght,etc...)
using System.ComponentModel.DataAnnotations;

namespace veterinaria_sanmiguel.Models;

public class Cliente
{
    [Key]
    public int id { get; set; }

    [Required]
    [MaxLength(100)]
    public string nombre { get; set; }

    [Required]
    [MaxLength(100)]
    public string apellido { get; set; }

    [Required]
    [MaxLength(200)]
    public string direccion { get; set; }

    [Required]
    [MaxLength(15)]
    public string celular { get; set; }


    public Cliente(string nombre, string apellido, string direccion, string celular)
    {
        this.nombre = nombre;
        this.apellido = apellido;
        this.direccion = direccion;
        this.celular = celular;
    }    
    
}

