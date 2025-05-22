using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonelTakipSistemi.Migrations
{
    /// <inheritdoc />
    public partial class UpdateIzinModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OlusturmaTarihi",
                table: "Izinler");

            migrationBuilder.DropColumn(
                name: "OnaylayanKullanici",
                table: "Izinler");

            migrationBuilder.RenameColumn(
                name: "Durum",
                table: "Izinler",
                newName: "OnayDurumu");

            migrationBuilder.AlterColumn<string>(
                name: "Aciklama",
                table: "Izinler",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OnaylayanId",
                table: "Izinler",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Izinler_OnaylayanId",
                table: "Izinler",
                column: "OnaylayanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Izinler_Admins_OnaylayanId",
                table: "Izinler",
                column: "OnaylayanId",
                principalTable: "Admins",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Izinler_Admins_OnaylayanId",
                table: "Izinler");

            migrationBuilder.DropIndex(
                name: "IX_Izinler_OnaylayanId",
                table: "Izinler");

            migrationBuilder.DropColumn(
                name: "OnaylayanId",
                table: "Izinler");

            migrationBuilder.RenameColumn(
                name: "OnayDurumu",
                table: "Izinler",
                newName: "Durum");

            migrationBuilder.AlterColumn<string>(
                name: "Aciklama",
                table: "Izinler",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<DateTime>(
                name: "OlusturmaTarihi",
                table: "Izinler",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "OnaylayanKullanici",
                table: "Izinler",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
