using Microsoft.EntityFrameworkCore.Migrations;

namespace HostelDB.Migrations
{
    public partial class Add_indexes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "I_Rooms_Id",
                table: "Rooms",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "I_RoomResidents_Id",
                table: "RoomResidents",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "I_Residents_Id",
                table: "Residents",
                column: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "I_Rooms_Id",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "I_RoomResidents_Id",
                table: "RoomResidents");

            migrationBuilder.DropIndex(
                name: "I_Residents_Id",
                table: "Residents");
        }
    }
}
