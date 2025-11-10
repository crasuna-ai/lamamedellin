using LAMAMedellin.API.Models;

namespace LAMAMedellin.API.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context)
        {
            
            context.Database.EnsureCreated();

           
            if (context.Miembros.Any())
            {
                return; 
            }


           
            var miembros = new Miembro[]
            {
                new Miembro
                {
                    Nombre = "Juan",
                    Apellido = "Pérez",
                    Celular = "3001234567",
                    CorreoElectronico = "juan.perez@email.com",
                    FechaIngreso = new DateTime(2020, 1, 15),
                    Member = 1,
                    Rango = "Full Color",
                    Estatus = "Activo",
                    FechaNacimiento = new DateTime(1985, 5, 20),
                    Cedula = "12345678",
                    Ciudad = "Medellín"
                },
                new Miembro
                {
                    Nombre = "Carlos",
                    Apellido = "Gómez",
                    Celular = "3009876543",
                    CorreoElectronico = "carlos.gomez@email.com",
                    FechaIngreso = new DateTime(2021, 3, 10),
                    Member = 2,
                    Rango = "Rockets",
                    Estatus = "Activo",
                    FechaNacimiento = new DateTime(1990, 8, 15),
                    Cedula = "87654321",
                    Ciudad = "Medellín"
                },
                new Miembro
                {
                    Nombre = "Pedro",
                    Apellido = "Martínez",
                    Celular = "3112345678",
                    CorreoElectronico = "pedro.martinez@email.com",
                    FechaIngreso = new DateTime(2023, 6, 1),
                    Member = 3,
                    Rango = "Prospect",
                    Estatus = "Activo",
                    FechaNacimiento = new DateTime(1995, 11, 30),
                    Cedula = "11223344",
                    Ciudad = "Medellín"
                }
            };

            context.Miembros.AddRange(miembros);
            context.SaveChanges();
        }
    }
}