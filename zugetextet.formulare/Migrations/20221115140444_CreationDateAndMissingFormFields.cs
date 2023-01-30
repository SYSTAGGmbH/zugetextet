using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zugetextet.formulare.Migrations
{
    public partial class CreationDateAndMissingFormFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "Forms",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "FormData",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "ConditionsOfParticipationConsent",
                table: "FormData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationDate",
                table: "FormData",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "FormData",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "FormData",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsUnderage",
                table: "FormData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "OriginatorAndPublicationConsent",
                table: "FormData",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "Forms");

            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "FormData");

            migrationBuilder.DropColumn(
                name: "ConditionsOfParticipationConsent",
                table: "FormData");

            migrationBuilder.DropColumn(
                name: "CreationDate",
                table: "FormData");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "FormData");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "FormData");

            migrationBuilder.DropColumn(
                name: "IsUnderage",
                table: "FormData");

            migrationBuilder.DropColumn(
                name: "OriginatorAndPublicationConsent",
                table: "FormData");
        }
    }
}
