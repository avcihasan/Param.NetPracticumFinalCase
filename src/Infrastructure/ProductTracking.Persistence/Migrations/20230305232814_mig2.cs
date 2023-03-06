using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductTracking.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "BasketTotalPrice",
                table: "Baskets",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "BasketItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BasketTotalPrice",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "BasketItems");
        }
    }
}
