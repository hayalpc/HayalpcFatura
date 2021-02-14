using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationTool.Migrations
{
    public partial class Mig1402 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_payments",
                table: "payments");

            migrationBuilder.RenameTable(
                name: "payments",
                newName: "invoice_payments");

            migrationBuilder.AddPrimaryKey(
                name: "pk_invoice_payments",
                table: "invoice_payments",
                column: "id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_invoice_payments",
                table: "invoice_payments");

            migrationBuilder.RenameTable(
                name: "invoice_payments",
                newName: "payments");

            migrationBuilder.AddPrimaryKey(
                name: "pk_payments",
                table: "payments",
                column: "id");
        }
    }
}
