using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PageManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class EntitesFixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageContents_PageData_PageDataId",
                table: "PageContents");

            migrationBuilder.DropForeignKey(
                name: "FK_PageContents_Pages_PageId",
                table: "PageContents");

            migrationBuilder.AlterColumn<int>(
                name: "PageDataId",
                table: "PageContents",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PageContents_PageData_PageDataId",
                table: "PageContents",
                column: "PageDataId",
                principalTable: "PageData",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PageContents_Pages_PageId",
                table: "PageContents",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PageContents_PageData_PageDataId",
                table: "PageContents");

            migrationBuilder.DropForeignKey(
                name: "FK_PageContents_Pages_PageId",
                table: "PageContents");

            migrationBuilder.AlterColumn<int>(
                name: "PageDataId",
                table: "PageContents",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_PageContents_PageData_PageDataId",
                table: "PageContents",
                column: "PageDataId",
                principalTable: "PageData",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PageContents_Pages_PageId",
                table: "PageContents",
                column: "PageId",
                principalTable: "Pages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
