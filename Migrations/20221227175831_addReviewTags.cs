using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseProject.Migrations
{
    /// <inheritdoc />
    public partial class addReviewTags : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewTag_Reviews_ReviewId",
                table: "ReviewTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewTag_Tags_TagId",
                table: "ReviewTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewTag",
                table: "ReviewTag");

            migrationBuilder.RenameTable(
                name: "ReviewTag",
                newName: "ReviewTags");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewTag_TagId",
                table: "ReviewTags",
                newName: "IX_ReviewTags_TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewTags",
                table: "ReviewTags",
                columns: new[] { "ReviewId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewTags_Reviews_ReviewId",
                table: "ReviewTags",
                column: "ReviewId",
                principalTable: "Reviews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewTags_Tags_TagId",
                table: "ReviewTags",
                column: "TagId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReviewTags_Reviews_ReviewId",
                table: "ReviewTags");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewTags_Tags_TagId",
                table: "ReviewTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewTags",
                table: "ReviewTags");

            migrationBuilder.RenameTable(
                name: "ReviewTags",
                newName: "ReviewTag");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewTags_TagId",
                table: "ReviewTag",
                newName: "IX_ReviewTag_TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewTag",
                table: "ReviewTag",
                columns: new[] { "ReviewId", "TagId" });

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
    }
}
