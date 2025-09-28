namespace veterinaria_sanmiguel.Models;

public class AtencionMedica
{
    public int Id { get; set; }
    public DateTime Fecha { get; set; }
    public string Diagnostico { get; set; } = string.Empty;
    public string Tratamiento { get; set; } = string.Empty;
    public decimal Precio { get; set; }

    // Relaciones
    public int MascotaId { get; set; }
    public Mascota Mascota { get; set; }

    public int VeterinarioId { get; set; }
    public Veterinario Veterinario { get; set; }
}
