using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NicoRealSolution.Migrations
{
    public partial class Initial6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsInvestment",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsFeatured",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFeatured",
                table: "Properties");

            migrationBuilder.AlterColumn<bool>(
                name: "IsInvestment",
                table: "Properties",
                type: "bit",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");
        }
    }
}
