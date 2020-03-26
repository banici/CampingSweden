using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CampingParkAPI.Migrations
{
    public partial class addImgToCampingPark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Picture",
                table: "CampingParks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Picture",
                table: "CampingParks");
        }
    }
}
