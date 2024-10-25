using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PageManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PageContentData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "PageData");

            migrationBuilder.RenameColumn(
                name: "Content",
                table: "PageContents",
                newName: "ContentValue");

            migrationBuilder.AddColumn<int>(
                name: "PageDataId",
                table: "PageContents",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PageContents_PageDataId",
                table: "PageContents",
                column: "PageDataId");

            migrationBuilder.AddForeignKey(
                name: "FK_PageContents_PageData_PageDataId",
                table: "PageContents",
                column: "PageDataId",
                principalTable: "PageData",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageContents_PageData_PageDataId",
                table: "PageContents");

            migrationBuilder.DropIndex(
                name: "IX_PageContents_PageDataId",
                table: "PageContents");

            migrationBuilder.DropColumn(
                name: "PageDataId",
                table: "PageContents");

            migrationBuilder.RenameColumn(
                name: "ContentValue",
                table: "PageContents",
                newName: "Content");

            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "PageData",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
