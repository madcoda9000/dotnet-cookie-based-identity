using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetIdentity.Data.Migrations
{
    public partial class AppSettings_mod3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppSettings",
                columns: new[] { "Name", "Type", "Value" },
                values: new object[] { "SmtpFromAddress", "MailSettings", "YOUR_From_Address" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                column: "ConcurrencyStamp",
                value: "3925e099-a2d3-4d89-8bfe-7ba70f5cb040");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                column: "ConcurrencyStamp",
                value: "6d4a6a0a-f3c1-4f81-b00d-4382531940c7");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                column: "ConcurrencyStamp",
                value: "619d5df4-b945-445c-80e9-1374fd8efcb0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                column: "ConcurrencyStamp",
                value: "c59f82da-57a3-407c-824b-5e302ee2ed5d");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e289740f-25ea-4af7-8adf-ea5f2a685447", "AQAAAAEAACcQAAAAEOTyCG3wnT1huKQ5dB3Wn125QqDaNVsR/hbK9waxmO+d7FAy3fIcX9msY62WUgzaYQ==", "d8292556-e17a-4aed-8e8e-14d50546c4fc" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "SmtpFromAddress", "MailSettings" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                column: "ConcurrencyStamp",
                value: "ad46a111-7454-4b19-9878-0b8d84923a75");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                column: "ConcurrencyStamp",
                value: "daa71e4e-ff83-44b1-bb2e-dcbaa3b00265");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                column: "ConcurrencyStamp",
                value: "72e790a9-ac63-407c-9a32-b3d41af38a69");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                column: "ConcurrencyStamp",
                value: "6ce6fbc4-b166-4a04-89ef-e78130eea6ba");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f45b80a-1f8a-4474-ada7-fc686a93c3f2", "AQAAAAEAACcQAAAAEKYruXvdO8qnqS4MWSkp181H8BoSg6OiAW3N0jtTvkQml85Q78/rNaDEV3PIHhrHjg==", "6355838e-c29a-43f3-bc67-67d4476abba1" });
        }
    }
}
