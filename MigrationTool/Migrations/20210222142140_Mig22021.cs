using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationTool.Migrations
{
    public partial class Mig22021 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "value1",
                table: "invoice_payments",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "value2",
                table: "invoice_payments",
                maxLength: 128,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "value3",
                table: "invoice_payments",
                maxLength: 128,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "value1",
                table: "invoice_payments");

            migrationBuilder.DropColumn(
                name: "value2",
                table: "invoice_payments");

            migrationBuilder.DropColumn(
                name: "value3",
                table: "invoice_payments");
        }
    }
}
