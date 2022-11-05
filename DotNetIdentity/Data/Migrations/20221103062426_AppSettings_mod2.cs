using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable 1591

namespace DotNetIdentity.Data.Migrations
{
    public partial class AppSettings_mod2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppSettings",
                columns: new[] { "Name", "Type", "Value" },
                values: new object[,]
                {
                    { "ApplicationName", "GlobalSettings", "SecPass" },
                    { "Password", "MailSettings", "YOUR_SmtpPassword" },
                    { "SessionCookieExpiration", "GlobalSettings", "7" },
                    { "SessionTimeoutRedirAfter", "GlobalSettings", "70000" },
                    { "SessionTimeoutWarnAfter", "GlobalSettings", "50000" },
                    { "SmtpPort", "MailSettings", "587" },
                    { "SmtpServer", "MailSettings", "YOUR_SmtpServer" },
                    { "SmtpUseTls", "MailSettings", "true" },
                    { "Username", "MailSettings", "YOUR_Smtp_Username" }
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "ApplicationName", "GlobalSettings" });

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "Password", "MailSettings" });

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "SessionCookieExpiration", "GlobalSettings" });

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "SessionTimeoutRedirAfter", "GlobalSettings" });

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "SessionTimeoutWarnAfter", "GlobalSettings" });

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "SmtpPort", "MailSettings" });

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "SmtpServer", "MailSettings" });

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "SmtpUseTls", "MailSettings" });

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "Username", "MailSettings" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                column: "ConcurrencyStamp",
                value: "edeacada-64f7-4a2a-8ff2-6156c1e21ffb");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                column: "ConcurrencyStamp",
                value: "0376911f-cf92-4a94-a511-88509ae014b9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                column: "ConcurrencyStamp",
                value: "8b78a8c8-9e3a-48eb-8bc3-41221d88345a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                column: "ConcurrencyStamp",
                value: "d2ffa92c-45ee-4eba-9d5b-04c70db4a064");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b9305552-a16b-4e0f-bd57-50179eac7e90", "AQAAAAEAACcQAAAAEITUNpw9ouKMVuPUS4+gjpoDbhZ7Br5nutQCR2huiH/UUqT9usjOPsYSulO3mqdiYQ==", "f476bcfc-75b3-4b24-acc7-cfb1557faa47" });
        }
    }
}
#pragma warning restore 1591 