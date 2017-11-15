using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SeacDigitTemplate.Migrations
{
    public partial class Startup : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VariazioneF",
                table: "TemplateEffettos");

            migrationBuilder.DropColumn(
                name: "TitoloInapplicabilita",
                table: "SituazioneVoceIVAs");

            migrationBuilder.DropColumn(
                name: "Valore",
                table: "SituazioneVoceIVAs");

            migrationBuilder.DropColumn(
                name: "Variazione",
                table: "SituazioneContos");

            migrationBuilder.AddColumn<string>(
                name: "VariazioneFiscale",
                table: "TemplateEffettos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Trattamento",
                table: "SituazioneVoceIVAs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "AliquotaIvaId",
                table: "SituazioneVoceIVAs",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<decimal>(
                name: "Imponibile",
                table: "SituazioneVoceIVAs",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Iva",
                table: "SituazioneVoceIVAs",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "TitoloInapplicabilitaId",
                table: "SituazioneVoceIVAs",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "VariazioneFiscale",
                table: "SituazioneContos",
                type: "decimal(18, 2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "PercentualeAliquotaIva",
                table: "RigaDigitatas",
                type: "decimal(18, 2)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VariazioneFiscale",
                table: "TemplateEffettos");

            migrationBuilder.DropColumn(
                name: "Imponibile",
                table: "SituazioneVoceIVAs");

            migrationBuilder.DropColumn(
                name: "Iva",
                table: "SituazioneVoceIVAs");

            migrationBuilder.DropColumn(
                name: "TitoloInapplicabilitaId",
                table: "SituazioneVoceIVAs");

            migrationBuilder.DropColumn(
                name: "VariazioneFiscale",
                table: "SituazioneContos");

            migrationBuilder.DropColumn(
                name: "PercentualeAliquotaIva",
                table: "RigaDigitatas");

            migrationBuilder.AddColumn<string>(
                name: "VariazioneF",
                table: "TemplateEffettos",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Trattamento",
                table: "SituazioneVoceIVAs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AliquotaIvaId",
                table: "SituazioneVoceIVAs",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TitoloInapplicabilita",
                table: "SituazioneVoceIVAs",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Valore",
                table: "SituazioneVoceIVAs",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Variazione",
                table: "SituazioneContos",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
