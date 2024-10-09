using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class AddClientsTP2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "name",
                table: "TaskStatus",
                newName: "Name");

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientID", "Address", "Company", "CreateDate", "Email", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "calle 54 4713, Buenos Aires, Argentina", "Tech Solutions", new DateTime(2024, 1, 15, 10, 30, 0, 0, DateTimeKind.Unspecified), "melinaccora97@gmail.com", "Melina Cora", "1153311347" },
                    { 2, "Av. Corrientes 1234, Buenos Aires, Argentina", "Empresa Global S.A.", new DateTime(2024, 2, 20, 11, 45, 0, 0, DateTimeKind.Unspecified), "martin.cora.72@gmail.com", "MArtin Cora", "1123545655" },
                    { 3, "Bv. Mitre 150, Rosario, Argentina", "Ingeniería y Construcciones", new DateTime(2024, 3, 10, 9, 20, 0, 0, DateTimeKind.Unspecified), "cvalenzuela@gmail.com", "Cristian Valenzuela", "1167812345" },
                    { 4, "San Martín 987, Mendoza, Argentina", "Consultoría Integral", new DateTime(2024, 4, 5, 14, 50, 0, 0, DateTimeKind.Unspecified), "tahielbenjaminvalenzuela@gmail.com", "Tahiel Valenzuela", "1123235454" },
                    { 5, "Calle Independencia 345, La Plata, Argentina", "Sofland Argentina", new DateTime(2024, 5, 25, 16, 10, 0, 0, DateTimeKind.Unspecified), "lofranco73@gmail.com", "Lorena Franco", "1154843550" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientID",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientID",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientID",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientID",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "ClientID",
                keyValue: 5);

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "TaskStatus",
                newName: "name");
        }
    }
}
