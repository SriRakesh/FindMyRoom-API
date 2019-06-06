using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FindMyRoom.DataService.Migrations
{
    public partial class FMRInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FMRRooms",
                columns: table => new
                {
                    RoomId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Cost = table.Column<int>(nullable: false),
                    NoOfBeds = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Area = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    Pincode = table.Column<int>(nullable: false),
                    Furniture = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FMRRooms", x => x.RoomId);
                });

            migrationBuilder.CreateTable(
                name: "FMRUsers",
                columns: table => new
                {
                    UserId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: false),
                    UserPassword = table.Column<string>(nullable: false),
                    UserEmail = table.Column<string>(nullable: false),
                    UserPhoneNumber = table.Column<string>(nullable: false),
                    UserAddress = table.Column<string>(nullable: false),
                    UserType = table.Column<string>(nullable: true),
                    UserStatus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FMRUsers", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "FMRBookingList",
                columns: table => new
                {
                    BookId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RenterId = table.Column<int>(nullable: false),
                    RoomId = table.Column<int>(nullable: false),
                    Cost = table.Column<int>(nullable: false),
                    Status = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FMRBookingList", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_FMRBookingList_FMRUsers_RenterId",
                        column: x => x.RenterId,
                        principalTable: "FMRUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FMRBookingList_FMRRooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "FMRRooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FMROwners",
                columns: table => new
                {
                    OwnerID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PartnerId = table.Column<int>(nullable: false),
                    RoomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FMROwners", x => x.OwnerID);
                    table.ForeignKey(
                        name: "FK_FMROwners_FMRUsers_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "FMRUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FMROwners_FMRRooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "FMRRooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FMRWishLists",
                columns: table => new
                {
                    WishListId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RenterId = table.Column<int>(nullable: false),
                    RoomId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FMRWishLists", x => x.WishListId);
                    table.ForeignKey(
                        name: "FK_FMRWishLists_FMRUsers_RenterId",
                        column: x => x.RenterId,
                        principalTable: "FMRUsers",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FMRWishLists_FMRRooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "FMRRooms",
                        principalColumn: "RoomId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FMRBookingList_RenterId",
                table: "FMRBookingList",
                column: "RenterId");

            migrationBuilder.CreateIndex(
                name: "IX_FMRBookingList_RoomId",
                table: "FMRBookingList",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_FMROwners_PartnerId",
                table: "FMROwners",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_FMROwners_RoomId",
                table: "FMROwners",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_FMRWishLists_RenterId",
                table: "FMRWishLists",
                column: "RenterId");

            migrationBuilder.CreateIndex(
                name: "IX_FMRWishLists_RoomId",
                table: "FMRWishLists",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FMRBookingList");

            migrationBuilder.DropTable(
                name: "FMROwners");

            migrationBuilder.DropTable(
                name: "FMRWishLists");

            migrationBuilder.DropTable(
                name: "FMRUsers");

            migrationBuilder.DropTable(
                name: "FMRRooms");
        }
    }
}
