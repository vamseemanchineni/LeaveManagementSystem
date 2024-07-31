using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LeaveManagementSystem.Web.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedingDefaultRoleandUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "641f6ede-4466-4b4e-8f01-c1a28602da86", null, "Supervisor", "SUPERVISOR" },
                    { "7252c346-8257-47fe-b91a-ecf35b3f7303", null, "Administrator", "ADMINISTRATOR" },
                    { "cb78847a-e1c8-421d-aebe-e69caa254b46", null, "Employee", "EMPLOYEE" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3e4d0ff0-8da2-4255-945e-71883be3e60b", 0, "2f1f8b7c-acee-407e-83e8-bb1c2d1afb01", "admin@localhost.com", true, false, null, "ADMIN@LOCALHOST.COM", "ADMIN@LOCALHOST.COM", "AQAAAAIAAYagAAAAEEH9CEfX7F5LiUPm5qTDXiRwSuYwqoEa0NgIMCAzY26iVJ+k7GluEcqIf2DZhK4TXg==", null, false, "519f2f38-5f03-40a5-89ff-df4ea68a9087", false, "admin@localhost.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "7252c346-8257-47fe-b91a-ecf35b3f7303", "3e4d0ff0-8da2-4255-945e-71883be3e60b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "641f6ede-4466-4b4e-8f01-c1a28602da86");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb78847a-e1c8-421d-aebe-e69caa254b46");

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "7252c346-8257-47fe-b91a-ecf35b3f7303", "3e4d0ff0-8da2-4255-945e-71883be3e60b" });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7252c346-8257-47fe-b91a-ecf35b3f7303");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3e4d0ff0-8da2-4255-945e-71883be3e60b");
        }
    }
}
