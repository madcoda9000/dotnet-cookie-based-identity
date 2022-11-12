using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable 1591

namespace DotNetIdentity.Data.Migrations
{
    public partial class BrandSettings1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AppSettings",
                columns: new[] { "Name", "Type", "Value" },
                values: new object[,]
                {
                    { "ColorDanger", "BrandSettings", "#f5023c" },
                    { "ColorInfo", "BrandSettings", "#2196f3" },
                    { "ColorLightBackground", "BrandSettings", "#f2f7ff" },
                    { "ColorPrimary", "BrandSettings", "#090251" },
                    { "ColorSuccess", "BrandSettings", "#00c853" },
                    { "ColorWarning", "BrandSettings", "#ff9800" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                column: "ConcurrencyStamp",
                value: "42cb0a7a-ecc1-47c2-8705-ed00ddaade24");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                column: "ConcurrencyStamp",
                value: "f0cf4b21-bee3-4121-b357-158111c12da9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                column: "ConcurrencyStamp",
                value: "4acdb6cb-7320-45b4-aae0-d2f0b214ab9a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                column: "ConcurrencyStamp",
                value: "45263e06-dd6b-48f4-ba84-8da371d1ea6c");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RolesCombined", "SecurityStamp" },
                values: new object[] { "a3ddbcc9-e8c1-4927-9314-da10ecf870b7", "AQAAAAEAACcQAAAAEIBS8bW+YssXHJWLSIxxw9YTyONlYnyeHDtev9n1ocpBxVZprHlvyPK+wn69DMw46Q==", "Admin", "fb78471a-7778-4bfa-b732-ff653a18042f" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "ColorDanger", "BrandSettings" });

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "ColorInfo", "BrandSettings" });

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "ColorLightBackground", "BrandSettings" });

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "ColorPrimary", "BrandSettings" });

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "ColorSuccess", "BrandSettings" });

            migrationBuilder.DeleteData(
                table: "AppSettings",
                keyColumns: new[] { "Name", "Type" },
                keyValues: new object[] { "ColorWarning", "BrandSettings" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                column: "ConcurrencyStamp",
                value: "09476ed9-844a-439a-8dac-1f3053e3f5d6");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                column: "ConcurrencyStamp",
                value: "6889ff6f-7a3c-4d21-af7d-4e8bc8c168e3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                column: "ConcurrencyStamp",
                value: "edef1e0a-accb-4d53-834f-c13d1dcacb01");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                column: "ConcurrencyStamp",
                value: "c280cbda-c71f-44dd-8576-c85a4c466081");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RolesCombined", "SecurityStamp" },
                values: new object[] { "32bc6aad-7099-456c-ba02-527c179acbff", "AQAAAAEAACcQAAAAEIsYBzJYOq6uyW4lCVgZzX8Mg/JV2SMsd+5mf8uzT1xMh54t/hqIHkRJH/II3+f2Cg==", "", "9ca58bdf-c737-4726-b950-953e2fb7f739" });
        }
    }
    #pragma warning restore 1591
}
