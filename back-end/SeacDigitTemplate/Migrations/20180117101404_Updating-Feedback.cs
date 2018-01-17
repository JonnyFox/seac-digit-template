using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SeacDigitTemplate.Migrations
{
    public partial class UpdatingFeedback : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DocumentoId",
                table: "FeedbackList",
                type: "int",
                nullable: false,
                defaultValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_FeedbackList_DocumentoId",
                table: "FeedbackList",
                column: "DocumentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_FeedbackList_Documentos_DocumentoId",
                table: "FeedbackList",
                column: "DocumentoId",
                principalTable: "Documentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeedbackList_Documentos_DocumentoId",
                table: "FeedbackList");

            migrationBuilder.DropIndex(
                name: "IX_FeedbackList_DocumentoId",
                table: "FeedbackList");

            migrationBuilder.DropColumn(
                name: "DocumentoId",
                table: "FeedbackList");
        }
    }
}
