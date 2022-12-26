using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseProject.Migrations
{
    /// <inheritdoc />
    public partial class addedRating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserRating",
                table: "Works",
                newName: "OverallUserRating");

            migrationBuilder.RenameColumn(
                name: "AuthorRating",
                table: "Works",
                newName: "OverallAuthorRating");

            migrationBuilder.CreateTable(
                name: "UserRatings",
                columns: table => new
                {
                    Rating = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WorkId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRatings");

            migrationBuilder.RenameColumn(
                name: "OverallUserRating",
                table: "Works",
                newName: "UserRating");

            migrationBuilder.RenameColumn(
                name: "OverallAuthorRating",
                table: "Works",
                newName: "AuthorRating");
        }
    }
}
