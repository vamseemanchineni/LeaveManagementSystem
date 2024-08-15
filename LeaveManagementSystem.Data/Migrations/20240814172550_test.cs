using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LeaveManagementSystem.Data.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3e4d0ff0-8da2-4255-945e-71883be3e60b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d93d6714-27d1-4c00-b7e6-beab7bc1d264", "AQAAAAIAAYagAAAAEGOYnaVT8u8rmTri6uHudMIK3jq8homj31A6KKhz05dfA/muiSx91oI5eJLfsZQrLg==", "2db52927-10e6-4467-b9f7-0524900e0394" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3e4d0ff0-8da2-4255-945e-71883be3e60b",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "051e3f5c-8542-481a-980d-aec981e1cc36", "AQAAAAIAAYagAAAAEPjGcDSWScXPf9HMfkPjClFq7Sp/w2Bx5REhjwTjqMBaY7e67HQJK4pZOciyOtgadw==", "1a480547-daa6-477c-9032-400d11472da4" });
        }
    }
}
