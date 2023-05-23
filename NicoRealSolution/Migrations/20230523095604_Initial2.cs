using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NicoRealSolution.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Properties_Categories_CategoryId",
                table: "Properties");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Properties_CategoryId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "DescriptionDE",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "DescriptionEN",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "DescriptionRO",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "FeaturesDE",
                table: "Properties");

            migrationBuilder.DropColumn(
                name: "FeaturesEN",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "TitleRO",
                table: "Properties",
                newName: "Title");

            migrationBuilder.RenameColumn(
                name: "TitleEN",
                table: "Properties",
                newName: "Features");

            migrationBuilder.RenameColumn(
                name: "TitleDE",
                table: "Properties",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "FeaturesRO",
                table: "Properties",
                newName: "Category");

            migrationBuilder.AddColumn<bool>(
                name: "IsInvestment",
                table: "Properties",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsInvestment",
                table: "Properties");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "Properties",
                newName: "TitleRO");

            migrationBuilder.RenameColumn(
                name: "Features",
                table: "Properties",
                newName: "TitleEN");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Properties",
                newName: "TitleDE");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Properties",
                newName: "FeaturesRO");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Properties",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "DescriptionDE",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionEN",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DescriptionRO",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FeaturesDE",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FeaturesEN",
                table: "Properties",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategDE = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategEN = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategRO = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Properties_CategoryId",
                table: "Properties",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Properties_Categories_CategoryId",
                table: "Properties",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
