using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AssessmentSystem.Service.Data.Migrations
{
    /// <inheritdoc />
    public partial class thirdmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "CandidateScores");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "CandidateScores",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
