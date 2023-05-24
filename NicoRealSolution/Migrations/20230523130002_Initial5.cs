using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NicoRealSolution.Migrations
{
    public partial class Initial5 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Bathrooms",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ConstructionYear",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Floors",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rooms",
                table: "Properties",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Zip",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bathrooms",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "ConstructionYear",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Floors",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Rooms",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "Zip",
                table: "Properties");
        }
    }
}
