using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductTracking.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProductListId",
                table: "Products",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ProductLists",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppUserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CratedDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductLists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductLists_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductListId",
                table: "Products",
                column: "ProductListId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductLists_AppUserId",
                table: "ProductLists",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_ProductLists_ProductListId",
                table: "Products",
                column: "ProductListId",
                principalTable: "ProductLists",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_ProductLists_ProductListId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "ProductLists");

            migrationBuilder.DropIndex(
                name: "IX_Products_ProductListId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ProductListId",
                table: "Products");
        }
    }
}
