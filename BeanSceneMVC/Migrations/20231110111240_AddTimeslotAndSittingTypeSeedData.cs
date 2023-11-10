using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeanSceneMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeslotAndSittingTypeSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "SittingTypes",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Breakfast" },
                    { 2, "Lunch" },
                    { 3, "Dinner" }
                });

            migrationBuilder.InsertData(
                table: "Timeslots",
                column: "Time",
                values: new object[]
                {
                    new DateTime(2000, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 8, 30, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 9, 30, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 11, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 11, 30, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 12, 30, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 13, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 13, 30, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 14, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 14, 30, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 15, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 15, 30, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 16, 30, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 17, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 17, 30, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 18, 30, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 19, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 19, 30, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 20, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 20, 30, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 21, 0, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 21, 30, 0, 0, DateTimeKind.Unspecified),
                    new DateTime(2000, 1, 1, 22, 0, 0, 0, DateTimeKind.Unspecified)
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SittingTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SittingTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "SittingTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 8, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 8, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 9, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 9, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 10, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 10, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 11, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 11, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 12, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 13, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 13, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 14, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 14, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 15, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 15, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 16, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 16, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 17, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 17, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 18, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 18, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 19, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 19, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 20, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 20, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 21, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 21, 30, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.DeleteData(
                table: "Timeslots",
                keyColumn: "Time",
                keyValue: new DateTime(2000, 1, 1, 22, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
