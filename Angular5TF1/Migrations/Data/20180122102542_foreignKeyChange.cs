using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Angular5TF1.Migrations.Data
{
    public partial class foreignKeyChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SearchTerms_Wikipedias_WikipediaId",
                table: "SearchTerms");

            migrationBuilder.DropIndex(
                name: "IX_SearchTerms_WikipediaId",
                table: "SearchTerms");

            migrationBuilder.DropColumn(
                name: "WikipediaId",
                table: "SearchTerms");

            migrationBuilder.AddColumn<int>(
                name: "SearchTermId",
                table: "Wikipedias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Wikipedias_SearchTermId",
                table: "Wikipedias",
                column: "SearchTermId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Wikipedias_SearchTerms_SearchTermId",
                table: "Wikipedias",
                column: "SearchTermId",
                principalTable: "SearchTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wikipedias_SearchTerms_SearchTermId",
                table: "Wikipedias");

            migrationBuilder.DropIndex(
                name: "IX_Wikipedias_SearchTermId",
                table: "Wikipedias");

            migrationBuilder.DropColumn(
                name: "SearchTermId",
                table: "Wikipedias");

            migrationBuilder.AddColumn<int>(
                name: "WikipediaId",
                table: "SearchTerms",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SearchTerms_WikipediaId",
                table: "SearchTerms",
                column: "WikipediaId",
                unique: true,
                filter: "[WikipediaId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_SearchTerms_Wikipedias_WikipediaId",
                table: "SearchTerms",
                column: "WikipediaId",
                principalTable: "Wikipedias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
