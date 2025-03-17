using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alloca8._2.Migrations
{
    /// <inheritdoc />
    public partial class AddOwnerIDtoUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "OwnerID",
                table: "AspNetUsers",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OwnerID",
                table: "AspNetUsers");
        }
    }
}
