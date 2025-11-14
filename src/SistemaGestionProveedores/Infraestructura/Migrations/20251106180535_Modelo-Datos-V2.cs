using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infraestructura.Migrations
{
    /// <inheritdoc />
    public partial class ModeloDatosV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "productos_visitas");

            migrationBuilder.DropTable(
                name: "productos");

            migrationBuilder.DropTable(
                name: "visitas");

            migrationBuilder.DropTable(
                name: "tipos_productos");

            migrationBuilder.CreateTable(
                name: "tipos_equipos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_equipos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tipos_mantenimiento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_mantenimiento", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "equipos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProveedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoEquipoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_equipos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_equipos_proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_equipos_tipos_equipos_TipoEquipoId",
                        column: x => x.TipoEquipoId,
                        principalTable: "tipos_equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "adquisiciones",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    EquipoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TecnicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    NumeroSerie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FechaAdquisicion = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaFinGarantia = table.Column<DateOnly>(type: "date", nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_adquisiciones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_adquisiciones_equipos_EquipoId",
                        column: x => x.EquipoId,
                        principalTable: "equipos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_adquisiciones_tecnicos_TecnicoId",
                        column: x => x.TecnicoId,
                        principalTable: "tecnicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "mantenimientos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    AdquisicionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TecnicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoMantenimientoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fecha = table.Column<DateOnly>(type: "date", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Costo = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Calificacion = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mantenimientos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_mantenimientos_adquisiciones_AdquisicionId",
                        column: x => x.AdquisicionId,
                        principalTable: "adquisiciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_mantenimientos_tecnicos_TecnicoId",
                        column: x => x.TecnicoId,
                        principalTable: "tecnicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_mantenimientos_tipos_mantenimiento_TipoMantenimientoId",
                        column: x => x.TipoMantenimientoId,
                        principalTable: "tipos_mantenimiento",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "proveedores",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Calificacion",
                value: 4.25);

            migrationBuilder.UpdateData(
                table: "tecnicos",
                keyColumn: "Id",
                keyValue: new Guid("3cda3b5b-3c70-48c8-bc95-aafdcd88601c"),
                column: "Calificacion",
                value: 4.5);

            migrationBuilder.UpdateData(
                table: "tecnicos",
                keyColumn: "Id",
                keyValue: new Guid("752b6aa3-4448-47eb-9edf-64c7b2fdcd02"),
                columns: new[] { "Calificacion", "ProveedorId" },
                values: new object[] { 4.0, new Guid("11111111-1111-1111-1111-111111111111") });

            migrationBuilder.InsertData(
                table: "tipos_equipos",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { new Guid("33333333-3333-3333-3333-333333333333"), "Hardware" },
                    { new Guid("44444444-4444-4444-4444-444444444444"), "Periféricos" },
                    { new Guid("55555555-5555-5555-5555-555555555555"), "Software" },
                    { new Guid("66666666-6666-6666-6666-666666666666"), "Redes" }
                });

            migrationBuilder.InsertData(
                table: "tipos_mantenimiento",
                columns: new[] { "Id", "Descripcion" },
                values: new object[,]
                {
                    { new Guid("1b74f1e6-18a3-40a7-bd25-279f97329661"), "Reparación en garantía" },
                    { new Guid("7f900d5c-45b5-424e-9a4c-7663c9acce1e"), "Instalación" },
                    { new Guid("97e99a3b-e5a1-4ac4-9d60-eac409c3d681"), "Reparación fuera de garantía" },
                    { new Guid("9de506e3-e483-4ce1-b44e-9d66ac847f8c"), "Actualización" }
                });

            migrationBuilder.InsertData(
                table: "equipos",
                columns: new[] { "Id", "Nombre", "ProveedorId", "TipoEquipoId" },
                values: new object[,]
                {
                    { new Guid("10101010-1010-1010-1010-101010101010"), "Licencia Microsoft Office 365", new Guid("22222222-2222-2222-2222-222222222222"), new Guid("55555555-5555-5555-5555-555555555555") },
                    { new Guid("77777777-7777-7777-7777-777777777777"), "Notebook Lenovo ThinkPad E14", new Guid("11111111-1111-1111-1111-111111111111"), new Guid("33333333-3333-3333-3333-333333333333") },
                    { new Guid("88888888-8888-8888-8888-888888888888"), "Mouse Logitech M720", new Guid("11111111-1111-1111-1111-111111111111"), new Guid("44444444-4444-4444-4444-444444444444") },
                    { new Guid("99999999-9999-9999-9999-999999999999"), "Router TP-Link Archer AX73", new Guid("22222222-2222-2222-2222-222222222222"), new Guid("66666666-6666-6666-6666-666666666666") }
                });

            migrationBuilder.InsertData(
                table: "adquisiciones",
                columns: new[] { "Id", "Costo", "EquipoId", "FechaAdquisicion", "FechaFinGarantia", "NumeroSerie", "TecnicoId" },
                values: new object[,]
                {
                    { new Guid("67a48d6c-e3d1-4aa5-bcf8-75f6404a8c8a"), 130000m, new Guid("77777777-7777-7777-7777-777777777777"), new DateOnly(2025, 10, 15), new DateOnly(2027, 10, 15), "SN-LEN-12345", new Guid("3cda3b5b-3c70-48c8-bc95-aafdcd88601c") },
                    { new Guid("cfc3972f-981a-4c8e-94a3-cce84d90785e"), 35000m, new Guid("88888888-8888-8888-8888-888888888888"), new DateOnly(2025, 10, 15), new DateOnly(2026, 10, 15), "SN-LOG-45678", new Guid("3cda3b5b-3c70-48c8-bc95-aafdcd88601c") },
                    { new Guid("d9b12292-133a-48e5-97e1-fcf2eb39b38d"), 70000m, new Guid("99999999-9999-9999-9999-999999999999"), new DateOnly(2025, 10, 20), new DateOnly(2028, 10, 20), "SN-TPL-98765", new Guid("3cda3b5b-3c70-48c8-bc95-aafdcd88601c") },
                    { new Guid("e0c65170-653d-4162-83bc-81bce59be1b4"), 180000m, new Guid("10101010-1010-1010-1010-101010101010"), new DateOnly(2025, 10, 23), new DateOnly(2026, 10, 23), "SN-MS-11223", new Guid("3cda3b5b-3c70-48c8-bc95-aafdcd88601c") }
                });

            migrationBuilder.InsertData(
                table: "mantenimientos",
                columns: new[] { "Id", "AdquisicionId", "Calificacion", "Costo", "Descripcion", "Estado", "Fecha", "TecnicoId", "TipoMantenimientoId" },
                values: new object[,]
                {
                    { new Guid("9f0e8550-0c39-4cf2-8255-852c72fe9aab"), new Guid("d9b12292-133a-48e5-97e1-fcf2eb39b38d"), 4.0, 15000m, "Actualización del firmware del router para mejorar la seguridad.", "FINALIZADO", new DateOnly(2026, 11, 27), new Guid("752b6aa3-4448-47eb-9edf-64c7b2fdcd02"), new Guid("9de506e3-e483-4ce1-b44e-9d66ac847f8c") },
                    { new Guid("cf58e1e4-cfc9-4dc4-8066-c82c0c62181d"), new Guid("67a48d6c-e3d1-4aa5-bcf8-75f6404a8c8a"), 4.5, 0m, "Instalación inicial del sistema operativo y software básico.", "FINALIZADO", new DateOnly(2025, 11, 1), new Guid("3cda3b5b-3c70-48c8-bc95-aafdcd88601c"), new Guid("7f900d5c-45b5-424e-9a4c-7663c9acce1e") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_adquisiciones_EquipoId",
                table: "adquisiciones",
                column: "EquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_adquisiciones_TecnicoId",
                table: "adquisiciones",
                column: "TecnicoId");

            migrationBuilder.CreateIndex(
                name: "IX_equipos_ProveedorId",
                table: "equipos",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_equipos_TipoEquipoId",
                table: "equipos",
                column: "TipoEquipoId");

            migrationBuilder.CreateIndex(
                name: "IX_mantenimientos_AdquisicionId",
                table: "mantenimientos",
                column: "AdquisicionId");

            migrationBuilder.CreateIndex(
                name: "IX_mantenimientos_TecnicoId",
                table: "mantenimientos",
                column: "TecnicoId");

            migrationBuilder.CreateIndex(
                name: "IX_mantenimientos_TipoMantenimientoId",
                table: "mantenimientos",
                column: "TipoMantenimientoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "mantenimientos");

            migrationBuilder.DropTable(
                name: "adquisiciones");

            migrationBuilder.DropTable(
                name: "tipos_mantenimiento");

            migrationBuilder.DropTable(
                name: "equipos");

            migrationBuilder.DropTable(
                name: "tipos_equipos");

            migrationBuilder.CreateTable(
                name: "tipos_productos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tipos_productos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "visitas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TecnicoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Calificacion = table.Column<double>(type: "float", nullable: false, defaultValue: 0.0),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MontoTotal = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visitas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_visitas_tecnicos_TecnicoId",
                        column: x => x.TecnicoId,
                        principalTable: "tecnicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productos",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProveedorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TipoProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productos_proveedores_ProveedorId",
                        column: x => x.ProveedorId,
                        principalTable: "proveedores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productos_tipos_productos_TipoProductoId",
                        column: x => x.TipoProductoId,
                        principalTable: "tipos_productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "productos_visitas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    VisitaId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Concepto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FinGarantia = table.Column<DateOnly>(type: "date", nullable: false),
                    NumeroSerie = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Observaciones = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    PrecioUnitario = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_productos_visitas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_productos_visitas_productos_ProductoId",
                        column: x => x.ProductoId,
                        principalTable: "productos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_productos_visitas_visitas_VisitaId",
                        column: x => x.VisitaId,
                        principalTable: "visitas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "proveedores",
                keyColumn: "Id",
                keyValue: new Guid("11111111-1111-1111-1111-111111111111"),
                column: "Calificacion",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "tecnicos",
                keyColumn: "Id",
                keyValue: new Guid("3cda3b5b-3c70-48c8-bc95-aafdcd88601c"),
                column: "Calificacion",
                value: 0.0);

            migrationBuilder.UpdateData(
                table: "tecnicos",
                keyColumn: "Id",
                keyValue: new Guid("752b6aa3-4448-47eb-9edf-64c7b2fdcd02"),
                columns: new[] { "Calificacion", "ProveedorId" },
                values: new object[] { 0.0, new Guid("22222222-2222-2222-2222-222222222222") });

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
                table: "visitas",
                columns: new[] { "Id", "Calificacion", "Estado", "FechaHora", "MontoTotal", "Observaciones", "TecnicoId" },
                values: new object[,]
                {
                    { new Guid("7c835130-a8e0-46cc-8aeb-8198a76d7222"), 5.0, "PENDIENTE", new DateTime(2025, 10, 15, 14, 30, 0, 0, DateTimeKind.Unspecified), 165000m, "Adquisición de Notebook Lenovo y Mouse Logitech", new Guid("3cda3b5b-3c70-48c8-bc95-aafdcd88601c") },
                    { new Guid("7c89a11d-8d06-4608-90f9-4bd1d6d2487e"), 4.0, "PENDIENTE", new DateTime(2025, 10, 20, 17, 15, 0, 0, DateTimeKind.Unspecified), 70000m, "Adquisición de Router Tp-Link", new Guid("752b6aa3-4448-47eb-9edf-64c7b2fdcd02") },
                    { new Guid("81a840f4-d3cf-49a1-959f-dcc3e5fbe9cf"), 5.0, "PENDIENTE", new DateTime(2025, 10, 23, 11, 47, 0, 0, DateTimeKind.Unspecified), 180000m, "Adquisición de Paquete Microsoft Office", new Guid("ee459fbd-509f-43cb-8068-f8c2047e03f0") }
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
                table: "productos_visitas",
                columns: new[] { "Id", "Concepto", "FinGarantia", "NumeroSerie", "Observaciones", "PrecioUnitario", "ProductoId", "VisitaId" },
                values: new object[,]
                {
                    { new Guid("67a48d6c-e3d1-4aa5-bcf8-75f6404a8c8a"), "VENTA", new DateOnly(2027, 10, 15), "SN-LEN-12345", null, 130000m, new Guid("77777777-7777-7777-7777-777777777777"), new Guid("7c835130-a8e0-46cc-8aeb-8198a76d7222") },
                    { new Guid("cfc3972f-981a-4c8e-94a3-cce84d90785e"), "VENTA", new DateOnly(2026, 10, 15), "SN-LOG-45678", null, 35000m, new Guid("88888888-8888-8888-8888-888888888888"), new Guid("7c835130-a8e0-46cc-8aeb-8198a76d7222") },
                    { new Guid("d9b12292-133a-48e5-97e1-fcf2eb39b38d"), "VENTA", new DateOnly(2028, 10, 20), "SN-TPL-98765", null, 70000m, new Guid("99999999-9999-9999-9999-999999999999"), new Guid("7c89a11d-8d06-4608-90f9-4bd1d6d2487e") },
                    { new Guid("e0c65170-653d-4162-83bc-81bce59be1b4"), "VENTA", new DateOnly(2026, 10, 23), "SN-MS-11223", null, 180000m, new Guid("10101010-1010-1010-1010-101010101010"), new Guid("81a840f4-d3cf-49a1-959f-dcc3e5fbe9cf") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_productos_ProveedorId",
                table: "productos",
                column: "ProveedorId");

            migrationBuilder.CreateIndex(
                name: "IX_productos_TipoProductoId",
                table: "productos",
                column: "TipoProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_productos_visitas_ProductoId",
                table: "productos_visitas",
                column: "ProductoId");

            migrationBuilder.CreateIndex(
                name: "IX_productos_visitas_VisitaId",
                table: "productos_visitas",
                column: "VisitaId");

            migrationBuilder.CreateIndex(
                name: "IX_visitas_TecnicoId",
                table: "visitas",
                column: "TecnicoId");
        }
    }
}
