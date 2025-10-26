using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace proyectoIngSoft.Data.Migrations
{
    /// <inheritdoc />
    public partial class fixmigracióntotal : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DbSetCodigoSocial",
                columns: table => new
                {
                    IdCodigo = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Codigo = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    Rol = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbSetCodigoSocial", x => x.IdCodigo);
                });

            migrationBuilder.CreateTable(
                name: "DbSetNotificacionSimulada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    De = table.Column<string>(type: "text", nullable: false),
                    Para = table.Column<string>(type: "text", nullable: false),
                    Tipo = table.Column<string>(type: "text", nullable: false),
                    Mensaje = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DbSetNotificacionSimulada", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Titulo = table.Column<string>(type: "text", nullable: false),
                    Mensaje = table.Column<string>(type: "text", nullable: false),
                    Estado = table.Column<string>(type: "text", nullable: false),
                    Fecha = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Detalle = table.Column<string>(type: "text", nullable: false),
                    DocumentoAdjuntos = table.Column<List<string>>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "t_Accidente",
                columns: table => new
                {
                    IdAccidente = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreComp = table.Column<string>(type: "text", nullable: false),
                    DNI = table.Column<int>(type: "integer", nullable: false),
                    FechaIni = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaFin = table.Column<DateOnly>(type: "date", nullable: false),
                    Observaciones = table.Column<string>(type: "text", nullable: false),
                    TipoDM = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Accidente", x => x.IdAccidente);
                });

            migrationBuilder.CreateTable(
                name: "t_Enfermedad",
                columns: table => new
                {
                    IdEnfermedad = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubtipoSol = table.Column<string>(type: "text", nullable: false),
                    FechaIni = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaFin = table.Column<DateOnly>(type: "date", nullable: false),
                    NombreMedi = table.Column<string>(type: "text", nullable: false),
                    CentroMedico = table.Column<string>(type: "text", nullable: false),
                    DiasDesc = table.Column<int>(type: "integer", nullable: false),
                    Diagnostico = table.Column<string>(type: "text", nullable: false),
                    DescEnfe = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Enfermedad", x => x.IdEnfermedad);
                });

            migrationBuilder.CreateTable(
                name: "t_EnfermedadFamiliar",
                columns: table => new
                {
                    IdEnfermedadFam = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreFamiliar = table.Column<string>(type: "text", nullable: false),
                    FechaIni = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaFin = table.Column<DateOnly>(type: "date", nullable: false),
                    Parentesco = table.Column<string>(type: "text", nullable: false),
                    CentroMedico = table.Column<string>(type: "text", nullable: false),
                    Medico = table.Column<string>(type: "text", nullable: false),
                    NumeroCMP = table.Column<string>(type: "text", nullable: false),
                    FechaDiag = table.Column<DateOnly>(type: "date", nullable: false),
                    DiaSoli = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_EnfermedadFamiliar", x => x.IdEnfermedadFam);
                });

            migrationBuilder.CreateTable(
                name: "t_Fallecimiento",
                columns: table => new
                {
                    IdFallec = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NombreFallec = table.Column<string>(type: "text", nullable: false),
                    FechaIni = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaFin = table.Column<DateOnly>(type: "date", nullable: false),
                    Parentesco = table.Column<string>(type: "text", nullable: false),
                    FechaComun = table.Column<DateOnly>(type: "date", nullable: false),
                    LugarSep = table.Column<string>(type: "text", nullable: false),
                    Traslado = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Fallecimiento", x => x.IdFallec);
                });

            migrationBuilder.CreateTable(
                name: "t_Maternidad",
                columns: table => new
                {
                    IdMater = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaParto = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaIni = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaFin = table.Column<DateOnly>(type: "date", nullable: false),
                    SemanasGest = table.Column<int>(type: "integer", nullable: false),
                    PartoMult = table.Column<string>(type: "text", nullable: false),
                    FechaUltM = table.Column<DateOnly>(type: "date", nullable: false),
                    CentroMed = table.Column<string>(type: "text", nullable: false),
                    MedicoT = table.Column<string>(type: "text", nullable: false),
                    Descripcion = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Maternidad", x => x.IdMater);
                });

            migrationBuilder.CreateTable(
                name: "t_Paternidad",
                columns: table => new
                {
                    IdPater = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FechaParto = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaIni = table.Column<DateOnly>(type: "date", nullable: false),
                    FechaFin = table.Column<DateOnly>(type: "date", nullable: false),
                    NombrePareja = table.Column<string>(type: "text", nullable: false),
                    TipoSituacion = table.Column<string>(type: "text", nullable: false),
                    CentroMed = table.Column<string>(type: "text", nullable: false),
                    FechaComun = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Paternidad", x => x.IdPater);
                });

            migrationBuilder.CreateTable(
                name: "t_TiposDescanso",
                columns: table => new
                {
                    IdTDescanso = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nombre = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_TiposDescanso", x => x.IdTDescanso);
                });

            migrationBuilder.CreateTable(
                name: "ValidarDatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DNI = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    Ubigeo = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    Captcha = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidarDatos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "T_Usuarios",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Apellidos = table.Column<string>(type: "text", nullable: false),
                    Dni = table.Column<string>(type: "text", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Telefono = table.Column<string>(type: "text", nullable: false),
                    Ubigeo = table.Column<string>(type: "text", nullable: false),
                    Distrito = table.Column<string>(type: "text", nullable: false),
                    RazonSocial = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    IdCodigo = table.Column<int>(type: "integer", nullable: true),
                    CargoLaboral = table.Column<string>(type: "text", nullable: true),
                    ConfirmarPassword = table.Column<string>(type: "text", nullable: false),
                    Rol = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_T_Usuarios", x => x.IdUser);
                    table.ForeignKey(
                        name: "FK_T_Usuarios_DbSetCodigoSocial_IdCodigo",
                        column: x => x.IdCodigo,
                        principalTable: "DbSetCodigoSocial",
                        principalColumn: "IdCodigo");
                });

            migrationBuilder.CreateTable(
                name: "t_Descanso",
                columns: table => new
                {
                    IdDescanso = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    TipoDescansoId = table.Column<int>(type: "integer", nullable: false),
                    FechaIni = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    FechaSolicitud = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccidenteId = table.Column<int>(type: "integer", nullable: true),
                    MaternidadId = table.Column<int>(type: "integer", nullable: true),
                    PaternidadId = table.Column<int>(type: "integer", nullable: true),
                    EnfermedadId = table.Column<int>(type: "integer", nullable: true),
                    FallecimientoId = table.Column<int>(type: "integer", nullable: true),
                    EnfermedadFamId = table.Column<int>(type: "integer", nullable: true),
                    EstadoESSALUD = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EstadoSubsidioA = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    EstadoSubsidioJ = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_t_Descanso", x => x.IdDescanso);
                    table.ForeignKey(
                        name: "FK_t_Descanso_T_Usuarios_UserId",
                        column: x => x.UserId,
                        principalTable: "T_Usuarios",
                        principalColumn: "IdUser",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_t_Descanso_t_Accidente_AccidenteId",
                        column: x => x.AccidenteId,
                        principalTable: "t_Accidente",
                        principalColumn: "IdAccidente");
                    table.ForeignKey(
                        name: "FK_t_Descanso_t_EnfermedadFamiliar_EnfermedadFamId",
                        column: x => x.EnfermedadFamId,
                        principalTable: "t_EnfermedadFamiliar",
                        principalColumn: "IdEnfermedadFam");
                    table.ForeignKey(
                        name: "FK_t_Descanso_t_Enfermedad_EnfermedadId",
                        column: x => x.EnfermedadId,
                        principalTable: "t_Enfermedad",
                        principalColumn: "IdEnfermedad");
                    table.ForeignKey(
                        name: "FK_t_Descanso_t_Fallecimiento_FallecimientoId",
                        column: x => x.FallecimientoId,
                        principalTable: "t_Fallecimiento",
                        principalColumn: "IdFallec");
                    table.ForeignKey(
                        name: "FK_t_Descanso_t_Maternidad_MaternidadId",
                        column: x => x.MaternidadId,
                        principalTable: "t_Maternidad",
                        principalColumn: "IdMater");
                    table.ForeignKey(
                        name: "FK_t_Descanso_t_Paternidad_PaternidadId",
                        column: x => x.PaternidadId,
                        principalTable: "t_Paternidad",
                        principalColumn: "IdPater");
                    table.ForeignKey(
                        name: "FK_t_Descanso_t_TiposDescanso_TipoDescansoId",
                        column: x => x.TipoDescansoId,
                        principalTable: "t_TiposDescanso",
                        principalColumn: "IdTDescanso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DocumentosMedicos",
                columns: table => new
                {
                    IdDocumento = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DescansoId = table.Column<int>(type: "integer", nullable: false),
                    Nombre = table.Column<string>(type: "text", nullable: false),
                    Tamaño = table.Column<long>(type: "bigint", nullable: false),
                    FechaSubida = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Archivo = table.Column<byte[]>(type: "bytea", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentosMedicos", x => x.IdDocumento);
                    table.ForeignKey(
                        name: "FK_DocumentosMedicos_t_Descanso_DescansoId",
                        column: x => x.DescansoId,
                        principalTable: "t_Descanso",
                        principalColumn: "IdDescanso",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "t_TiposDescanso",
                columns: new[] { "IdTDescanso", "Nombre" },
                values: new object[,]
                {
                    { 1, "Enfermedad" },
                    { 2, "Maternidad" },
                    { 3, "Paternidad" },
                    { 4, "Fallecimiento Familiar" },
                    { 5, "Enfermedad Familiar" },
                    { 6, "Accidente" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_DocumentosMedicos_DescansoId",
                table: "DocumentosMedicos",
                column: "DescansoId");

            migrationBuilder.CreateIndex(
                name: "IX_t_Descanso_AccidenteId",
                table: "t_Descanso",
                column: "AccidenteId");

            migrationBuilder.CreateIndex(
                name: "IX_t_Descanso_EnfermedadFamId",
                table: "t_Descanso",
                column: "EnfermedadFamId");

            migrationBuilder.CreateIndex(
                name: "IX_t_Descanso_EnfermedadId",
                table: "t_Descanso",
                column: "EnfermedadId");

            migrationBuilder.CreateIndex(
                name: "IX_t_Descanso_FallecimientoId",
                table: "t_Descanso",
                column: "FallecimientoId");

            migrationBuilder.CreateIndex(
                name: "IX_t_Descanso_MaternidadId",
                table: "t_Descanso",
                column: "MaternidadId");

            migrationBuilder.CreateIndex(
                name: "IX_t_Descanso_PaternidadId",
                table: "t_Descanso",
                column: "PaternidadId");

            migrationBuilder.CreateIndex(
                name: "IX_t_Descanso_TipoDescansoId",
                table: "t_Descanso",
                column: "TipoDescansoId");

            migrationBuilder.CreateIndex(
                name: "IX_t_Descanso_UserId",
                table: "t_Descanso",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_T_Usuarios_IdCodigo",
                table: "T_Usuarios",
                column: "IdCodigo");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DbSetNotificacionSimulada");

            migrationBuilder.DropTable(
                name: "DocumentosMedicos");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "ValidarDatos");

            migrationBuilder.DropTable(
                name: "t_Descanso");

            migrationBuilder.DropTable(
                name: "T_Usuarios");

            migrationBuilder.DropTable(
                name: "t_Accidente");

            migrationBuilder.DropTable(
                name: "t_EnfermedadFamiliar");

            migrationBuilder.DropTable(
                name: "t_Enfermedad");

            migrationBuilder.DropTable(
                name: "t_Fallecimiento");

            migrationBuilder.DropTable(
                name: "t_Maternidad");

            migrationBuilder.DropTable(
                name: "t_Paternidad");

            migrationBuilder.DropTable(
                name: "t_TiposDescanso");

            migrationBuilder.DropTable(
                name: "DbSetCodigoSocial");
        }
    }
}
