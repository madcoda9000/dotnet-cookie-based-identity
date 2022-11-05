using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
#pragma warning disable 1591

namespace DotNetIdentity.Data.Migrations
{
    public partial class SessionCache_Mod2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AbsoluteExpiration",
                table: "AppSessionCache",
                type: "datetime(6)",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                column: "ConcurrencyStamp",
                value: "fcca32ee-4cee-445f-8be0-92375f7a12d1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                column: "ConcurrencyStamp",
                value: "6594291f-d352-4269-855b-8abc0ab18e01");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                column: "ConcurrencyStamp",
                value: "99b2b129-74d0-40ec-8892-e679d560c74f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                column: "ConcurrencyStamp",
                value: "07f2c15c-a1a3-4840-87a6-645446d15f0f");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0bd8f427-6b8a-444c-b34e-ba42537137da", "AQAAAAEAACcQAAAAEImvoKdoqh4RAms6fLIxbS8vshGY7MoW37zCdI/DIKK/br9dE33l7dugU7bxT4YiTQ==", "9188049f-056e-45f0-bb3b-706ce2102631" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "AbsoluteExpiration",
                table: "AppSessionCache",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime(6)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                column: "ConcurrencyStamp",
                value: "a79fea86-2a4c-4b00-8bdf-be7ba146f432");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                column: "ConcurrencyStamp",
                value: "4457bd66-a98a-45a3-b5ac-113432c10318");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                column: "ConcurrencyStamp",
                value: "15f61f78-eb42-4bf9-a25d-ab98776500df");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                column: "ConcurrencyStamp",
                value: "5557772c-a953-4663-9823-775045c328b3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8bd09316-769d-4e3c-b8e7-546d4c10328d", "AQAAAAEAACcQAAAAEMhv/Srsl/U0ePBs8x0NZL82Ff4FsfGJ1gYbBRimD/vFDDC7hVLyH2tU80l+0ySgxg==", "4a76ef23-70b7-43bd-adf9-2f9280056169" });
        }
    }
}
#pragma warning restore 1591 