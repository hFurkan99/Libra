using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book.Data.Migrations
{
    /// <inheritdoc />
    public partial class addingBookModule : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "book");

            migrationBuilder.CreateTable(
                name: "BookItems",
                schema: "book",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CatalogBookId = table.Column<Guid>(type: "uuid", nullable: false),
                    Barcode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    Location = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    ConditionNote = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true, defaultValue: "Available"),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookItems", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BookStocks",
                schema: "book",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CatalogBookId = table.Column<Guid>(type: "uuid", nullable: false),
                    Total = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    Available = table.Column<int>(type: "integer", nullable: false, defaultValue: 0),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<string>(type: "text", nullable: true),
                    LastModified = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookStocks", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookItems",
                schema: "book");

            migrationBuilder.DropTable(
                name: "BookStocks",
                schema: "book");
        }
    }
}
