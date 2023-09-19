using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBankAPI.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AccountHolders",
                columns: new[] { "Id", "DateOfBirth", "EmailAddress", "FirstName", "IdNumber", "LastName", "MobileNumber", "ResidentialAddress" },
                values: new object[] { 1, new DateTime(1990, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "john@example.com", "John", "123456789", "Doe", "123-456-7890", "123 Main St, City" });

            migrationBuilder.InsertData(
                table: "AccountHolders",
                columns: new[] { "Id", "DateOfBirth", "EmailAddress", "FirstName", "IdNumber", "LastName", "MobileNumber", "ResidentialAddress" },
                values: new object[] { 2, new DateTime(1985, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "jane@example.com", "Jane", "987654321", "Smith", "987-654-3210", "456 Elm St, Town" });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "AccountHolderId", "AccountNumber", "AccountType", "AvailableBalance", "LastModified", "Name", "Status" },
                values: new object[] { 1, 1, "1234567890", "Cheque", 100000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Doe Cheque Account", "Active" });

            migrationBuilder.InsertData(
                table: "BankAccounts",
                columns: new[] { "Id", "AccountHolderId", "AccountNumber", "AccountType", "AvailableBalance", "LastModified", "Name", "Status" },
                values: new object[] { 2, 2, "9876543210", "Savings", 250000m, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane Smith Savings Account", "Active" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "BankAccounts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "AccountHolders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "AccountHolders",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
