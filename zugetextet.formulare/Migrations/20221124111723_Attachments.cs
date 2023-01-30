using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zugetextet.formulare.Migrations
{
    public partial class Attachments : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "Images",
                table: "FormData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Kurzbibliographie",
                table: "FormData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Kurzbiographie",
                table: "FormData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Lyrik",
                table: "FormData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "ParentalConsent",
                table: "FormData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "Prosa",
                table: "FormData",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Attachment",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attachment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttachmentVersion",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Version = table.Column<int>(type: "INTEGER", nullable: false),
                    FormDataId = table.Column<Guid>(type: "TEXT", nullable: false),
                    OriginalAttachmentId = table.Column<Guid>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: false),
                    FileBytes = table.Column<byte[]>(type: "BLOB", nullable: false),
                    MimeType = table.Column<string>(type: "TEXT", nullable: false),
                    Type = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttachmentVersion", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttachmentVersion_Attachment_OriginalAttachmentId",
                        column: x => x.OriginalAttachmentId,
                        principalTable: "Attachment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttachmentVersion_FormData_FormDataId",
                        column: x => x.FormDataId,
                        principalTable: "FormData",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FormData_Images",
                table: "FormData",
                column: "Images");

            migrationBuilder.CreateIndex(
                name: "IX_FormData_Kurzbibliographie",
                table: "FormData",
                column: "Kurzbibliographie");

            migrationBuilder.CreateIndex(
                name: "IX_FormData_Kurzbiographie",
                table: "FormData",
                column: "Kurzbiographie");

            migrationBuilder.CreateIndex(
                name: "IX_FormData_Lyrik",
                table: "FormData",
                column: "Lyrik");

            migrationBuilder.CreateIndex(
                name: "IX_FormData_ParentalConsent",
                table: "FormData",
                column: "ParentalConsent");

            migrationBuilder.CreateIndex(
                name: "IX_FormData_Prosa",
                table: "FormData",
                column: "Prosa");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentVersion_FormDataId",
                table: "AttachmentVersion",
                column: "FormDataId");

            migrationBuilder.CreateIndex(
                name: "IX_AttachmentVersion_OriginalAttachmentId",
                table: "AttachmentVersion",
                column: "OriginalAttachmentId");

            migrationBuilder.AddForeignKey(
                name: "FK_FormData_Attachment_Images",
                table: "FormData",
                column: "Images",
                principalTable: "Attachment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormData_Attachment_Kurzbibliographie",
                table: "FormData",
                column: "Kurzbibliographie",
                principalTable: "Attachment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormData_Attachment_Kurzbiographie",
                table: "FormData",
                column: "Kurzbiographie",
                principalTable: "Attachment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormData_Attachment_Lyrik",
                table: "FormData",
                column: "Lyrik",
                principalTable: "Attachment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormData_Attachment_ParentalConsent",
                table: "FormData",
                column: "ParentalConsent",
                principalTable: "Attachment",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FormData_Attachment_Prosa",
                table: "FormData",
                column: "Prosa",
                principalTable: "Attachment",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FormData_Attachment_Images",
                table: "FormData");

            migrationBuilder.DropForeignKey(
                name: "FK_FormData_Attachment_Kurzbibliographie",
                table: "FormData");

            migrationBuilder.DropForeignKey(
                name: "FK_FormData_Attachment_Kurzbiographie",
                table: "FormData");

            migrationBuilder.DropForeignKey(
                name: "FK_FormData_Attachment_Lyrik",
                table: "FormData");

            migrationBuilder.DropForeignKey(
                name: "FK_FormData_Attachment_ParentalConsent",
                table: "FormData");

            migrationBuilder.DropForeignKey(
                name: "FK_FormData_Attachment_Prosa",
                table: "FormData");

            migrationBuilder.DropTable(
                name: "AttachmentVersion");

            migrationBuilder.DropTable(
                name: "Attachment");

            migrationBuilder.DropIndex(
                name: "IX_FormData_Images",
                table: "FormData");

            migrationBuilder.DropIndex(
                name: "IX_FormData_Kurzbibliographie",
                table: "FormData");

            migrationBuilder.DropIndex(
                name: "IX_FormData_Kurzbiographie",
                table: "FormData");

            migrationBuilder.DropIndex(
                name: "IX_FormData_Lyrik",
                table: "FormData");

            migrationBuilder.DropIndex(
                name: "IX_FormData_ParentalConsent",
                table: "FormData");

            migrationBuilder.DropIndex(
                name: "IX_FormData_Prosa",
                table: "FormData");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "FormData");

            migrationBuilder.DropColumn(
                name: "Kurzbibliographie",
                table: "FormData");

            migrationBuilder.DropColumn(
                name: "Kurzbiographie",
                table: "FormData");

            migrationBuilder.DropColumn(
                name: "Lyrik",
                table: "FormData");

            migrationBuilder.DropColumn(
                name: "ParentalConsent",
                table: "FormData");

            migrationBuilder.DropColumn(
                name: "Prosa",
                table: "FormData");
        }
    }
}
