using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastXBookingSample.Migrations
{
    public partial class Upd_migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookingDateTime",
                table: "Booking");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDateTime",
                table: "Seats",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "BoardingId",
                table: "Booking",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DroppingId",
                table: "Booking",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_BoardingId",
                table: "Booking",
                column: "BoardingId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_DroppingId",
                table: "Booking",
                column: "DroppingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_BoardingId",
                table: "Booking",
                column: "BoardingId",
                principalTable: "BoardingPoints",
                principalColumn: "BoardingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_DroppingId",
                table: "Booking",
                column: "DroppingId",
                principalTable: "DroppingPoints",
                principalColumn: "DroppingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_BoardingId",
                table: "Booking");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_DroppingId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_BoardingId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_DroppingId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "BookingDateTime",
                table: "Seats");

            migrationBuilder.DropColumn(
                name: "BoardingId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "DroppingId",
                table: "Booking");

            migrationBuilder.AddColumn<DateTime>(
                name: "BookingDateTime",
                table: "Booking",
                type: "datetime",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
