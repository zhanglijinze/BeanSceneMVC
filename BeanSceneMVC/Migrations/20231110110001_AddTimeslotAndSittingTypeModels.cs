using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeanSceneMVC.Migrations
{
    /// <inheritdoc />
    public partial class AddTimeslotAndSittingTypeModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SittingTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SittingTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Timeslots",
                columns: table => new
                {
                    Time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timeslots", x => x.Time);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SittingTypes_Name",
                table: "SittingTypes",
                column: "Name",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SittingTypes");

            migrationBuilder.DropTable(
                name: "Timeslots");
        }
    }
}
