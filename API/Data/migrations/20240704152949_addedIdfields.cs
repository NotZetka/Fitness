using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class addedIdfields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Exercises",
                table: "FitnessPlanTemplates");

            migrationBuilder.AddColumn<int>(
                name: "TemplateId",
                table: "FitnessPlans",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExerciseTemplate",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FitnessPlanTemplateId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExerciseTemplate", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExerciseTemplate_FitnessPlanTemplates_FitnessPlanTemplateId",
                        column: x => x.FitnessPlanTemplateId,
                        principalTable: "FitnessPlanTemplates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExerciseTemplate_FitnessPlanTemplateId",
                table: "ExerciseTemplate",
                column: "FitnessPlanTemplateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExerciseTemplate");

            migrationBuilder.DropColumn(
                name: "TemplateId",
                table: "FitnessPlans");

            migrationBuilder.AddColumn<string>(
                name: "Exercises",
                table: "FitnessPlanTemplates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
