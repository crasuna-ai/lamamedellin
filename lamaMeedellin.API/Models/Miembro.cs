using System.ComponentModel.DataAnnotations;

namespace LAMAMedellin.API.Models
{
    public class Miembro
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Nombre { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string Apellido { get; set; } = string.Empty;

        [Required]
        [MaxLength(20)]
        public string Celular { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string CorreoElectronico { get; set; } = string.Empty;

        [Required]
        public DateTime FechaIngreso { get; set; }

        [MaxLength(255)]
        public string? Direccion { get; set; }

        [Required]
        public int Member { get; set; }

        [MaxLength(100)]
        public string? Cargo { get; set; }

        [Required]
        [MaxLength(50)]
        public string Rango { get; set; } = "Prospect";

        [Required]
        [MaxLength(50)]
        public string Estatus { get; set; } = "Activo";

        [Required]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [MaxLength(20)]
        public string Cedula { get; set; } = string.Empty;

        [MaxLength(5)]
        public string? RH { get; set; }

        [MaxLength(100)]
        public string? EPS { get; set; }

        [MaxLength(100)]
        public string? Padrino { get; set; }

        [MaxLength(500)]
        public string? Foto { get; set; }

        [MaxLength(255)]
        public string? ContactoEmergencia { get; set; }

        [MaxLength(100)]
        public string? Ciudad { get; set; }

        // Información de Motocicleta
        [MaxLength(100)]
        public string? Moto { get; set; }

        public int? AnoModelo { get; set; }

        [MaxLength(100)]
        public string? Marca { get; set; }

        public int? CilindrajeCC { get; set; }

        [MaxLength(20)]
        public string? PlacaMatricula { get; set; }

        public DateTime? FechaExpedicionLicenciaConduccion { get; set; }

        public DateTime? FechaExpedicionSOAT { get; set; }

        // Auditoría
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
        public DateTime? FechaActualizacion { get; set; }
        public string? CreadoPor { get; set; }
        public string? ActualizadoPor { get; set; }
    }
}