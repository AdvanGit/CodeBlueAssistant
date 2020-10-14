using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.EntityFramework.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Belays",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: true),
                    Info = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Belays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    MidName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    PhoneNumeber = table.Column<long>(nullable: false),
                    BirthDay = table.Column<DateTime>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETDATE()"),
                    Adress = table.Column<string>(nullable: true),
                    Gender = table.Column<byte>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    BelayId = table.Column<int>(nullable: true),
                    BelayCode = table.Column<int>(nullable: true),
                    BelayDateOut = table.Column<DateTime>(nullable: true),
                    IsMarried = table.Column<bool>(nullable: true),
                    HasChild = table.Column<bool>(nullable: true),
                    IsEnabled = table.Column<bool>(nullable: true, defaultValue: true),
                    Qualification = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                    table.ForeignKey(
                        name: "FK_User_Belays_BelayId",
                        column: x => x.BelayId,
                        principalTable: "Belays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_User_BelayId",
                table: "User",
                column: "BelayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropTable(
                name: "Belays");
        }
    }
}
