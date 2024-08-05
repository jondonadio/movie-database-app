using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MovieDatabaseApp.Migrations
{
    // Migration class to create the initial database schema
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        // Defines operations to be applied when migrating up
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Creates the Movies table with the specified columns and types
            migrationBuilder.CreateTable(
                name: "Movies",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Genre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movies", x => x.Id);
                });
        }
        // Defines operations to be applied when migrating down (rollback)
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movies");
        }
    }
}
