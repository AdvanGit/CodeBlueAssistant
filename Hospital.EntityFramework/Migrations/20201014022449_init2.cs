using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.EntityFramework.Migrations
{
    public partial class init2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_User_Belays_BelayId",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropIndex(
                name: "IX_User_BelayId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BelayCode",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BelayDateOut",
                table: "User");

            migrationBuilder.DropColumn(
                name: "BelayId",
                table: "User");

            migrationBuilder.DropColumn(
                name: "HasChild",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsMarried",
                table: "User");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "User");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Staffs");

            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Staffs",
                newName: "_Adress");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "Staffs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "Staffs",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Staffs",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<bool>(
                name: "IsEnabled",
                table: "Staffs",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldNullable: true,
                oldDefaultValue: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    MidName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    PhoneNumeber = table.Column<long>(nullable: false),
                    BirthDay = table.Column<DateTime>(nullable: false),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    _Adress = table.Column<string>(nullable: true),
                    Gender = table.Column<byte>(nullable: false),
                    BelayId = table.Column<int>(nullable: true),
                    BelayCode = table.Column<int>(nullable: false),
                    BelayDateOut = table.Column<DateTime>(nullable: false),
                    IsMarried = table.Column<bool>(nullable: false),
                    HasChild = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Patients_Belays_BelayId",
                        column: x => x.BelayId,
                        principalTable: "Belays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Patients_BelayId",
                table: "Patients",
                column: "BelayId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Staffs",
                table: "Staffs");

            migrationBuilder.RenameTable(
                name: "Staffs",
                newName: "User");

            migrationBuilder.RenameColumn(
                name: "_Adress",
                table: "User",
                newName: "Adress");

            migrationBuilder.AlterColumn<string>(
                name: "LastName",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsEnabled",
                table: "User",
                type: "bit",
                nullable: true,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "FirstName",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "User",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime));

            migrationBuilder.AddColumn<int>(
                name: "BelayCode",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "BelayDateOut",
                table: "User",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BelayId",
                table: "User",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "HasChild",
                table: "User",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsMarried",
                table: "User",
                type: "bit",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_User_BelayId",
                table: "User",
                column: "BelayId");

            migrationBuilder.AddForeignKey(
                name: "FK_User_Belays_BelayId",
                table: "User",
                column: "BelayId",
                principalTable: "Belays",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
