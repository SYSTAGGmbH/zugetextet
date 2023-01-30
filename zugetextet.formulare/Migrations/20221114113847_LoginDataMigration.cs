using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace zugetextet.formulare.Migrations
{
    public partial class LoginDataMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoginData",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginData", x => x.Id);
                });

            string username = Program.AppMetaData.InitialUsername;
            string password = Program.AppMetaData.InitialPasswordSHA512Hash;
            
            migrationBuilder.InsertData(
               table: "LoginData",
               columns: new[] { "Id", "Username", "Password" },
               values: new object[,]
               {
                    { Guid.NewGuid(), username, password },
               }
           );

          

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginData");
        }
    }
}
