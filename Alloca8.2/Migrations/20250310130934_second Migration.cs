using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Alloca8._2.Migrations
{
    /// <inheritdoc />
    public partial class secondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Reviews",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "StarRating",
                table: "Hotels",
                type: "decimal(3,1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Rating",
                table: "Reviews",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decima(3, 1)");

            migrationBuilder.AlterColumn<decimal>(
                name: "StarRating",
                table: "Hotels",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3,1)");
        }
    }
}
