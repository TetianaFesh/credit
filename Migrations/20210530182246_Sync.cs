using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Credit.Migrations
{
    public partial class Sync : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "type_of_loan",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type_of_loan_rate = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    percent = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_type_of_loan", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    first_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    last_name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    patronymic = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    email = table.Column<string>(type: "varchar(150)", unicode: false, maxLength: 150, nullable: false),
                    address = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    phone = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    probability_of_insolvency = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    type_of_user = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "accounts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    login = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    password = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_accounts", x => x.id);
                    table.ForeignKey(
                        name: "FK_accounts_users",
                        column: x => x.id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "debt_payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date = table.Column<DateTime>(type: "date", nullable: false),
                    payment_amount = table.Column<double>(type: "float", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    Paid = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_debt_payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_debt_payment_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "submitted_applications",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date_of_application_submission = table.Column<DateTime>(type: "date", nullable: false),
                    loan_size = table.Column<double>(type: "float", nullable: false),
                    status = table.Column<string>(type: "varchar(15)", unicode: false, maxLength: 15, nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: false),
                    type_of_loan_id = table.Column<int>(type: "int", nullable: false),
                    NumberOfPayments = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_submitted_applications", x => x.id);
                    table.ForeignKey(
                        name: "FK_submitted_applications_type_of_loan",
                        column: x => x.type_of_loan_id,
                        principalTable: "type_of_loan",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_submitted_applications_users",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "UQ_login",
                table: "accounts",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_debt_payment_user_id",
                table: "debt_payment",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ_Table_1",
                table: "debt_payment",
                column: "id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_submitted_applications_type_of_loan_id",
                table: "submitted_applications",
                column: "type_of_loan_id");

            migrationBuilder.CreateIndex(
                name: "IX_submitted_applications_user_id",
                table: "submitted_applications",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "UQ_client",
                table: "users",
                column: "id",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "accounts");

            migrationBuilder.DropTable(
                name: "debt_payment");

            migrationBuilder.DropTable(
                name: "submitted_applications");

            migrationBuilder.DropTable(
                name: "type_of_loan");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
