using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationTool.Migrations
{
    public partial class Mig1801 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "calidation_text",
                table: "institutions");

            migrationBuilder.AddColumn<string>(
                name: "validation_text",
                table: "institutions",
                maxLength: 256,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "validation_text",
                table: "institutions");

            migrationBuilder.AddColumn<string>(
                name: "calidation_text",
                table: "institutions",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
