using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SeacDigitTemplate.Migrations
{
    public partial class Template : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RigaDigitatas_AliquotaIvas_AliquotaIVAId",
                table: "RigaDigitatas");

            migrationBuilder.DropForeignKey(
                name: "FK_SituazioneVoceIVAs_AliquotaIvas_AliquotaIVAId",
                table: "SituazioneVoceIVAs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AliquotaIvas",
                table: "AliquotaIvas");

            migrationBuilder.DropColumn(
                name: "DocumentId",
                table: "RigaDigitatas");

            migrationBuilder.RenameTable(
                name: "AliquotaIvas",
                newName: "AliquotaIVAs");

            migrationBuilder.AlterColumn<decimal>(
                name: "Valore",
                table: "SituazioneVoceIVAs",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "Variazione",
                table: "SituazioneContos",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "Valore",
                table: "SituazioneContos",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentualeIndetraibilita",
                table: "RigaDigitatas",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "PercentualeIndeducibilita",
                table: "RigaDigitatas",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "Imponibile",
                table: "RigaDigitatas",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "IVA",
                table: "RigaDigitatas",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "DocumentoId",
                table: "RigaDigitatas",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Totale",
                table: "Documentos",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "RitenutaAcconto",
                table: "Documentos",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<decimal>(
                name: "Percentuale",
                table: "AliquotaIVAs",
                type: "decimal(18, 2)",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_AliquotaIVAs",
                table: "AliquotaIVAs",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RigaDigitatas_AliquotaIVAs_AliquotaIVAId",
                table: "RigaDigitatas",
                column: "AliquotaIVAId",
                principalTable: "AliquotaIVAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SituazioneVoceIVAs_AliquotaIVAs_AliquotaIVAId",
                table: "SituazioneVoceIVAs",
                column: "AliquotaIVAId",
                principalTable: "AliquotaIVAs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RigaDigitatas_AliquotaIVAs_AliquotaIVAId",
                table: "RigaDigitatas");

            migrationBuilder.DropForeignKey(
                name: "FK_SituazioneVoceIVAs_AliquotaIVAs_AliquotaIVAId",
                table: "SituazioneVoceIVAs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_AliquotaIVAs",
                table: "AliquotaIVAs");

            migrationBuilder.RenameTable(
                name: "AliquotaIVAs",
                newName: "AliquotaIvas");

            migrationBuilder.AlterColumn<int>(
                name: "Valore",
                table: "SituazioneVoceIVAs",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<int>(
                name: "Variazione",
                table: "SituazioneContos",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<int>(
                name: "Valore",
                table: "SituazioneContos",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<int>(
                name: "PercentualeIndetraibilita",
                table: "RigaDigitatas",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<int>(
                name: "PercentualeIndeducibilita",
                table: "RigaDigitatas",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<int>(
                name: "Imponibile",
                table: "RigaDigitatas",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<int>(
                name: "IVA",
                table: "RigaDigitatas",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<int>(
                name: "DocumentoId",
                table: "RigaDigitatas",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "DocumentId",
                table: "RigaDigitatas",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Totale",
                table: "Documentos",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<int>(
                name: "RitenutaAcconto",
                table: "Documentos",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AlterColumn<int>(
                name: "Percentuale",
                table: "AliquotaIvas",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18, 2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AliquotaIvas",
                table: "AliquotaIvas",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RigaDigitatas_AliquotaIvas_AliquotaIVAId",
                table: "RigaDigitatas",
                column: "AliquotaIVAId",
                principalTable: "AliquotaIvas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SituazioneVoceIVAs_AliquotaIvas_AliquotaIVAId",
                table: "SituazioneVoceIVAs",
                column: "AliquotaIVAId",
                principalTable: "AliquotaIvas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
