using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FindMyRoom.DataService.Migrations
{
    public partial class FMRTimeBox2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "FMRBookingList");

            migrationBuilder.CreateTable(
                name: "FMRGeolocation",
                columns: table => new
                {
                    GeoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoomId = table.Column<int>(nullable: false),
                    Latitude = table.Column<string>(nullable: true),
                    Longitude = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FMRGeolocation", x => x.GeoId);
                    table.ForeignKey(
                        name: "FK_FMRGeolocation_FMRRooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "FMRRooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FMRImages",
                columns: table => new
                {
                    ImageId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoomId = table.Column<int>(nullable: false),
                    RoomImage = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FMRImages", x => x.ImageId);
                    table.ForeignKey(
                        name: "FK_FMRImages_FMRRooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "FMRRooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FMRSociallogin",
                columns: table => new
                {
                    SocialId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<int>(nullable: false),
                    ProviderId = table.Column<string>(nullable: true),
                    ProviderName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FMRSociallogin", x => x.SocialId);
                    table.ForeignKey(
                        name: "FK_FMRSociallogin_FMRUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "FMRUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FMRGeolocation_RoomId",
                table: "FMRGeolocation",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_FMRImages_RoomId",
                table: "FMRImages",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_FMRSociallogin_UserId",
                table: "FMRSociallogin",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FMRGeolocation");

            migrationBuilder.DropTable(
                name: "FMRImages");

            migrationBuilder.DropTable(
                name: "FMRSociallogin");

            migrationBuilder.AddColumn<int>(
                name: "Cost",
                table: "FMRBookingList",
                nullable: false,
                defaultValue: 0);
        }
    }
}
