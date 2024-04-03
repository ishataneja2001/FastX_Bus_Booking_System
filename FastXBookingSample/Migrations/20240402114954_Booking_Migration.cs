using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FastXBookingSample.Migrations
{
    public partial class Booking_Migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Amenity",
                columns: table => new
                {
                    AmenityId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AmenityName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Amenity", x => x.AmenityId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Name = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Password = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Address = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Gender = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    ContactNo = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Bus",
                columns: table => new
                {
                    BusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    BusNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    BusType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    NoOfSeats = table.Column<int>(type: "int", nullable: false),
                    Origin = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Destination = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Fare = table.Column<int>(type: "int", nullable: false),
                    BusOperator = table.Column<int>(type: "int", nullable: true),
                    DepartureDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bus", x => x.BusId);
                    table.ForeignKey(
                        name: "FK__Bus__BusOperator__4D94879B",
                        column: x => x.BusOperator,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "BoardingPoints",
                columns: table => new
                {
                    BoardingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Timings = table.Column<TimeSpan>(type: "time", nullable: false),
                    BusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Boarding__057071EA38123087", x => x.BoardingId);
                    table.ForeignKey(
                        name: "FK__BoardingP__BusId__534D60F1",
                        column: x => x.BusId,
                        principalTable: "Bus",
                        principalColumn: "BusId");
                });

            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    BusId = table.Column<int>(type: "int", nullable: true),
                    BookingDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK__Booking__BusId__619B8048",
                        column: x => x.BusId,
                        principalTable: "Bus",
                        principalColumn: "BusId");
                    table.ForeignKey(
                        name: "FK__Booking__UserId__60A75C0F",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "Bus_Amenities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusId = table.Column<int>(type: "int", nullable: true),
                    AmenityId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bus_Amenities", x => x.Id);
                    table.ForeignKey(
                        name: "FK__Bus_Ameni__Ameni__73BA3083",
                        column: x => x.AmenityId,
                        principalTable: "Amenity",
                        principalColumn: "AmenityId");
                    table.ForeignKey(
                        name: "FK__Bus_Ameni__BusId__72C60C4A",
                        column: x => x.BusId,
                        principalTable: "Bus",
                        principalColumn: "BusId");
                });

            migrationBuilder.CreateTable(
                name: "BusSeats",
                columns: table => new
                {
                    SeatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BusId = table.Column<int>(type: "int", nullable: true),
                    SeatNo = table.Column<int>(type: "int", nullable: true),
                    IsBooked = table.Column<bool>(type: "bit", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BusSeats__311713F37D9D17DE", x => x.SeatId);
                    table.ForeignKey(
                        name: "FK__BusSeats__BusId__5070F446",
                        column: x => x.BusId,
                        principalTable: "Bus",
                        principalColumn: "BusId");
                });

            migrationBuilder.CreateTable(
                name: "DroppingPoints",
                columns: table => new
                {
                    DroppingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Timings = table.Column<TimeSpan>(type: "time", nullable: false),
                    BusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Dropping__DCC8059A043CD558", x => x.DroppingId);
                    table.ForeignKey(
                        name: "FK__DroppingP__BusId__5629CD9C",
                        column: x => x.BusId,
                        principalTable: "Bus",
                        principalColumn: "BusId");
                });

            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    RouteId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlaceName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    BusId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.RouteId);
                    table.ForeignKey(
                        name: "FK__Route__BusId__59063A47",
                        column: x => x.BusId,
                        principalTable: "Bus",
                        principalColumn: "BusId");
                });

            migrationBuilder.CreateTable(
                name: "BookingHistory",
                columns: table => new
                {
                    BookId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingId = table.Column<int>(type: "int", nullable: true),
                    UserName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    BusName = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    BusNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    Seats = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    IsCancelled = table.Column<bool>(type: "bit", nullable: false),
                    BookingDateTime = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__BookingH__3DE0C20740FA3004", x => x.BookId);
                    table.ForeignKey(
                        name: "FK__BookingHi__Booki__6FE99F9F",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "BookingId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "Seats",
                columns: table => new
                {
                    SeatId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeatNumber = table.Column<int>(type: "int", nullable: false),
                    BookingId = table.Column<int>(type: "int", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    CardDetails = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seats", x => x.SeatId);
                    table.ForeignKey(
                        name: "FK__Seats__BookingId__6477ECF3",
                        column: x => x.BookingId,
                        principalTable: "Booking",
                        principalColumn: "BookingId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardingPoints_BusId",
                table: "BoardingPoints",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_BusId",
                table: "Booking",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_UserId",
                table: "Booking",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingHistory_BookingId",
                table: "BookingHistory",
                column: "BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_BusOperator",
                table: "Bus",
                column: "BusOperator");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_Amenities_AmenityId",
                table: "Bus_Amenities",
                column: "AmenityId");

            migrationBuilder.CreateIndex(
                name: "IX_Bus_Amenities_BusId",
                table: "Bus_Amenities",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_BusSeats_BusId",
                table: "BusSeats",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_DroppingPoints_BusId",
                table: "DroppingPoints",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_Route_BusId",
                table: "Route",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_Seats_BookingId",
                table: "Seats",
                column: "BookingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardingPoints");

            migrationBuilder.DropTable(
                name: "BookingHistory");

            migrationBuilder.DropTable(
                name: "Bus_Amenities");

            migrationBuilder.DropTable(
                name: "BusSeats");

            migrationBuilder.DropTable(
                name: "DroppingPoints");

            migrationBuilder.DropTable(
                name: "Route");

            migrationBuilder.DropTable(
                name: "Seats");

            migrationBuilder.DropTable(
                name: "Amenity");

            migrationBuilder.DropTable(
                name: "Booking");

            migrationBuilder.DropTable(
                name: "Bus");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
