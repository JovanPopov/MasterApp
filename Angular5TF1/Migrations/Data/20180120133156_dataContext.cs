using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Angular5TF1.Migrations.Data
{
    public partial class dataContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Wikipedias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wikipedias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchTerms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SearchDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Term = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WikipediaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchTerms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SearchTerms_Wikipedias_WikipediaId",
                        column: x => x.WikipediaId,
                        principalTable: "Wikipedias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    SearchTermId = table.Column<int>(type: "int", nullable: true),
                    banner_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    end_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    event_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    event_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    eventname = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    featured = table.Column<int>(type: "int", nullable: false),
                    full_address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    label = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    location = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    object_type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    share_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    start_time = table.Column<int>(type: "int", nullable: false),
                    start_time_display = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    thumb_url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    thumb_url_large = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_SearchTerms_SearchTermId",
                        column: x => x.SearchTermId,
                        principalTable: "SearchTerms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Events_SearchTermId",
                table: "Events",
                column: "SearchTermId");

            migrationBuilder.CreateIndex(
                name: "IX_SearchTerms_WikipediaId",
                table: "SearchTerms",
                column: "WikipediaId",
                unique: true,
                filter: "[WikipediaId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "SearchTerms");

            migrationBuilder.DropTable(
                name: "Wikipedias");
        }
    }
}
