using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alloca8._2.Migrations
{
    public partial class FixHotelImages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop existing foreign keys
            migrationBuilder.DropForeignKey(
                name: "FK_HotelImagees_Hotels_HotelsHotelID",
                table: "HotelImagees");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelImagees_Rooms_RoomsRoomID",
                table: "HotelImagees");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_AspNetUsers_HotelOwnerId",
                table: "Hotels");

            // Drop existing indexes
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

            // Drop unwanted columns
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

            // Rename table
            migrationBuilder.RenameTable(
                name: "HotelImagees",
                newName: "HotelImages");

            // Alter Hotels.Name column
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            // Drop and recreate RoomID column
            migrationBuilder.DropColumn(
                name: "RoomID",
                table: "HotelImages");

            migrationBuilder.AddColumn<Guid>(
                name: "RoomID",
                table: "HotelImages",
                type: "uniqueidentifier",
                nullable: true);

            // Drop and recreate HotelID column
            migrationBuilder.Sql(@"
        IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_HotelImages_Hotels_HotelID]', N'F') AND parent_object_id = OBJECT_ID(N'[HotelImages]', N'U'))
        BEGIN
            ALTER TABLE [HotelImages] DROP CONSTRAINT [FK_HotelImages_Hotels_HotelID];
        END
    ");

            migrationBuilder.Sql(@"
        IF EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_HotelImages_Hotels_HotelID]', N'F') AND parent_object_id = OBJECT_ID(N'[HotelImages]', N'U'))
        BEGIN
            ALTER TABLE [HotelImages] DROP CONSTRAINT [FK_HotelImages_Hotels_HotelID];
        END
    ");

            migrationBuilder.Sql(@"
        IF EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[HotelImages]', N'U') AND name = N'IX_HotelImages_HotelID')
        BEGIN
            DROP INDEX [IX_HotelImages_HotelID] ON [HotelImages];
        END
    ");

            migrationBuilder.DropColumn(
                name: "HotelID",
                table: "HotelImages");

            migrationBuilder.AddColumn<Guid>(
                name: "HotelID",
                table: "HotelImages",
                type: "uniqueidentifier",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_HotelID",
                table: "HotelImages",
                column: "HotelID");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImages_Hotels_HotelID",
                table: "HotelImages",
                column: "HotelID",
                principalTable: "Hotels",
                principalColumn: "HotelID",
                onDelete: ReferentialAction.Cascade);

            // Add ImagePath column
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "HotelImages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            // Add primary key
            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelImages",
                table: "HotelImages",
                column: "ImageID");

            // Recreate indexes
            migrationBuilder.CreateIndex(
                name: "IX_Hotels_OwnerID",
                table: "Hotels",
                column: "OwnerID");

            migrationBuilder.CreateIndex(
                name: "IX_HotelImages_RoomID",
                table: "HotelImages",
                column: "RoomID");

            // Recreate foreign keys
            migrationBuilder.Sql(@"
        IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_HotelImages_Hotels_HotelID]', N'F') AND parent_object_id = OBJECT_ID(N'[HotelImages]', N'U'))
        BEGIN
            ALTER TABLE [HotelImages] ADD CONSTRAINT [FK_HotelImages_Hotels_HotelID] FOREIGN KEY ([HotelID]) REFERENCES [Hotels] ([HotelID]) ON DELETE CASCADE;
        END
    ");

            migrationBuilder.AddForeignKey(
                name: "FK_HotelImages_Rooms_RoomID",
                table: "HotelImages",
                column: "RoomID",
                principalTable: "Rooms",
                principalColumn: "RoomID",
                onDelete: ReferentialAction.Restrict); // Or ON DELETE NO ACTION

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_AspNetUsers_OwnerID",
                table: "Hotels",
                column: "OwnerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

    
        }


        // Reverse the Up() method's changes.
        // (Your existing Down() method should be fine, but make sure it reverses the changes correctly)
        // ... (Your Down() method code) ...
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Revert foreign key changes
            migrationBuilder.DropForeignKey(
                name: "FK_HotelImages_Hotels_HotelID",
                table: "HotelImages");

            migrationBuilder.DropForeignKey(
                name: "FK_HotelImages_Rooms_RoomID",
                table: "HotelImages");

            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_AspNetUsers_OwnerID",
                table: "Hotels");

            // Revert index changes
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

            // Revert column additions
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "HotelImages");

            // Revert RoomID column to int
            migrationBuilder.DropColumn(
                name: "RoomID",
                table: "HotelImages");

            migrationBuilder.AddColumn<int>(
                name: "RoomID",
                table: "HotelImagees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            // Revert HotelID column to int
            migrationBuilder.AlterColumn<int>(
                name: "HotelID",
                table: "HotelImagees",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            // Revert table name
            migrationBuilder.RenameTable(
                name: "HotelImages",
                newName: "HotelImagees");

            // Revert Hotels.Name column
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Hotels",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            // Revert unwanted column removals
            migrationBuilder.AddColumn<Guid>(
                name: "HotelOwnerId",
                table: "Hotels",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "StarRating",
                table: "Hotels",
                type: "decimal(3, 1)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

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

            // Revert PK
            migrationBuilder.AddPrimaryKey(
                name: "PK_HotelImagees",
                table: "HotelImagees",
                column: "ImageID");

            // Revert indexes
            migrationBuilder.CreateIndex(
                name: "PK_HotelImagees",
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

            // Revert FKs
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
