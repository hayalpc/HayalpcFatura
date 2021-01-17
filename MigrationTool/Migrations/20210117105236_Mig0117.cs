using Microsoft.EntityFrameworkCore.Migrations;

namespace MigrationTool.Migrations
{
    public partial class Mig0117 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "merchant_id",
                schema: "tracking",
                table: "user_logs");

            migrationBuilder.DropColumn(
                name: "merchant_id",
                schema: "tracking",
                table: "user_bulletins");

            migrationBuilder.DropColumn(
                name: "merchant_id",
                schema: "tracking",
                table: "table_histories");

            migrationBuilder.DropColumn(
                name: "merchant_id",
                schema: "panel",
                table: "users");

            migrationBuilder.AddColumn<long>(
                name: "dealer_id",
                schema: "tracking",
                table: "user_logs",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "dealer_id",
                schema: "tracking",
                table: "user_bulletins",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "dealer_id",
                schema: "tracking",
                table: "table_histories",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "dealer_id",
                schema: "panel",
                table: "users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "dealer_id",
                schema: "tracking",
                table: "user_logs");

            migrationBuilder.DropColumn(
                name: "dealer_id",
                schema: "tracking",
                table: "user_bulletins");

            migrationBuilder.DropColumn(
                name: "dealer_id",
                schema: "tracking",
                table: "table_histories");

            migrationBuilder.DropColumn(
                name: "dealer_id",
                schema: "panel",
                table: "users");

            migrationBuilder.AddColumn<long>(
                name: "merchant_id",
                schema: "tracking",
                table: "user_logs",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "merchant_id",
                schema: "tracking",
                table: "user_bulletins",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "merchant_id",
                schema: "tracking",
                table: "table_histories",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "merchant_id",
                schema: "panel",
                table: "users",
                type: "bigint",
                nullable: true);
        }
    }
}
