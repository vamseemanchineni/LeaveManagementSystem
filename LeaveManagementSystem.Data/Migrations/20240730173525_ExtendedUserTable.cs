using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExtendedUserTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateOnly>(
                name: "DateOfBirth",
                table: "AspNetUsers",
                type: "date",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3e4d0ff0-8da2-4255-945e-71883be3e60b",
                columns: new[] { "ConcurrencyStamp", "DateOfBirth", "FirstName", "LastName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a21fc2e-86dd-42df-b7e5-a1012fe6d9d7", new DateOnly(1950, 12, 1), "Default", "Admin", "AQAAAAIAAYagAAAAENagP6oIVAAyja83MxlnaiuAYNXCHSIE0WaX98hoSnmsAUA12DDuLSq+0regStnHMA==", "70de0d80-ee66-4d53-b1df-94f4491cef31" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3e4d0ff0-8da2-4255-945e-71883be3e60b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2f1f8b7c-acee-407e-83e8-bb1c2d1afb01", "AQAAAAIAAYagAAAAEEH9CEfX7F5LiUPm5qTDXiRwSuYwqoEa0NgIMCAzY26iVJ+k7GluEcqIf2DZhK4TXg==", "519f2f38-5f03-40a5-89ff-df4ea68a9087" });
        }
    }
}
