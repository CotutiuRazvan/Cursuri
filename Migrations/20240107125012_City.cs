using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cursuri.Migrations
{
    public partial class City : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CityID",
                table: "Course",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "City",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_City", x => x.ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Course_CityID",
                table: "Course",
                column: "CityID");

            migrationBuilder.AddForeignKey(
                name: "FK_Course_City_CityID",
                table: "Course",
                column: "CityID",
                principalTable: "City",
                principalColumn: "ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Course_City_CityID",
                table: "Course");

            migrationBuilder.DropTable(
                name: "City");

            migrationBuilder.DropIndex(
                name: "IX_Course_CityID",
                table: "Course");

            migrationBuilder.DropColumn(
                name: "CityID",
                table: "Course");
        }
    }
}
