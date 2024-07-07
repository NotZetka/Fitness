using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class addedPropertyToExerciseTemplate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Sets",
                table: "ExerciseTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sets",
                table: "ExerciseTemplates");
        }
    }
}
