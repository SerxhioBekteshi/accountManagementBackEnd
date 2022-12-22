using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AccountManagement.Migrations
{
    public partial class updateColumnCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ExhangeRate",
                table: "Currencies",
                newName: "ExchangeRate");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "CurrencyBank",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "CurrencyBank",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "CurrencyBank",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ModifiedBy",
                table: "CurrencyBank",
                type: "int",
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

            migrationBuilder.UpdateData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 11, 16, 27, 0, 341, DateTimeKind.Local).AddTicks(3107));

            migrationBuilder.UpdateData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 12, 11, 16, 27, 0, 341, DateTimeKind.Local).AddTicks(3123));

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "CurrencyBank");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "CurrencyBank");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "CurrencyBank");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "CurrencyBank");

            migrationBuilder.RenameColumn(
                name: "ExchangeRate",
                table: "Currencies",
                newName: "ExhangeRate");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "cd034978-2508-4120-8d0e-daf4b0cc7503");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 2,
                column: "ConcurrencyStamp",
                value: "f98753c5-cdb4-4fb5-874b-140f4583ec13");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 3,
                column: "ConcurrencyStamp",
                value: "b74743b2-ae42-44d6-98be-f45503cdcbf7");

            migrationBuilder.UpdateData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 9, 0, 4, 32, 168, DateTimeKind.Local).AddTicks(6063));

            migrationBuilder.UpdateData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 12, 9, 0, 4, 32, 168, DateTimeKind.Local).AddTicks(6082));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 9, 0, 4, 32, 167, DateTimeKind.Local).AddTicks(2510));

            migrationBuilder.UpdateData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 12, 9, 0, 4, 32, 167, DateTimeKind.Local).AddTicks(2797));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 9, 0, 4, 32, 168, DateTimeKind.Local).AddTicks(1958));

            migrationBuilder.UpdateData(
                table: "Currencies",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 12, 9, 0, 4, 32, 168, DateTimeKind.Local).AddTicks(1985));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 1,
                column: "DateCreated",
                value: new DateTime(2022, 12, 9, 0, 4, 32, 167, DateTimeKind.Local).AddTicks(7240));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 2,
                column: "DateCreated",
                value: new DateTime(2022, 12, 9, 0, 4, 32, 167, DateTimeKind.Local).AddTicks(7820));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: 3,
                column: "DateCreated",
                value: new DateTime(2022, 12, 9, 0, 4, 32, 167, DateTimeKind.Local).AddTicks(7849));
        }
    }
}
