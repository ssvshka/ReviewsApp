using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseProject.Migrations
{
    /// <inheritdoc />
    public partial class ChangedRatingProps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_ReviewSubjects_WorkId",
                table: "Reviews");


            migrationBuilder.DropForeignKey(
                name: "FK_ReviewSubjects_Categories_CategoryId",
                table: "ReviewSubjects");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewSubjects",
                table: "ReviewSubjects");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "Users");

            migrationBuilder.RenameTable(
                name: "ReviewSubjects",
                newName: "Works");

            migrationBuilder.RenameColumn(
                name: "Rating",
                table: "Works",
                newName: "UserRating");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewSubjects_CategoryId",
                table: "Works",
                newName: "IX_Works_CategoryId");

            migrationBuilder.AddColumn<decimal>(
                name: "AuthorRating",
                table: "Works",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Works",
                table: "Works",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Works_WorkId",
                table: "Reviews",
                column: "WorkId",
                principalTable: "Works",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Works_Categories_CategoryId",
                table: "Works",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Works_WorkId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Works_Categories_CategoryId",
                table: "Works");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Works",
                table: "Works");

            migrationBuilder.DropColumn(
                name: "AuthorRating",
                table: "Works");

            migrationBuilder.RenameTable(
                name: "Works",
                newName: "ReviewSubjects");

            migrationBuilder.RenameColumn(
                name: "UserRating",
                table: "ReviewSubjects",
                newName: "Rating");

            migrationBuilder.RenameIndex(
                name: "IX_Works_CategoryId",
                table: "ReviewSubjects",
                newName: "IX_ReviewSubjects_CategoryId");

            migrationBuilder.AddColumn<decimal>(
                name: "Rating",
                table: "Users",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewSubjects",
                table: "ReviewSubjects",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_ReviewSubjects_WorkId",
                table: "Reviews",
                column: "WorkId",
                principalTable: "ReviewSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewSubjects_Categories_CategoryId",
                table: "ReviewSubjects",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
