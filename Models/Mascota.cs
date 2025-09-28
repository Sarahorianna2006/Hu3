using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace veterinaria_sanmiguel.Models;

public class Mascota
{
    [Key]
    public int IdMascota { get; set; }

    public int IdCliente { get; set; }

    [MaxLength(50)]
    public string Especie { get; set; }

    [MaxLength(50)]
    public string Nombre { get; set; }

    [MaxLength(50)]
    public string Raza { get; set; }

    public int Edad { get; set; }

    
    // --- Aquí definimos la relación con Cliente ---
    public int ClienteId { get; set; } //Clave Foránea: Guarda el ID del dueño de la mascota

    [ForeignKey("ClienteId")]//Permite acceder al objeto Cliente completo desde una Mascota
    public virtual Cliente Cliente { get; set; } 
    

}