using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PruebaBackDotnet.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataInitial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Cargos_CargoId",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Departamentos_DepartamentoId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_CargoId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_DepartamentoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "CargoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Usuarios");

            migrationBuilder.InsertData(
                table: "Cargos",
                columns: new[] { "Id", "Activo", "Codigo", "IdUsuarioCreacion", "Nombre" },
                values: new object[,]
                {
                    { new Guid("1a449dc0-f6da-4c90-93f2-d46a1d313f5b"), true, "C001", 1, "Desarrollador" },
                    { new Guid("60eb22e3-94c7-40fd-ac3d-eab04744cb5e"), true, "C003", 1, "Tester" },
                    { new Guid("ee71fc56-718e-4fdf-8ebf-72dfb4d7c1fc"), true, "C002", 1, "Analista" }
                });

            migrationBuilder.InsertData(
                table: "Departamentos",
                columns: new[] { "Id", "Activo", "Codigo", "IdUsuarioCreacion", "Nombre" },
                values: new object[,]
                {
                    { new Guid("943c35c8-23d6-4a95-ab53-fd427ea59f39"), true, "D002", 1, "Recursos Humanos" },
                    { new Guid("f8c52a3c-708b-4c41-934e-c73a81858697"), true, "D001", 1, "TI" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "IdCargo", "IdDepartamento", "PrimerApellido", "PrimerNombre", "SegundoApellido", "SegundoNombre", "Usuario" },
                values: new object[,]
                {
                    { new Guid("6434e312-e1aa-42a1-8595-78ebc76c23c2"), new Guid("ee71fc56-718e-4fdf-8ebf-72dfb4d7c1fc"), new Guid("943c35c8-23d6-4a95-ab53-fd427ea59f39"), "Jones", "Mary", "Johnson", "Jones", "mjones" },
                    { new Guid("f2027cdd-e421-468c-8f84-85f3cf4d6095"), new Guid("1a449dc0-f6da-4c90-93f2-d46a1d313f5b"), new Guid("f8c52a3c-708b-4c41-934e-c73a81858697"), "Doe", "John", "Smith", "Doe", "jdoe" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdCargo",
                table: "Usuarios",
                column: "IdCargo");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_IdDepartamento",
                table: "Usuarios",
                column: "IdDepartamento");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Cargos_IdCargo",
                table: "Usuarios",
                column: "IdCargo",
                principalTable: "Cargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Departamentos_IdDepartamento",
                table: "Usuarios",
                column: "IdDepartamento",
                principalTable: "Departamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Cargos_IdCargo",
                table: "Usuarios");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Departamentos_IdDepartamento",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_IdCargo",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_IdDepartamento",
                table: "Usuarios");

            migrationBuilder.DeleteData(
                table: "Cargos",
                keyColumn: "Id",
                keyValue: new Guid("60eb22e3-94c7-40fd-ac3d-eab04744cb5e"));

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("6434e312-e1aa-42a1-8595-78ebc76c23c2"));

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("f2027cdd-e421-468c-8f84-85f3cf4d6095"));

            migrationBuilder.DeleteData(
                table: "Cargos",
                keyColumn: "Id",
                keyValue: new Guid("1a449dc0-f6da-4c90-93f2-d46a1d313f5b"));

            migrationBuilder.DeleteData(
                table: "Cargos",
                keyColumn: "Id",
                keyValue: new Guid("ee71fc56-718e-4fdf-8ebf-72dfb4d7c1fc"));

            migrationBuilder.DeleteData(
                table: "Departamentos",
                keyColumn: "Id",
                keyValue: new Guid("943c35c8-23d6-4a95-ab53-fd427ea59f39"));

            migrationBuilder.DeleteData(
                table: "Departamentos",
                keyColumn: "Id",
                keyValue: new Guid("f8c52a3c-708b-4c41-934e-c73a81858697"));

            migrationBuilder.AddColumn<Guid>(
                name: "CargoId",
                table: "Usuarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "DepartamentoId",
                table: "Usuarios",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_CargoId",
                table: "Usuarios",
                column: "CargoId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_DepartamentoId",
                table: "Usuarios",
                column: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Cargos_CargoId",
                table: "Usuarios",
                column: "CargoId",
                principalTable: "Cargos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Departamentos_DepartamentoId",
                table: "Usuarios",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
