using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zugetextet.formulare.Migrations
{
    public partial class NewFormAttributes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmountLyrik",
                table: "Forms",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "ImagesIsVisible",
                table: "Forms",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "ProsaIsVisible",
                table: "Forms",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AmountLyrik",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "ImagesIsVisible",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "ProsaIsVisible",
                table: "Forms");
        }
    }
}
