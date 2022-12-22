using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountManagement.Migrations
{
    public partial class UpdateBank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyBank_BankAccounts_BankId",
                table: "CurrencyBank");

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "Code",
                table: "BankAccounts");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BankAccounts");

            migrationBuilder.AddColumn<int>(
                name: "BankId",
                table: "BankAccounts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Bank",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<int>(type: "int", nullable: false),
                    DateModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedBy = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bank", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "38f2da2e-b31c-4f0c-9059-aa01575890e6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "25566238-b0ed-40b5-86c4-e3e6f16b2579");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "fa868382-f2b1-4838-8449-74e17bd21c16");

            migrationBuilder.InsertData(
                table: "Bank",
                columns: new[] { "Id", "Code", "CreatedBy", "DateCreated", "DateModified", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, "Credins", 0, new DateTime(2022, 12, 11, 18, 45, 59, 732, DateTimeKind.Local).AddTicks(3867), null, null, "Credins Bank" },
                    { 2, "BKT", 0, new DateTime(2022, 12, 11, 18, 45, 59, 732, DateTimeKind.Local).AddTicks(3881), null, null, "BKT Bank" }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 11, 18, 45, 59, 731, DateTimeKind.Local).AddTicks(3278));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 12, 11, 18, 45, 59, 731, DateTimeKind.Local).AddTicks(3538));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 11, 18, 45, 59, 732, DateTimeKind.Local).AddTicks(1422));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 12, 11, 18, 45, 59, 732, DateTimeKind.Local).AddTicks(1441));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 11, 18, 45, 59, 731, DateTimeKind.Local).AddTicks(8072));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 12, 11, 18, 45, 59, 731, DateTimeKind.Local).AddTicks(8285));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 12, 11, 18, 45, 59, 731, DateTimeKind.Local).AddTicks(8292));

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyBank_Bank_BankId",
                table: "CurrencyBank",
                column: "BankId",
                principalTable: "Bank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CurrencyBank_Bank_BankId",
                table: "CurrencyBank");

            migrationBuilder.DropTable(
                name: "Bank");

            migrationBuilder.DropColumn(
                name: "BankId",
                table: "BankAccounts");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "BankAccounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BankAccounts",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "44a74869-47cf-4571-a9c7-e023acb1ad37");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "e2e4ae19-9c2f-4785-b632-4a8a086ef83d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "5b1c695b-fae0-4b0d-872c-5f9f8f4ea525");

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "Balance", "ClientId", "Code", "CreatedBy", "DateCreated", "DateModified", "IsActive", "ModifiedBy", "Name" },
                values: new object[,]
                {
                    { 1, 10000m, 1, "Credins", 0, new DateTime(2022, 12, 11, 16, 27, 0, 341, DateTimeKind.Local).AddTicks(3107), null, true, null, "Credins Bank" },
                    { 2, 10000m, 1, "BKT", 0, new DateTime(2022, 12, 11, 16, 27, 0, 341, DateTimeKind.Local).AddTicks(3123), null, true, null, "BKT Bank" }
                });

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 11, 16, 27, 0, 340, DateTimeKind.Local).AddTicks(2871));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 12, 11, 16, 27, 0, 340, DateTimeKind.Local).AddTicks(3126));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 11, 16, 27, 0, 340, DateTimeKind.Local).AddTicks(9956));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 12, 11, 16, 27, 0, 340, DateTimeKind.Local).AddTicks(9972));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 11, 16, 27, 0, 340, DateTimeKind.Local).AddTicks(6993));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 12, 11, 16, 27, 0, 340, DateTimeKind.Local).AddTicks(7180));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 12, 11, 16, 27, 0, 340, DateTimeKind.Local).AddTicks(7185));

            migrationBuilder.AddForeignKey(
                name: "FK_CurrencyBank_BankAccounts_BankId",
                table: "CurrencyBank",
                column: "BankId",
                principalTable: "BankAccounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
