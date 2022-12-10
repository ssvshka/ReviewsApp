using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseProject.Migrations
{
    /// <inheritdoc />
    public partial class changedReviewSubjectName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_ReviewSubjects_ReviewSubjectId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "ReviewSubjectId",
                table: "Reviews",
                newName: "WorkId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_ReviewSubjectId",
                table: "Reviews",
                newName: "IX_Reviews_WorkId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_ReviewSubjects_WorkId",
                table: "Reviews",
                column: "WorkId",
                principalTable: "ReviewSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_ReviewSubjects_WorkId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Reviews");

            migrationBuilder.RenameColumn(
                name: "WorkId",
                table: "Reviews",
                newName: "ReviewSubjectId");

            migrationBuilder.RenameIndex(
                name: "IX_Reviews_WorkId",
                table: "Reviews",
                newName: "IX_Reviews_ReviewSubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_ReviewSubjects_ReviewSubjectId",
                table: "Reviews",
                column: "ReviewSubjectId",
                principalTable: "ReviewSubjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
