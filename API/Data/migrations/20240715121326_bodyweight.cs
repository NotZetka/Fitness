using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.migrations
{
    /// <inheritdoc />
    public partial class bodyweight : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BodyWeights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Height = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyWeights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyWeights_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BodyWeightRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyWeightId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Neck = table.Column<int>(type: "int", nullable: true),
                    Chest = table.Column<int>(type: "int", nullable: true),
                    Arm = table.Column<int>(type: "int", nullable: true),
                    Forearm = table.Column<int>(type: "int", nullable: true),
                    Waist = table.Column<int>(type: "int", nullable: true),
                    Hip = table.Column<int>(type: "int", nullable: true),
                    Thigh = table.Column<int>(type: "int", nullable: true),
                    Calf = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BodyWeightRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BodyWeightRecords_BodyWeights_BodyWeightId",
                        column: x => x.BodyWeightId,
                        principalTable: "BodyWeights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BodyWeightRecords_BodyWeightId",
                table: "BodyWeightRecords",
                column: "BodyWeightId");

            migrationBuilder.CreateIndex(
                name: "IX_BodyWeights_UserId",
                table: "BodyWeights",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BodyWeightRecords");

            migrationBuilder.DropTable(
                name: "BodyWeights");
        }
    }
}
