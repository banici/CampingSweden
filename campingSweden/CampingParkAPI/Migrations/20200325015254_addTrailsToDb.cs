using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CampingParkAPI.Migrations
{
    public partial class addTrailsToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Trails",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Distance = table.Column<double>(nullable: false),
                    Difficulty = table.Column<int>(nullable: false),
                    CampingParkId = table.Column<int>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trails_CampingParks_CampingParkId",
                        column: x => x.CampingParkId,
                        principalTable: "CampingParks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trails_CampingParkId",
                table: "Trails",
                column: "CampingParkId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trails");
        }
    }
}
