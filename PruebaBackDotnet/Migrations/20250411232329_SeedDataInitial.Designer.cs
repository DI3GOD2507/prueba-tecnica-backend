﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PruebaBackDotnet.Data;

#nullable disable

namespace PruebaBackDotnet.Migrations
{
    [DbContext(typeof(AplicationDbContext))]
    [Migration("20250411232329_SeedDataInitial")]
    partial class SeedDataInitial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PruebaBackDotnet.Models.Entities.Cargo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdUsuarioCreacion")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Cargos");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1a449dc0-f6da-4c90-93f2-d46a1d313f5b"),
                            Activo = true,
                            Codigo = "C001",
                            IdUsuarioCreacion = 1,
                            Nombre = "Desarrollador"
                        },
                        new
                        {
                            Id = new Guid("ee71fc56-718e-4fdf-8ebf-72dfb4d7c1fc"),
                            Activo = true,
                            Codigo = "C002",
                            IdUsuarioCreacion = 1,
                            Nombre = "Analista"
                        },
                        new
                        {
                            Id = new Guid("60eb22e3-94c7-40fd-ac3d-eab04744cb5e"),
                            Activo = true,
                            Codigo = "C003",
                            IdUsuarioCreacion = 1,
                            Nombre = "Tester"
                        });
                });

            modelBuilder.Entity("PruebaBackDotnet.Models.Entities.Departamento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Activo")
                        .HasColumnType("bit");

                    b.Property<string>("Codigo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("IdUsuarioCreacion")
                        .HasColumnType("int");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Departamentos");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f8c52a3c-708b-4c41-934e-c73a81858697"),
                            Activo = true,
                            Codigo = "D001",
                            IdUsuarioCreacion = 1,
                            Nombre = "TI"
                        },
                        new
                        {
                            Id = new Guid("943c35c8-23d6-4a95-ab53-fd427ea59f39"),
                            Activo = true,
                            Codigo = "D002",
                            IdUsuarioCreacion = 1,
                            Nombre = "Recursos Humanos"
                        });
                });

            modelBuilder.Entity("PruebaBackDotnet.Models.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdCargo")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("IdDepartamento")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("PrimerApellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PrimerNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SegundoApellido")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("SegundoNombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Usuario")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("IdCargo");

                    b.HasIndex("IdDepartamento");

                    b.ToTable("Usuarios");

                    b.HasData(
                        new
                        {
                            Id = new Guid("f2027cdd-e421-468c-8f84-85f3cf4d6095"),
                            IdCargo = new Guid("1a449dc0-f6da-4c90-93f2-d46a1d313f5b"),
                            IdDepartamento = new Guid("f8c52a3c-708b-4c41-934e-c73a81858697"),
                            PrimerApellido = "Doe",
                            PrimerNombre = "John",
                            SegundoApellido = "Smith",
                            SegundoNombre = "Doe",
                            Usuario = "jdoe"
                        },
                        new
                        {
                            Id = new Guid("6434e312-e1aa-42a1-8595-78ebc76c23c2"),
                            IdCargo = new Guid("ee71fc56-718e-4fdf-8ebf-72dfb4d7c1fc"),
                            IdDepartamento = new Guid("943c35c8-23d6-4a95-ab53-fd427ea59f39"),
                            PrimerApellido = "Jones",
                            PrimerNombre = "Mary",
                            SegundoApellido = "Johnson",
                            SegundoNombre = "Jones",
                            Usuario = "mjones"
                        });
                });

            modelBuilder.Entity("PruebaBackDotnet.Models.Entities.User", b =>
                {
                    b.HasOne("PruebaBackDotnet.Models.Entities.Cargo", "Cargo")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdCargo")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("PruebaBackDotnet.Models.Entities.Departamento", "Departamento")
                        .WithMany("Usuarios")
                        .HasForeignKey("IdDepartamento")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Cargo");

                    b.Navigation("Departamento");
                });

            modelBuilder.Entity("PruebaBackDotnet.Models.Entities.Cargo", b =>
                {
                    b.Navigation("Usuarios");
                });

            modelBuilder.Entity("PruebaBackDotnet.Models.Entities.Departamento", b =>
                {
                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
