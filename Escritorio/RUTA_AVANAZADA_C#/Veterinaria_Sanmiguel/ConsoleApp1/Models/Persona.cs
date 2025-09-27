using System;
using System.ComponentModel.DataAnnotations;


namespace veterinaria_sanmiguel.Models;

public class Persona
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string nombre { get; set; }

    [Required]
    [MaxLength(100)]
    public string apellido { get; set; }

    [Required]
    [MaxLength(15)]
    public string celular { get; set; }
    
}