using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CourseProject.Migrations
{
    /// <inheritdoc />
    public partial class anEmptyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(
                sql: "CREATE FULLTEXT CATALOG ftCatalog AS DEFAULT;",
                suppressTransaction: true
            );

            migrationBuilder.CreateIndex(
                name: "FullTextIndex1",
                table: "Review",
                column: "Title");
            migrationBuilder.CreateIndex(
               name: "FullTextIndex2",
               table: "Comments",
               column: "Text");
            migrationBuilder.CreateIndex(
               name: "FullTextIndex3",
               table: "Work",
               column: "Title");
        }
        //CREATE FULLTEXT INDEX ON Reviews(Text) KEY INDEX PK_Reviews;
        
        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
