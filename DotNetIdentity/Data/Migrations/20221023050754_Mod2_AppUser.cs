using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DotNetIdentity.Data.Migrations
{
    public partial class Mod2_AppUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "AspNetUsers",
                type: "longblob",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                column: "ConcurrencyStamp",
                value: "5edf9fae-4c66-47df-b71d-654aa560cc6b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                column: "ConcurrencyStamp",
                value: "8cd4129e-f57b-4538-a186-f76bdde3414c");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                column: "ConcurrencyStamp",
                value: "c4670695-7cfa-45e1-9c53-270f6b28160b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                column: "ConcurrencyStamp",
                value: "fba12fb1-0fe7-4ccb-bb7d-e83afc1bc048");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0f2d516e-0fa6-4a6d-bd1f-aef06819203b", "AQAAAAEAACcQAAAAEBF0ITcM86qjKfTEC5aD4LROsUdkOENeDJc2wUjnqY+tT6BXNP60P3MnRLff+12jtQ==", "67399829-e02d-4b61-a76e-8fcf605e7b08" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "dffc6dd5-b145-41e9-a861-c87ff673e9ca",
                column: "ConcurrencyStamp",
                value: "9a39f005-16f0-42d4-bb0d-33d5250177a4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f8a527ac-d7f6-4d9d-aca6-46b2261b042b",
                column: "ConcurrencyStamp",
                value: "2eab52be-daa1-4a52-aef1-322c41a5cd6f");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "g7a527ac-d7t6-4d7z-aca6-45t2261b042b",
                column: "ConcurrencyStamp",
                value: "60b81ca7-84d3-403f-9fa2-4312d4dd8167");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "p9a527ac-d77w-4d3r-aca6-35b2261b042b",
                column: "ConcurrencyStamp",
                value: "e5e19bf7-09fe-4101-bb4a-0f9cdb206da4");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "6fbfb682-568c-4f5b-a298-85937ca4f7f3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "17455578-a93e-4338-8e2d-091a1932702d", "AQAAAAEAACcQAAAAEEbierr9P0D8F/KxxDwS8DXQXrH7gWHHz9WDdOjGCHpSffhlfiAS0/82FKReADlA0g==", "44c2e14c-f74d-4448-992d-cfc8f9b09cec" });
        }
    }
}
