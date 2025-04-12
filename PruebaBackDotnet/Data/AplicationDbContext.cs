using Microsoft.EntityFrameworkCore;
using PruebaBackDotnet.Models.Entities;

namespace PruebaBackDotnet.Data
{
    public class AplicationDbContext : DbContext
    {
        public AplicationDbContext(DbContextOptions<AplicationDbContext> options) : base(options) { }

        public DbSet<User> Usuarios { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Cargo> Cargos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Crear Cargos
            var cargo1 = new Cargo { Id = Guid.NewGuid(), Codigo = "C001", Nombre = "Desarrollador", Activo = true, IdUsuarioCreacion = 1 };
            var cargo2 = new Cargo { Id = Guid.NewGuid(), Codigo = "C002", Nombre = "Analista", Activo = true, IdUsuarioCreacion = 1 };
            var cargo3 = new Cargo { Id = Guid.NewGuid(), Codigo = "C003", Nombre = "Tester", Activo = true, IdUsuarioCreacion = 1 };

            // Crear Departamentos
            var departamento1 = new Departamento { Id = Guid.NewGuid(), Codigo = "D001", Nombre = "TI", Activo = true, IdUsuarioCreacion = 1 };
            var departamento2 = new Departamento { Id = Guid.NewGuid(), Codigo = "D002", Nombre = "Recursos Humanos", Activo = true, IdUsuarioCreacion = 1 };

            // Insertar Cargos y Departamentos
            modelBuilder.Entity<Cargo>().HasData(cargo1, cargo2, cargo3);
            modelBuilder.Entity<Departamento>().HasData(departamento1, departamento2);

            // Crear Usuarios y asignarles IdCargo e IdDepartamento
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.NewGuid(),
                    Usuario = "jdoe",
                    PrimerNombre = "John",
                    SegundoNombre = "Doe",
                    PrimerApellido = "Doe",
                    SegundoApellido = "Smith",
                    IdDepartamento = departamento1.Id,
                    IdCargo = cargo1.Id
                },
                new User
                {
                    Id = Guid.NewGuid(),
                    Usuario = "mjones",
                    PrimerNombre = "Mary",
                    SegundoNombre = "Jones",
                    PrimerApellido = "Jones",
                    SegundoApellido = "Johnson",
                    IdDepartamento = departamento2.Id,
                    IdCargo = cargo2.Id
                }
            );

            // Definir las relaciones explícitas
            modelBuilder.Entity<User>()
                .HasOne(u => u.Cargo)
                .WithMany(c => c.Usuarios)
                .HasForeignKey(u => u.IdCargo)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Departamento)
                .WithMany(d => d.Usuarios)
                .HasForeignKey(u => u.IdDepartamento)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }



    }
}
