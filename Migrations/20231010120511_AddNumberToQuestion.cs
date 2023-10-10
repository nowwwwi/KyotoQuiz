using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KyotoQuiz.Migrations
{
    /// <inheritdoc />
    public partial class AddNumberToQuestion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Number",
                table: "Question",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Number",
                table: "Question");
        }
    }
}
