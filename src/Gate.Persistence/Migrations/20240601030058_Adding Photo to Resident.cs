using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gate.Persistence.Migrations
{
    public partial class AddingPhototoResident : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoBase64",
                table: "RESIDENTS",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoBase64",
                table: "RESIDENTS");
        }
    }
}
