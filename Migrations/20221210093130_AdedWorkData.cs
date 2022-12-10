using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseProject.Migrations
{
    /// <inheritdoc />
    public partial class AdedWorkData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Works",
                columns: new[] { "Id", "AuthorRating", "CategoryId", "Title", "UserRating" },
                values: new object[] { 1, 0m, 1, "Cars", 0m });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Works",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
