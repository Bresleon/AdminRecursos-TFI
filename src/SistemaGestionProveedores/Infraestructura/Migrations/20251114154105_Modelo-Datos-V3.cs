using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class ModeloDatosV3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Activado",
                table: "tecnicos",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "Activado",
                table: "proveedores",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.UpdateData(
                table: "proveedores",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Activado",
                value: 1);

            migrationBuilder.UpdateData(
                table: "proveedores",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"),
                column: "Activado",
                value: 1);

            migrationBuilder.UpdateData(
                table: "tecnicos",
                keyColumn: "Id",
                keyValue: new Guid("3cda3b5b-3c70-48c8-bc95-aafdcd88601c"),
                column: "Activado",
                value: 1);

            migrationBuilder.UpdateData(
                table: "tecnicos",
                keyColumn: "Id",
                keyValue: new Guid("752b6aa3-4448-47eb-9edf-64c7b2fdcd02"),
                column: "Activado",
                value: 1);

            migrationBuilder.UpdateData(
                table: "tecnicos",
                keyColumn: "Id",
                keyValue: new Guid("ee459fbd-509f-43cb-8068-f8c2047e03f0"),
                column: "Activado",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Activado",
                table: "tecnicos");

            migrationBuilder.DropColumn(
                name: "Activado",
                table: "proveedores");
        }
    }
}
