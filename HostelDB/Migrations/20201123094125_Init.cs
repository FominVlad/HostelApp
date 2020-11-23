using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelDB.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Residents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Resident unique identifier.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Surname = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Resident surname."),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Resident name."),
                    Patronymic = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "Resident patronymic."),
                    Birthday = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Resident birthday.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Residents_Id", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Room unique identifier.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaxResidentsCount = table.Column<int>(type: "int", nullable: false, comment: "Maximum number of residents in a room."),
                    Floor = table.Column<int>(type: "int", nullable: false, comment: "Floor number."),
                    Number = table.Column<int>(type: "int", nullable: false, comment: "Room number.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms_Id", x => x.Id);
                    table.UniqueConstraint("AK_Rooms_Floor_Number", x => new { x.Floor, x.Number });
                });

            migrationBuilder.CreateTable(
                name: "RoomResidents",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false, comment: "Room and resident connection unique identifier.")
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoomId = table.Column<int>(type: "int", nullable: false, comment: "Room unique identifier."),
                    ResidentId = table.Column<int>(type: "int", nullable: false, comment: "Resident unique identifier."),
                    SettleDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Resident's settle date."),
                    EvictDate = table.Column<DateTime>(type: "datetime2", nullable: false, comment: "Resident's evicting date.")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomResidents_Id", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoomResidents_ResidentId_Residents_Id",
                        column: x => x.ResidentId,
                        principalTable: "Residents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoomResidents_RoomId_Rooms_Id",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "I_Residents_Surname_Name_Patronymic",
                table: "Residents",
                columns: new[] { "Surname", "Name", "Patronymic" });

            migrationBuilder.CreateIndex(
                name: "I_RoomResidents_EvictDate",
                table: "RoomResidents",
                column: "EvictDate");

            migrationBuilder.CreateIndex(
                name: "I_RoomResidents_SettleDate",
                table: "RoomResidents",
                column: "SettleDate");

            migrationBuilder.CreateIndex(
                name: "IX_RoomResidents_ResidentId",
                table: "RoomResidents",
                column: "ResidentId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomResidents_RoomId",
                table: "RoomResidents",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoomResidents");

            migrationBuilder.DropTable(
                name: "Residents");

            migrationBuilder.DropTable(
                name: "Rooms");
        }
    }
}
