
using System.ComponentModel.DataAnnotations;

namespace veterinaria_sanmiguel.Models;

public class Veterinario : Persona
{
    [Required]
    [MaxLength(100)]
    public string Especialidad { get; set; }

    public List<AtencionMedica> Atenciones { get; set; } = new();
}