using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PruebaBackDotnet.Migrations
{
    /// <inheritdoc />
    public partial class UsuarioUnico : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AlterColumn<string>(
                name: "Usuario",
                table: "Usuarios",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Cargos",
                columns: new[] { "Id", "Activo", "Codigo", "IdUsuarioCreacion", "Nombre" },
                values: new object[,]
                {
                    { new Guid("099c91db-2f14-441c-9d73-60f0f4388b2f"), true, "C001", 1, "Desarrollador" },
                    { new Guid("3c853ca0-ae07-4f9e-af22-b54a46ab26b1"), true, "C002", 1, "Analista" },
                    { new Guid("c2c48250-a919-4d26-9d7f-e26c365992ee"), true, "C003", 1, "Tester" }
                });

            migrationBuilder.InsertData(
                table: "Departamentos",
                columns: new[] { "Id", "Activo", "Codigo", "IdUsuarioCreacion", "Nombre" },
                values: new object[,]
                {
                    { new Guid("2ac1b857-37ca-4169-ab21-36dad77ab81a"), true, "D001", 1, "TI" },
                    { new Guid("dfbd8911-9c55-4e75-b6f8-291485ccf4bc"), true, "D002", 1, "Recursos Humanos" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "IdCargo", "IdDepartamento", "PrimerApellido", "PrimerNombre", "SegundoApellido", "SegundoNombre", "Usuario" },
                values: new object[,]
                {
                    { new Guid("72372272-c28f-449f-b473-7f824358d23f"), new Guid("3c853ca0-ae07-4f9e-af22-b54a46ab26b1"), new Guid("dfbd8911-9c55-4e75-b6f8-291485ccf4bc"), "Jones", "Mary", "Johnson", "Jones", "mjones" },
                    { new Guid("decc4131-06ef-49b6-9e7f-97dfbf75d290"), new Guid("099c91db-2f14-441c-9d73-60f0f4388b2f"), new Guid("2ac1b857-37ca-4169-ab21-36dad77ab81a"), "Doe", "John", "Smith", "Doe", "jdoe" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Usuario_Unique",
                table: "Usuarios",
                column: "Usuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_Usuario_Unique",
                table: "Usuarios");

            migrationBuilder.DeleteData(
                table: "Cargos",
                keyColumn: "Id",
                keyValue: new Guid("c2c48250-a919-4d26-9d7f-e26c365992ee"));

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("72372272-c28f-449f-b473-7f824358d23f"));

            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("decc4131-06ef-49b6-9e7f-97dfbf75d290"));

            migrationBuilder.DeleteData(
                table: "Cargos",
                keyColumn: "Id",
                keyValue: new Guid("099c91db-2f14-441c-9d73-60f0f4388b2f"));

            migrationBuilder.DeleteData(
                table: "Cargos",
                keyColumn: "Id",
                keyValue: new Guid("3c853ca0-ae07-4f9e-af22-b54a46ab26b1"));

            migrationBuilder.DeleteData(
                table: "Departamentos",
                keyColumn: "Id",
                keyValue: new Guid("2ac1b857-37ca-4169-ab21-36dad77ab81a"));

            migrationBuilder.DeleteData(
                table: "Departamentos",
                keyColumn: "Id",
                keyValue: new Guid("dfbd8911-9c55-4e75-b6f8-291485ccf4bc"));

            migrationBuilder.AlterColumn<string>(
                name: "Usuario",
                table: "Usuarios",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

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
        }
    }
}
