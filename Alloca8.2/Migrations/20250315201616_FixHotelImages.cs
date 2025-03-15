using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alloca8._2.Migrations
{
    public partial class FixHotelImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelImagees_Hotels_HotelsHotelID",
                table: "HotelImagees");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelImagees_Rooms_RoomsRoomID",
                table: "HotelImagees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_AspNetUsers_HotelOwnerId",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_HotelOwnerId",
                table: "Hotels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelImagees",
                table: "HotelImagees");

            migrationBuilder.DropIndex(
                name: "IX_HotelImagees_HotelsHotelID",
                table: "HotelImagees");

            migrationBuilder.DropIndex(
                name: "IX_HotelImagees_RoomsRoomID",
                table: "HotelImagees");

            migrationBuilder.DropColumn(
                name: "HotelOwnerId",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "StarRating",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "HotelsHotelID",
                table: "HotelImagees");

            migrationBuilder.DropColumn(
                name: "ImageURL",
                table: "HotelImagees");

            migrationBuilder.DropColumn(
                name: "RoomsRoomID",
                table: "HotelImagees");

            migrationBuilder.RenameTable(
                name: "HotelImagees",
                newName: "HotelImages");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "RoomID",
                table: "HotelImages",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<Guid>(
                name: "HotelID",
                table: "HotelImages",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "HotelImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelImages",
                table: "HotelImages",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_OwnerID",
                table: "Hotels",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_HotelID",
                table: "HotelImages",
                column: "HotelID");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_RoomID",
                table: "HotelImages",
                column: "RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImages_Hotels_HotelID",
                table: "HotelImages",
                column: "HotelID",
                principalTable: "Hotels",
                principalColumn: "HotelID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImages_Rooms_RoomID",
                table: "HotelImages",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "RoomID",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_AspNetUsers_OwnerID",
                table: "Hotels",
                column: "OwnerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_HotelImages_Hotels_HotelID",
                table: "HotelImages");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelImages_Rooms_RoomID",
                table: "HotelImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_AspNetUsers_OwnerID",
                table: "Hotels");

            migrationBuilder.DropIndex(
                name: "IX_Hotels_OwnerID",
                table: "Hotels");

            migrationBuilder.DropPrimaryKey(
                name: "PK_HotelImages",
                table: "HotelImages");

            migrationBuilder.DropIndex(
                name: "IX_HotelImages_HotelID",
                table: "HotelImages");

            migrationBuilder.DropIndex(
                name: "IX_HotelImages_RoomID",
                table: "HotelImages");

            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "HotelImages");

            migrationBuilder.RenameTable(
                name: "HotelImages",
                newName: "HotelImagees");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "HotelOwnerId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "StarRating",
                table: "Hotels",
                type: "decimal(3,1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoomID",
                table: "HotelImagees",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "HotelID",
                table: "HotelImagees",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "ImageID",
                table: "HotelImagees",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<Guid>(
                name: "HotelsHotelID",
                table: "HotelImagees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImageURL",
                table: "HotelImagees",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RoomsRoomID",
                table: "HotelImagees",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelImagees",
                table: "HotelImagees",
                column: "ImageID");

            migrationBuilder.CreateIndex(
                name: "IX_Hotels_HotelOwnerId",
                table: "Hotels",
                column: "HotelOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImagees_HotelsHotelID",
                table: "HotelImagees",
                column: "HotelsHotelID");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImagees_RoomsRoomID",
                table: "HotelImagees",
                column: "RoomsRoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImagees_Hotels_HotelsHotelID",
                table: "HotelImagees",
                column: "HotelsHotelID",
                principalTable: "Hotels",
                principalColumn: "HotelID");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImagees_Rooms_RoomsRoomID",
                table: "HotelImagees",
                column: "RoomsRoomID",
                principalTable: "Rooms",
                principalColumn: "RoomID");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_AspNetUsers_HotelOwnerId",
                table: "Hotels",
                column: "HotelOwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
