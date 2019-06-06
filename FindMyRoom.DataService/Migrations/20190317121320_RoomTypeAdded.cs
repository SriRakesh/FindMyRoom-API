using Microsoft.EntityFrameworkCore.Migrations;

namespace FindMyRoom.DataService.Migrations
{
    public partial class RoomTypeAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "NoOfBeds",
                table: "FMRRooms",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<string>(
                name: "RoomType",
                table: "FMRRooms",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RoomType",
                table: "FMRRooms");

            migrationBuilder.AlterColumn<string>(
                name: "NoOfBeds",
                table: "FMRRooms",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
