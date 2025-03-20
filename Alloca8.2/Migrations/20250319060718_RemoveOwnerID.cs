using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alloca8._2.Migrations
{
    /// <inheritdoc />
    public partial class RemoveOwnerID : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_AspNetUsers_OwnerID",
                table: "Hotels");

            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "OwnerID",
                table: "Hotels",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Hotels_OwnerID",
                table: "Hotels",
                newName: "IX_Hotels_UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_AspNetUsers_UserId",
                table: "Hotels",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Hotels_AspNetUsers_UserId",
                table: "Hotels");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Hotels",
                newName: "OwnerID");

            migrationBuilder.RenameIndex(
                name: "IX_Hotels_UserId",
                table: "Hotels",
                newName: "IX_Hotels_OwnerID");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerID",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Hotels_AspNetUsers_OwnerID",
                table: "Hotels",
                column: "OwnerID",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
