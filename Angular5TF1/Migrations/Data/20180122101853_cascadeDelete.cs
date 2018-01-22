using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Angular5TF1.Migrations.Data
{
    public partial class cascadeDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_SearchTerms_SearchTermId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_SearchTerms_Wikipedias_WikipediaId",
                table: "SearchTerms");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_SearchTerms_SearchTermId",
                table: "Events",
                column: "SearchTermId",
                principalTable: "SearchTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SearchTerms_Wikipedias_WikipediaId",
                table: "SearchTerms",
                column: "WikipediaId",
                principalTable: "Wikipedias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_SearchTerms_SearchTermId",
                table: "Events");

            migrationBuilder.DropForeignKey(
                name: "FK_SearchTerms_Wikipedias_WikipediaId",
                table: "SearchTerms");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_SearchTerms_SearchTermId",
                table: "Events",
                column: "SearchTermId",
                principalTable: "SearchTerms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_SearchTerms_Wikipedias_WikipediaId",
                table: "SearchTerms",
                column: "WikipediaId",
                principalTable: "Wikipedias",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
