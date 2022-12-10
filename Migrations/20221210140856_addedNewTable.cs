using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CourseProject.Migrations
{
    /// <inheritdoc />
    public partial class addedNewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewTag_Reviews_ReviewsId",
                table: "ReviewTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewTag_Tags_TagsId",
                table: "ReviewTag");

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tags",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Works",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.RenameColumn(
                name: "TagsId",
                table: "ReviewTag",
                newName: "TagId");

            migrationBuilder.RenameColumn(
                name: "ReviewsId",
                table: "ReviewTag",
                newName: "ReviewId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewTag_TagsId",
                table: "ReviewTag",
                newName: "IX_ReviewTag_TagId");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Reviews",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewTag_Reviews_ReviewId",
                table: "ReviewTag",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewTag_Tags_TagId",
                table: "ReviewTag",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewTag_Reviews_ReviewId",
                table: "ReviewTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewTag_Tags_TagId",
                table: "ReviewTag");

            migrationBuilder.RenameColumn(
                name: "TagId",
                table: "ReviewTag",
                newName: "TagsId");

            migrationBuilder.RenameColumn(
                name: "ReviewId",
                table: "ReviewTag",
                newName: "ReviewsId");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewTag_TagId",
                table: "ReviewTag",
                newName: "IX_ReviewTag_TagsId");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Reviews",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Album" },
                    { 2, "Book" },
                    { 3, "Movie" },
                    { 4, "Game" }
                });

            migrationBuilder.InsertData(
                table: "Tags",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "Great Plot" },
                    { 2, "Upbeat" },
                    { 3, "Fantastic" },
                    { 4, "Short" }
                });

            migrationBuilder.InsertData(
                table: "Works",
                columns: new[] { "Id", "AuthorRating", "CategoryId", "Title", "UserRating" },
                values: new object[] { 1, 0m, 1, "Cars", 0m });

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewTag_Reviews_ReviewsId",
                table: "ReviewTag",
                column: "ReviewsId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewTag_Tags_TagsId",
                table: "ReviewTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
