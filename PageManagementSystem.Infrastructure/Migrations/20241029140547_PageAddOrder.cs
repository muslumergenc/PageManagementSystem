using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PageManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PageAddOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "Pages",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PageId1",
                table: "PageContents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageContents_PageId1",
                table: "PageContents",
                column: "PageId1");

            migrationBuilder.AddForeignKey(
                name: "FK_PageContents_Pages_PageId1",
                table: "PageContents",
                column: "PageId1",
                principalTable: "Pages",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageContents_Pages_PageId1",
                table: "PageContents");

            migrationBuilder.DropIndex(
                name: "IX_PageContents_PageId1",
                table: "PageContents");

            migrationBuilder.DropColumn(
                name: "Order",
                table: "Pages");

            migrationBuilder.DropColumn(
                name: "PageId1",
                table: "PageContents");
        }
    }
}
