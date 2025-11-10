namespace LAMAMEdellin.client.Models
{
    public class Miembro
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Apellido { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public string CorreoElectronico { get; set; } = string.Empty;
        public DateTime FechaIngreso { get; set; }
        public string? Direccion { get; set; }
        public int Member { get; set; }
        public string? Cargo { get; set; }
        public string Rango { get; set; } = "Prospect";
        public string Estatus { get; set; } = "Activo";
        public DateTime FechaNacimiento { get; set; }
        public string Cedula { get; set; } = string.Empty;
        public string? RH { get; set; }
        public string? EPS { get; set; }
        public string? Padrino { get; set; }
        public string? Foto { get; set; }
        public string? ContactoEmergencia { get; set; }
        public string? Ciudad { get; set; }
        public string? Moto { get; set; }
        public int? AnoModelo { get; set; }
        public string? Marca { get; set; }
        public int? CilindrajeCC { get; set; }
        public string? PlacaMatricula { get; set; }
        public DateTime? FechaExpedicionLicenciaConduccion { get; set; }
        public DateTime? FechaExpedicionSOAT { get; set; }

        public string NombreCompleto => $"{Nombre} {Apellido}";
    }
}