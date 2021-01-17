using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace MigrationTool.Migrations
{
    public partial class FirstMig1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "parameter");

            migrationBuilder.EnsureSchema(
                name: "panel");

            migrationBuilder.EnsureSchema(
                name: "tracking");

            migrationBuilder.CreateTable(
                name: "blob_files",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    update_user_id = table.Column<long>(nullable: true),
                    data_id = table.Column<long>(nullable: false),
                    type = table.Column<int>(nullable: false),
                    token = table.Column<Guid>(nullable: false),
                    name = table.Column<string>(maxLength: 256, nullable: false),
                    file_name = table.Column<string>(maxLength: 256, nullable: false),
                    blob_url = table.Column<string>(maxLength: 256, nullable: false),
                    account_name = table.Column<string>(maxLength: 256, nullable: false),
                    account_key = table.Column<string>(maxLength: 256, nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_blob_files", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dealers",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    update_user_id = table.Column<long>(nullable: true),
                    code = table.Column<long>(nullable: false),
                    logo = table.Column<string>(maxLength: 256, nullable: true),
                    name = table.Column<string>(maxLength: 64, nullable: false),
                    description = table.Column<string>(maxLength: 256, nullable: false),
                    priorty = table.Column<int>(nullable: false),
                    @default = table.Column<bool>(name: "default", nullable: false),
                    channel = table.Column<string>(maxLength: 64, nullable: true),
                    person_name = table.Column<string>(maxLength: 64, nullable: true),
                    person_surname = table.Column<string>(maxLength: 64, nullable: true),
                    person_phone = table.Column<string>(maxLength: 64, nullable: true),
                    person_email = table.Column<string>(maxLength: 64, nullable: true),
                    person_address = table.Column<string>(maxLength: 64, nullable: true),
                    person_tck_no = table.Column<string>(maxLength: 64, nullable: true),
                    person_ıban = table.Column<string>(maxLength: 64, nullable: true),
                    person_account_name = table.Column<string>(maxLength: 64, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_dealers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "institutions",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    update_user_id = table.Column<long>(nullable: true),
                    code = table.Column<string>(maxLength: 64, nullable: false),
                    name = table.Column<string>(maxLength: 64, nullable: false),
                    description = table.Column<string>(maxLength: 256, nullable: false),
                    category_ıd = table.Column<long>(nullable: false),
                    type = table.Column<string>(maxLength: 256, nullable: true),
                    placeholder = table.Column<string>(maxLength: 256, nullable: true),
                    calidation_text = table.Column<string>(maxLength: 256, nullable: true),
                    reverse = table.Column<bool>(nullable: false),
                    logo = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_institutions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "invoices",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    update_user_id = table.Column<long>(nullable: true),
                    token = table.Column<Guid>(maxLength: 64, nullable: false),
                    dealer_id = table.Column<long>(nullable: false),
                    payment_id = table.Column<long>(nullable: false),
                    category_id = table.Column<long>(nullable: false),
                    category_name = table.Column<long>(maxLength: 128, nullable: false),
                    institution_id = table.Column<long>(nullable: false),
                    institution_name = table.Column<string>(maxLength: 128, nullable: false),
                    subscriber_no = table.Column<string>(maxLength: 64, nullable: false),
                    invoice_no = table.Column<string>(maxLength: 64, nullable: false),
                    invoice_date = table.Column<string>(maxLength: 64, nullable: false),
                    invoice_owner = table.Column<string>(maxLength: 64, nullable: false),
                    amount = table.Column<decimal>(nullable: false),
                    delay_amount = table.Column<decimal>(nullable: false),
                    fee = table.Column<decimal>(nullable: false),
                    total_amount = table.Column<decimal>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    institution_trans_id = table.Column<string>(maxLength: 128, nullable: true),
                    user_ip = table.Column<string>(maxLength: 64, nullable: false),
                    channel = table.Column<string>(maxLength: 16, nullable: false),
                    error = table.Column<string>(maxLength: 64, nullable: true),
                    err_desc = table.Column<string>(maxLength: 256, nullable: true),
                    value1 = table.Column<string>(maxLength: 128, nullable: true),
                    value2 = table.Column<string>(maxLength: 128, nullable: true),
                    value3 = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_invoices", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "payments",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    update_user_id = table.Column<long>(nullable: true),
                    token = table.Column<Guid>(maxLength: 64, nullable: false),
                    dealer_id = table.Column<long>(nullable: false),
                    dealer_name = table.Column<long>(maxLength: 128, nullable: false),
                    category_id = table.Column<long>(nullable: false),
                    category_name = table.Column<long>(maxLength: 128, nullable: false),
                    institution_id = table.Column<long>(nullable: false),
                    institution_name = table.Column<string>(maxLength: 128, nullable: false),
                    subscriber_no = table.Column<string>(maxLength: 64, nullable: false),
                    payment_method = table.Column<string>(maxLength: 64, nullable: false),
                    payment_date = table.Column<DateTime>(nullable: true),
                    expire_date = table.Column<DateTime>(nullable: false),
                    payment_channel = table.Column<string>(maxLength: 64, nullable: false),
                    masked_data = table.Column<string>(maxLength: 128, nullable: true),
                    masked_data2 = table.Column<string>(maxLength: 128, nullable: true),
                    masked_data3 = table.Column<string>(maxLength: 128, nullable: true),
                    institution_trans_id = table.Column<string>(maxLength: 128, nullable: true),
                    amount = table.Column<decimal>(nullable: false),
                    delay_amount = table.Column<decimal>(nullable: false),
                    fee = table.Column<decimal>(nullable: false),
                    user_ip = table.Column<string>(maxLength: 64, nullable: false),
                    error = table.Column<string>(maxLength: 64, nullable: true),
                    err_desc = table.Column<string>(maxLength: 256, nullable: true),
                    remote_order = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_payments", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "reset_passwords",
                schema: "panel",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    update_user_id = table.Column<long>(nullable: true),
                    token = table.Column<Guid>(nullable: false),
                    user_id = table.Column<long>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_reset_passwords", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "role_permissions",
                schema: "panel",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    update_user_id = table.Column<long>(nullable: true),
                    role_id = table.Column<long>(nullable: false),
                    name = table.Column<string>(nullable: false),
                    icon = table.Column<string>(nullable: true),
                    role_permission_id = table.Column<long>(nullable: false),
                    order = table.Column<int>(nullable: false),
                    controller = table.Column<string>(nullable: true),
                    action = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true),
                    is_menu = table.Column<bool>(nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_role_permissions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "panel",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    update_user_id = table.Column<long>(nullable: true),
                    type = table.Column<int>(nullable: false),
                    name = table.Column<string>(maxLength: 64, nullable: false),
                    description = table.Column<string>(maxLength: 64, nullable: false),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "panel",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    update_user_id = table.Column<long>(nullable: true),
                    type = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    merchant_id = table.Column<long>(nullable: true),
                    name = table.Column<string>(maxLength: 64, nullable: false),
                    surname = table.Column<string>(maxLength: 64, nullable: false),
                    image_path = table.Column<string>(nullable: true),
                    email = table.Column<string>(maxLength: 64, nullable: false),
                    title = table.Column<string>(maxLength: 64, nullable: false),
                    password = table.Column<string>(maxLength: 128, nullable: true),
                    phone = table.Column<string>(maxLength: 64, nullable: false),
                    last_session_id = table.Column<string>(maxLength: 128, nullable: true),
                    last_login_date = table.Column<DateTime>(nullable: true),
                    last_password_change_date = table.Column<DateTime>(nullable: true),
                    wrong_login_try_count = table.Column<int>(nullable: false),
                    last_wrong_login_try_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "categories",
                schema: "parameter",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    update_user_id = table.Column<long>(nullable: true),
                    code = table.Column<string>(maxLength: 64, nullable: false),
                    logo = table.Column<string>(maxLength: 256, nullable: true),
                    name = table.Column<string>(maxLength: 64, nullable: false),
                    description = table.Column<string>(maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "city",
                schema: "parameter",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    update_user_id = table.Column<long>(nullable: true),
                    name = table.Column<string>(maxLength: 128, nullable: false),
                    description = table.Column<string>(maxLength: 128, nullable: true),
                    country_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_city", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "country",
                schema: "parameter",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    update_user_id = table.Column<long>(nullable: true),
                    name = table.Column<string>(maxLength: 128, nullable: false),
                    description = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "district",
                schema: "parameter",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    update_user_id = table.Column<long>(nullable: true),
                    city_id = table.Column<long>(nullable: false),
                    name = table.Column<string>(maxLength: 256, nullable: false),
                    description = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_district", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "table_definitions",
                schema: "tracking",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    update_user_id = table.Column<long>(nullable: true),
                    action_type = table.Column<int>(nullable: false),
                    assembly = table.Column<string>(maxLength: 256, nullable: false),
                    @namespace = table.Column<string>(name: "namespace", maxLength: 256, nullable: false),
                    model_name = table.Column<string>(maxLength: 256, nullable: false),
                    role_id1 = table.Column<long>(nullable: true),
                    role_id2 = table.Column<long>(nullable: true),
                    description = table.Column<string>(maxLength: 128, nullable: true),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_table_definitions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "table_histories",
                schema: "tracking",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    update_user_id = table.Column<long>(nullable: true),
                    merchant_id = table.Column<long>(nullable: true),
                    table_definition_id = table.Column<long>(nullable: false),
                    action_type = table.Column<int>(nullable: false),
                    action_detail = table.Column<string>(maxLength: 64, nullable: false),
                    note = table.Column<string>(maxLength: 256, nullable: true),
                    model_name = table.Column<string>(maxLength: 64, nullable: false),
                    data_id = table.Column<long>(nullable: true),
                    old_data = table.Column<string>(nullable: true),
                    new_data = table.Column<string>(nullable: true),
                    role_id1 = table.Column<long>(nullable: true),
                    role_id2 = table.Column<long>(nullable: true),
                    status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_table_histories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_bulletins",
                schema: "tracking",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    update_user_id = table.Column<long>(nullable: true),
                    merchant_id = table.Column<long>(nullable: true),
                    role_id = table.Column<long>(nullable: true),
                    user_id = table.Column<long>(nullable: false),
                    action_type = table.Column<string>(maxLength: 64, nullable: false),
                    title = table.Column<string>(maxLength: 128, nullable: false),
                    message = table.Column<string>(maxLength: 1024, nullable: false),
                    language = table.Column<string>(maxLength: 8, nullable: true),
                    type = table.Column<int>(nullable: false),
                    status = table.Column<int>(nullable: false),
                    start_date = table.Column<DateTime>(nullable: false),
                    expire_date = table.Column<DateTime>(nullable: false),
                    read_date = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_bulletins", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_logs",
                schema: "tracking",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    update_user_id = table.Column<long>(nullable: true),
                    merchant_id = table.Column<long>(nullable: true),
                    user_id = table.Column<long>(nullable: false),
                    action_type = table.Column<string>(maxLength: 64, nullable: false),
                    note = table.Column<string>(maxLength: 512, nullable: true),
                    user_ip = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_logs", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                schema: "panel",
                columns: table => new
                {
                    id = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    create_time = table.Column<DateTime>(nullable: false),
                    create_user_id = table.Column<long>(nullable: false),
                    update_time = table.Column<DateTime>(nullable: true),
                    update_user_id = table.Column<long>(nullable: true),
                    user_id = table.Column<long>(nullable: false),
                    role_id = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user_roles", x => x.id);
                    table.ForeignKey(
                        name: "fk_user_roles_roles_role_id",
                        column: x => x.role_id,
                        principalSchema: "panel",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ıx_user_roles_role_id",
                schema: "panel",
                table: "user_roles",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "blob_files");

            migrationBuilder.DropTable(
                name: "dealers");

            migrationBuilder.DropTable(
                name: "institutions");

            migrationBuilder.DropTable(
                name: "invoices");

            migrationBuilder.DropTable(
                name: "payments");

            migrationBuilder.DropTable(
                name: "reset_passwords",
                schema: "panel");

            migrationBuilder.DropTable(
                name: "role_permissions",
                schema: "panel");

            migrationBuilder.DropTable(
                name: "user_roles",
                schema: "panel");

            migrationBuilder.DropTable(
                name: "users",
                schema: "panel");

            migrationBuilder.DropTable(
                name: "categories",
                schema: "parameter");

            migrationBuilder.DropTable(
                name: "city",
                schema: "parameter");

            migrationBuilder.DropTable(
                name: "country",
                schema: "parameter");

            migrationBuilder.DropTable(
                name: "district",
                schema: "parameter");

            migrationBuilder.DropTable(
                name: "table_definitions",
                schema: "tracking");

            migrationBuilder.DropTable(
                name: "table_histories",
                schema: "tracking");

            migrationBuilder.DropTable(
                name: "user_bulletins",
                schema: "tracking");

            migrationBuilder.DropTable(
                name: "user_logs",
                schema: "tracking");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "panel");
        }
    }
}
