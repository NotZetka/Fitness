using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class configurationUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseTemplate_FitnessPlanTemplates_FitnessPlanTemplateId",
                table: "ExerciseTemplate");

            migrationBuilder.DropForeignKey(
                name: "FK_Record_Exercises_ExerciseId",
                table: "Record");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Record",
                table: "Record");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseTemplate",
                table: "ExerciseTemplate");

            migrationBuilder.RenameTable(
                name: "Record",
                newName: "Records");

            migrationBuilder.RenameTable(
                name: "ExerciseTemplate",
                newName: "ExerciseTemplates");

            migrationBuilder.RenameIndex(
                name: "IX_Record_ExerciseId",
                table: "Records",
                newName: "IX_Records_ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseTemplate_FitnessPlanTemplateId",
                table: "ExerciseTemplates",
                newName: "IX_ExerciseTemplates_FitnessPlanTemplateId");

            migrationBuilder.AlterColumn<int>(
                name: "FitnessPlanTemplateId",
                table: "ExerciseTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FitnessPlanTemaplteId",
                table: "ExerciseTemplates",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Records",
                table: "Records",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseTemplates",
                table: "ExerciseTemplates",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseTemplates_FitnessPlanTemplates_FitnessPlanTemplateId",
                table: "ExerciseTemplates",
                column: "FitnessPlanTemplateId",
                principalTable: "FitnessPlanTemplates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Records_Exercises_ExerciseId",
                table: "Records",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExerciseTemplates_FitnessPlanTemplates_FitnessPlanTemplateId",
                table: "ExerciseTemplates");

            migrationBuilder.DropForeignKey(
                name: "FK_Records_Exercises_ExerciseId",
                table: "Records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Records",
                table: "Records");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ExerciseTemplates",
                table: "ExerciseTemplates");

            migrationBuilder.DropColumn(
                name: "FitnessPlanTemaplteId",
                table: "ExerciseTemplates");

            migrationBuilder.RenameTable(
                name: "Records",
                newName: "Record");

            migrationBuilder.RenameTable(
                name: "ExerciseTemplates",
                newName: "ExerciseTemplate");

            migrationBuilder.RenameIndex(
                name: "IX_Records_ExerciseId",
                table: "Record",
                newName: "IX_Record_ExerciseId");

            migrationBuilder.RenameIndex(
                name: "IX_ExerciseTemplates_FitnessPlanTemplateId",
                table: "ExerciseTemplate",
                newName: "IX_ExerciseTemplate_FitnessPlanTemplateId");

            migrationBuilder.AlterColumn<int>(
                name: "FitnessPlanTemplateId",
                table: "ExerciseTemplate",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Record",
                table: "Record",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ExerciseTemplate",
                table: "ExerciseTemplate",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ExerciseTemplate_FitnessPlanTemplates_FitnessPlanTemplateId",
                table: "ExerciseTemplate",
                column: "FitnessPlanTemplateId",
                principalTable: "FitnessPlanTemplates",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Record_Exercises_ExerciseId",
                table: "Record",
                column: "ExerciseId",
                principalTable: "Exercises",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
