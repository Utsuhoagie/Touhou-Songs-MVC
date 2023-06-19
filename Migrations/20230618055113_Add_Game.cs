using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Touhou_Songs_MVC.Migrations
{
    /// <inheritdoc />
    public partial class Add_Game : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NameCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumberCode = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    YearReleased = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");
        }
    }
}
