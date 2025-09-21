using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations.AppDb
{
    /// <inheritdoc />
    public partial class AddPageSectionPhotos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ContentAR",
                table: "PageSections",
                newName: "ContentAr");

            migrationBuilder.CreateTable(
                name: "PageSectionPhoto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FilePath = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UploadedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PageSectionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PageSectionPhoto", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PageSectionPhoto_PageSections_PageSectionId",
                        column: x => x.PageSectionId,
                        principalTable: "PageSections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PageSectionPhoto_PageSectionId",
                table: "PageSectionPhoto",
                column: "PageSectionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PageSectionPhoto");

            migrationBuilder.RenameColumn(
                name: "ContentAr",
                table: "PageSections",
                newName: "ContentAR");
        }
    }
}
