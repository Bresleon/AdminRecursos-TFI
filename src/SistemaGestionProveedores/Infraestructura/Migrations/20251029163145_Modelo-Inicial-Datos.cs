using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class ModeloInicialDatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "proveedores",
                columns: new[] { "Id", "CUIT", "Direccion", "Email", "RazonSocial", "Telefono" },
                values: new object[,]
                {
                    { new Guid("11111111-1111-1111-1111-111111111111"), "30-65432198-9", "Av. Corrientes 2400, CABA", "contacto@techworld.com", "TechWorld S.A.", "011-4785-5566" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), "30-74569852-4", "Av. Rivadavia 9850, CABA", "ventas@pcmax.com", "PCMax SRL", "011-4678-1133" }
                });

            migrationBuilder.InsertData(
                table: "tipos_productos",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Hardware" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Periféricos" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Software" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Redes" }
                });

            migrationBuilder.InsertData(
                table: "productos",
                columns: new[] { "Id", "Nombre", "ProveedorId", "TipoProductoId" },
                values: new object[,]
                {
                    { new Guid("10101010-1010-1010-1010-101010101010"), "Licencia Microsoft Office 365", new Guid("22222222-2222-2222-2222-222222222222"), new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Notebook Lenovo ThinkPad E14", new Guid("11111111-1111-1111-1111-111111111111"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "Mouse Logitech M720", new Guid("11111111-1111-1111-1111-111111111111"), new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "Router TP-Link Archer AX73", new Guid("22222222-2222-2222-2222-222222222222"), new Guid("66666666-6666-6666-6666-666666666666") }
                });

            migrationBuilder.InsertData(
                table: "tecnicos",
                columns: new[] { "Id", "Apellido", "DNI", "Nombre", "ProveedorId", "Telefono" },
                values: new object[,]
                {
                    { new Guid("3cda3b5b-3c70-48c8-bc95-aafdcd88601c"), "Giménez", "33244567", "Javier", new Guid("11111111-1111-1111-1111-111111111111"), "+543814445566" },
                    { new Guid("752b6aa3-4448-47eb-9edf-64c7b2fdcd02"), "Ríos", "37456789", "Lucía", new Guid("22222222-2222-2222-2222-222222222222"), "+543814567891" },
                    { new Guid("ee459fbd-509f-43cb-8068-f8c2047e03f0"), "Coronel", "40899877", "Martín", new Guid("22222222-2222-2222-2222-222222222222"), "+543817894561" }
                });

            migrationBuilder.InsertData(
                table: "visitas",
                columns: new[] { "Id", "Calificacion", "Estado", "FechaHora", "MontoTotal", "Observaciones", "TecnicoId" },
                values: new object[,]
                {
                    { new Guid("7c835130-a8e0-46cc-8aeb-8198a76d7222"), 5.0, "PENDIENTE", new DateTime(2025, 10, 15, 14, 30, 0, 0, DateTimeKind.Unspecified), 165000m, "Adquisición de Notebook Lenovo y Mouse Logitech", new Guid("3cda3b5b-3c70-48c8-bc95-aafdcd88601c") },
                    { new Guid("7c89a11d-8d06-4608-90f9-4bd1d6d2487e"), 4.0, "PENDIENTE", new DateTime(2025, 10, 20, 17, 15, 0, 0, DateTimeKind.Unspecified), 70000m, "Adquisición de Router Tp-Link", new Guid("752b6aa3-4448-47eb-9edf-64c7b2fdcd02") },
                    { new Guid("81a840f4-d3cf-49a1-959f-dcc3e5fbe9cf"), 5.0, "PENDIENTE", new DateTime(2025, 10, 23, 11, 47, 0, 0, DateTimeKind.Unspecified), 180000m, "Adquisición de Paquete Microsoft Office", new Guid("ee459fbd-509f-43cb-8068-f8c2047e03f0") }
                });

            migrationBuilder.InsertData(
                table: "productos_visitas",
                columns: new[] { "Id", "Concepto", "FinGarantia", "NumeroSerie", "Observaciones", "PrecioUnitario", "ProductoId", "VisitaId" },
                values: new object[,]
                {
                    { new Guid("67a48d6c-e3d1-4aa5-bcf8-75f6404a8c8a"), "VENTA", new DateOnly(2027, 10, 15), "SN-LEN-12345", null, 130000m, new Guid("77777777-7777-7777-7777-777777777777"), new Guid("7c835130-a8e0-46cc-8aeb-8198a76d7222") },
                    { new Guid("cfc3972f-981a-4c8e-94a3-cce84d90785e"), "VENTA", new DateOnly(2026, 10, 15), "SN-LOG-45678", null, 35000m, new Guid("88888888-8888-8888-8888-888888888888"), new Guid("7c835130-a8e0-46cc-8aeb-8198a76d7222") },
                    { new Guid("d9b12292-133a-48e5-97e1-fcf2eb39b38d"), "VENTA", new DateOnly(2028, 10, 20), "SN-TPL-98765", null, 70000m, new Guid("99999999-9999-9999-9999-999999999999"), new Guid("7c89a11d-8d06-4608-90f9-4bd1d6d2487e") },
                    { new Guid("e0c65170-653d-4162-83bc-81bce59be1b4"), "VENTA", new DateOnly(2026, 10, 23), "SN-MS-11223", null, 180000m, new Guid("10101010-1010-1010-1010-101010101010"), new Guid("81a840f4-d3cf-49a1-959f-dcc3e5fbe9cf") }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "productos_visitas",
                keyColumn: "Id",
                keyValue: new Guid("67a48d6c-e3d1-4aa5-bcf8-75f6404a8c8a"));

            migrationBuilder.DeleteData(
                table: "productos_visitas",
                keyColumn: "Id",
                keyValue: new Guid("cfc3972f-981a-4c8e-94a3-cce84d90785e"));

            migrationBuilder.DeleteData(
                table: "productos_visitas",
                keyColumn: "Id",
                keyValue: new Guid("d9b12292-133a-48e5-97e1-fcf2eb39b38d"));

            migrationBuilder.DeleteData(
                table: "productos_visitas",
                keyColumn: "Id",
                keyValue: new Guid("e0c65170-653d-4162-83bc-81bce59be1b4"));

            migrationBuilder.DeleteData(
                table: "productos",
                keyColumn: "Id",
                keyValue: new Guid("10101010-1010-1010-1010-101010101010"));

            migrationBuilder.DeleteData(
                table: "productos",
                keyColumn: "Id",
                keyValue: new Guid("77777777-7777-7777-7777-777777777777"));

            migrationBuilder.DeleteData(
                table: "productos",
                keyColumn: "Id",
                keyValue: new Guid("88888888-8888-8888-8888-888888888888"));

            migrationBuilder.DeleteData(
                table: "productos",
                keyColumn: "Id",
                keyValue: new Guid("99999999-9999-9999-9999-999999999999"));

            migrationBuilder.DeleteData(
                table: "visitas",
                keyColumn: "Id",
                keyValue: new Guid("7c835130-a8e0-46cc-8aeb-8198a76d7222"));

            migrationBuilder.DeleteData(
                table: "visitas",
                keyColumn: "Id",
                keyValue: new Guid("7c89a11d-8d06-4608-90f9-4bd1d6d2487e"));

            migrationBuilder.DeleteData(
                table: "visitas",
                keyColumn: "Id",
                keyValue: new Guid("81a840f4-d3cf-49a1-959f-dcc3e5fbe9cf"));

            migrationBuilder.DeleteData(
                table: "tecnicos",
                keyColumn: "Id",
                keyValue: new Guid("3cda3b5b-3c70-48c8-bc95-aafdcd88601c"));

            migrationBuilder.DeleteData(
                table: "tecnicos",
                keyColumn: "Id",
                keyValue: new Guid("752b6aa3-4448-47eb-9edf-64c7b2fdcd02"));

            migrationBuilder.DeleteData(
                table: "tecnicos",
                keyColumn: "Id",
                keyValue: new Guid("ee459fbd-509f-43cb-8068-f8c2047e03f0"));

            migrationBuilder.DeleteData(
                table: "tipos_productos",
                keyColumn: "Id",
                keyValue: new Guid("33333333-3333-3333-3333-333333333333"));

            migrationBuilder.DeleteData(
                table: "tipos_productos",
                keyColumn: "Id",
                keyValue: new Guid("44444444-4444-4444-4444-444444444444"));

            migrationBuilder.DeleteData(
                table: "tipos_productos",
                keyColumn: "Id",
                keyValue: new Guid("55555555-5555-5555-5555-555555555555"));

            migrationBuilder.DeleteData(
                table: "tipos_productos",
                keyColumn: "Id",
                keyValue: new Guid("66666666-6666-6666-6666-666666666666"));

            migrationBuilder.DeleteData(
                table: "proveedores",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"));

            migrationBuilder.DeleteData(
                table: "proveedores",
                keyColumn: "Id",
                keyValue: new Guid("22222222-2222-2222-2222-222222222222"));
        }
    }
}
