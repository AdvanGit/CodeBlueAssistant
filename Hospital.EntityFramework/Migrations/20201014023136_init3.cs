using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hospital.EntityFramework.Migrations
{
    public partial class init3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "_Adress",
                table: "Staffs",
                newName: "Adress");

            migrationBuilder.RenameColumn(
                name: "_Adress",
                table: "Patients",
                newName: "Adress");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Staffs",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Patients",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Staffs",
                newName: "_Adress");

            migrationBuilder.RenameColumn(
                name: "Adress",
                table: "Patients",
                newName: "_Adress");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Staffs",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreateDate",
                table: "Patients",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
