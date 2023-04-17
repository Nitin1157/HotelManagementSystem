using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MVCAppSystem.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "customerHistories",
                columns: table => new
                {
                    CustomerName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoomNo = table.Column<int>(type: "int", nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bed = table.Column<int>(type: "int", nullable: false),
                    IdProof = table.Column<int>(type: "int", nullable: false),
                    EmaiId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PinCode = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckinTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Checkout = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoomRent = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customerHistories", x => x.CustomerName);
                });

            migrationBuilder.CreateTable(
                name: "Logins",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Logins", x => x.Email);
                });

            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    RoomNo = table.Column<int>(type: "int", nullable: false)
                        /*.Annotation("SqlServer:Identity", "1, 1")*/,
                    RoomRent = table.Column<double>(type: "float", nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bed = table.Column<int>(type: "int", nullable: false),
                    IsAllocated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rooms", x => x.RoomNo);
                });

            migrationBuilder.CreateTable(
                name: "staffRegistrations",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_staffRegistrations", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "customerRegisters",
                columns: table => new
                {
                    RoomNo = table.Column<int>(type: "int", nullable: false),
                    RoomType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Bed = table.Column<int>(type: "int", nullable: false),
                    IdProof = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmaiId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    State = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PinCode = table.Column<int>(type: "int", nullable: false),
                    Nationality = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CheckinTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Checkout = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoomRent = table.Column<double>(type: "float", nullable: false),
                    IsAllocated = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customerRegisters", x => x.RoomNo);
                    table.ForeignKey(
                        name: "FK_customerRegisters_rooms_RoomNo",
                        column: x => x.RoomNo,
                        principalTable: "rooms",
                        principalColumn: "RoomNo",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "customerHistories");

            migrationBuilder.DropTable(
                name: "customerRegisters");

            migrationBuilder.DropTable(
                name: "Logins");

            migrationBuilder.DropTable(
                name: "staffRegistrations");

            migrationBuilder.DropTable(
                name: "rooms");
        }
    }
}
