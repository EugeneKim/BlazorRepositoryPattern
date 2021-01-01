using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Wwg.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Level = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Meaning",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    PartOfSpeech = table.Column<int>(type: "INTEGER", nullable: false),
                    WordId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Meaning", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Meaning_Words_WordId",
                        column: x => x.WordId,
                        principalTable: "Words",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Definition",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Language = table.Column<int>(type: "INTEGER", nullable: false),
                    Define = table.Column<string>(type: "TEXT", nullable: true),
                    Example = table.Column<string>(type: "TEXT", nullable: true),
                    MeaningId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Definition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Definition_Meaning_MeaningId",
                        column: x => x.MeaningId,
                        principalTable: "Meaning",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Antonym",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Word = table.Column<string>(type: "TEXT", nullable: true),
                    DefinitionId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Antonym", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Antonym_Definition_DefinitionId",
                        column: x => x.DefinitionId,
                        principalTable: "Definition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Synonym",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Word = table.Column<string>(type: "TEXT", nullable: true),
                    DefinitionId = table.Column<Guid>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Synonym", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Synonym_Definition_DefinitionId",
                        column: x => x.DefinitionId,
                        principalTable: "Definition",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Antonym_DefinitionId",
                table: "Antonym",
                column: "DefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Definition_MeaningId",
                table: "Definition",
                column: "MeaningId");

            migrationBuilder.CreateIndex(
                name: "IX_Meaning_WordId",
                table: "Meaning",
                column: "WordId");

            migrationBuilder.CreateIndex(
                name: "IX_Synonym_DefinitionId",
                table: "Synonym",
                column: "DefinitionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Antonym");

            migrationBuilder.DropTable(
                name: "Synonym");

            migrationBuilder.DropTable(
                name: "Definition");

            migrationBuilder.DropTable(
                name: "Meaning");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
