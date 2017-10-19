using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SeacDigitTemplate.Migrations
{
    public partial class Startup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AliquotaIvas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Percentuale = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AliquotaIvas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clifors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Istituzionale = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Soggetto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clifors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoceIVAs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoceIVAs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Caratteristica = table.Column<int>(type: "int", nullable: false),
                    CliforId = table.Column<int>(type: "int", nullable: false),
                    Registro = table.Column<int>(type: "int", nullable: false),
                    RiferimentoDocumentoId = table.Column<int>(type: "int", nullable: false),
                    RitenutaAcconto = table.Column<int>(type: "int", nullable: false),
                    Sospeso = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Totale = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Documentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Documentos_Clifors_CliforId",
                        column: x => x.CliforId,
                        principalTable: "Clifors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SituazioneContos",
                columns: table => new
                {
                    ContoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ContoId1 = table.Column<int>(type: "int", nullable: true),
                    Valore = table.Column<int>(type: "int", nullable: false),
                    Variazione = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SituazioneContos", x => x.ContoId);
                    table.ForeignKey(
                        name: "FK_SituazioneContos_Contos_ContoId1",
                        column: x => x.ContoId1,
                        principalTable: "Contos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SituazioneVoceIVAs",
                columns: table => new
                {
                    VoceIVAId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AliquotaIVAId = table.Column<int>(type: "int", nullable: false),
                    TitoloInapplicabilita = table.Column<int>(type: "int", nullable: false),
                    Trattamento = table.Column<int>(type: "int", nullable: false),
                    Valore = table.Column<int>(type: "int", nullable: false),
                    VoceIVAId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SituazioneVoceIVAs", x => x.VoceIVAId);
                    table.ForeignKey(
                        name: "FK_SituazioneVoceIVAs_AliquotaIvas_AliquotaIVAId",
                        column: x => x.AliquotaIVAId,
                        principalTable: "AliquotaIvas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SituazioneVoceIVAs_VoceIVAs_VoceIVAId1",
                        column: x => x.VoceIVAId1,
                        principalTable: "VoceIVAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RigaDigitatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AliquotaIVAId = table.Column<int>(type: "int", nullable: false),
                    ContoAvereId = table.Column<int>(type: "int", nullable: false),
                    ContoDareId = table.Column<int>(type: "int", nullable: false),
                    DocumentId = table.Column<int>(type: "int", nullable: false),
                    DocumentoId = table.Column<int>(type: "int", nullable: true),
                    IVA = table.Column<int>(type: "int", nullable: false),
                    Imponibile = table.Column<int>(type: "int", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PercentualeIndeducibilita = table.Column<int>(type: "int", nullable: false),
                    PercentualeIndetraibilita = table.Column<int>(type: "int", nullable: false),
                    Settore = table.Column<int>(type: "int", nullable: false),
                    TitoloInapplicabilita = table.Column<int>(type: "int", nullable: false),
                    Trattamento = table.Column<int>(type: "int", nullable: false),
                    VoceIVAId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RigaDigitatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RigaDigitatas_AliquotaIvas_AliquotaIVAId",
                        column: x => x.AliquotaIVAId,
                        principalTable: "AliquotaIvas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RigaDigitatas_Contos_ContoAvereId",
                        column: x => x.ContoAvereId,
                        principalTable: "Contos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RigaDigitatas_Contos_ContoDareId",
                        column: x => x.ContoDareId,
                        principalTable: "Contos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RigaDigitatas_Documentos_DocumentoId",
                        column: x => x.DocumentoId,
                        principalTable: "Documentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RigaDigitatas_VoceIVAs_VoceIVAId",
                        column: x => x.VoceIVAId,
                        principalTable: "VoceIVAs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_CliforId",
                table: "Documentos",
                column: "CliforId");

            migrationBuilder.CreateIndex(
                name: "IX_RigaDigitatas_AliquotaIVAId",
                table: "RigaDigitatas",
                column: "AliquotaIVAId");

            migrationBuilder.CreateIndex(
                name: "IX_RigaDigitatas_ContoAvereId",
                table: "RigaDigitatas",
                column: "ContoAvereId");

            migrationBuilder.CreateIndex(
                name: "IX_RigaDigitatas_ContoDareId",
                table: "RigaDigitatas",
                column: "ContoDareId");

            migrationBuilder.CreateIndex(
                name: "IX_RigaDigitatas_DocumentoId",
                table: "RigaDigitatas",
                column: "DocumentoId");

            migrationBuilder.CreateIndex(
                name: "IX_RigaDigitatas_VoceIVAId",
                table: "RigaDigitatas",
                column: "VoceIVAId");

            migrationBuilder.CreateIndex(
                name: "IX_SituazioneContos_ContoId1",
                table: "SituazioneContos",
                column: "ContoId1");

            migrationBuilder.CreateIndex(
                name: "IX_SituazioneVoceIVAs_AliquotaIVAId",
                table: "SituazioneVoceIVAs",
                column: "AliquotaIVAId");

            migrationBuilder.CreateIndex(
                name: "IX_SituazioneVoceIVAs_VoceIVAId1",
                table: "SituazioneVoceIVAs",
                column: "VoceIVAId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RigaDigitatas");

            migrationBuilder.DropTable(
                name: "SituazioneContos");

            migrationBuilder.DropTable(
                name: "SituazioneVoceIVAs");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "Contos");

            migrationBuilder.DropTable(
                name: "AliquotaIvas");

            migrationBuilder.DropTable(
                name: "VoceIVAs");

            migrationBuilder.DropTable(
                name: "Clifors");
        }
    }
}
