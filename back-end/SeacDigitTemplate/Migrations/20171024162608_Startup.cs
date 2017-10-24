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
                    Percentuale = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AliquotaIvas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ApplicazioneTemplateEffettos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AliquotaIva = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContoAvere = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContoDare = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imponibile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iva = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PercentualeIndeducibilita = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PercentualeIndetraibilita = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Settore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitoloInapplicabilita = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trattamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoceIva = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicazioneTemplateEffettos", x => x.Id);
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
                name: "TitoloInapplicabilitas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TitoloInapplicabilitas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VoceIvas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VoceIvas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TemplateEffettos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AliquotaIvaId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ApplicazioneTemplateEffettoId = table.Column<int>(type: "int", nullable: false),
                    ContoAvereId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContoDareId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataOperazione = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imponibile = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Iva = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RifRow = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TitoloInapplicabilitaId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Trattamento = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Valore = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VariazioneF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VoceIvaId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateEffettos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TemplateEffettos_ApplicazioneTemplateEffettos_ApplicazioneTemplateEffettoId",
                        column: x => x.ApplicazioneTemplateEffettoId,
                        principalTable: "ApplicazioneTemplateEffettos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Documentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Caratteristica = table.Column<int>(type: "int", nullable: false),
                    CliforId = table.Column<int>(type: "int", nullable: false),
                    Numero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Protocollo = table.Column<int>(type: "int", nullable: false),
                    Registro = table.Column<int>(type: "int", nullable: false),
                    RiferimentoDocumentoId = table.Column<int>(type: "int", nullable: false),
                    RitenutaAcconto = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Sospeso = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<int>(type: "int", nullable: false),
                    Totale = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
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
                    Valore = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Variazione = table.Column<decimal>(type: "decimal(18, 2)", nullable: false)
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
                    VoceIvaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AliquotaIvaId = table.Column<int>(type: "int", nullable: false),
                    TitoloInapplicabilita = table.Column<int>(type: "int", nullable: false),
                    Trattamento = table.Column<int>(type: "int", nullable: false),
                    Valore = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    VoceIvaId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SituazioneVoceIVAs", x => x.VoceIvaId);
                    table.ForeignKey(
                        name: "FK_SituazioneVoceIVAs_AliquotaIvas_AliquotaIvaId",
                        column: x => x.AliquotaIvaId,
                        principalTable: "AliquotaIvas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SituazioneVoceIVAs_VoceIvas_VoceIvaId1",
                        column: x => x.VoceIvaId1,
                        principalTable: "VoceIvas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RigaDigitatas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AliquotaIvaId = table.Column<int>(type: "int", nullable: true),
                    ContoAvereId = table.Column<int>(type: "int", nullable: true),
                    ContoDareId = table.Column<int>(type: "int", nullable: true),
                    DocumentoId = table.Column<int>(type: "int", nullable: false),
                    Imponibile = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    Iva = table.Column<decimal>(type: "decimal(18, 2)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PercentualeIndeducibilita = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    PercentualeIndetraibilita = table.Column<decimal>(type: "decimal(18, 2)", nullable: false),
                    Settore = table.Column<int>(type: "int", nullable: true),
                    TitoloInapplicabilitaId = table.Column<int>(type: "int", nullable: true),
                    Trattamento = table.Column<int>(type: "int", nullable: true),
                    VoceIvaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RigaDigitatas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RigaDigitatas_AliquotaIvas_AliquotaIvaId",
                        column: x => x.AliquotaIvaId,
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
                        name: "FK_RigaDigitatas_TitoloInapplicabilitas_TitoloInapplicabilitaId",
                        column: x => x.TitoloInapplicabilitaId,
                        principalTable: "TitoloInapplicabilitas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RigaDigitatas_VoceIvas_VoceIvaId",
                        column: x => x.VoceIvaId,
                        principalTable: "VoceIvas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Documentos_CliforId",
                table: "Documentos",
                column: "CliforId");

            migrationBuilder.CreateIndex(
                name: "IX_RigaDigitatas_AliquotaIvaId",
                table: "RigaDigitatas",
                column: "AliquotaIvaId");

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
                name: "IX_RigaDigitatas_TitoloInapplicabilitaId",
                table: "RigaDigitatas",
                column: "TitoloInapplicabilitaId");

            migrationBuilder.CreateIndex(
                name: "IX_RigaDigitatas_VoceIvaId",
                table: "RigaDigitatas",
                column: "VoceIvaId");

            migrationBuilder.CreateIndex(
                name: "IX_SituazioneContos_ContoId1",
                table: "SituazioneContos",
                column: "ContoId1");

            migrationBuilder.CreateIndex(
                name: "IX_SituazioneVoceIVAs_AliquotaIvaId",
                table: "SituazioneVoceIVAs",
                column: "AliquotaIvaId");

            migrationBuilder.CreateIndex(
                name: "IX_SituazioneVoceIVAs_VoceIvaId1",
                table: "SituazioneVoceIVAs",
                column: "VoceIvaId1");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateEffettos_ApplicazioneTemplateEffettoId",
                table: "TemplateEffettos",
                column: "ApplicazioneTemplateEffettoId");
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
                name: "TemplateEffettos");

            migrationBuilder.DropTable(
                name: "Documentos");

            migrationBuilder.DropTable(
                name: "TitoloInapplicabilitas");

            migrationBuilder.DropTable(
                name: "Contos");

            migrationBuilder.DropTable(
                name: "AliquotaIvas");

            migrationBuilder.DropTable(
                name: "VoceIvas");

            migrationBuilder.DropTable(
                name: "ApplicazioneTemplateEffettos");

            migrationBuilder.DropTable(
                name: "Clifors");
        }
    }
}
