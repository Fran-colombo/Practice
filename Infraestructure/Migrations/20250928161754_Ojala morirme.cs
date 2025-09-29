using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infraestructure.Migrations
{
    /// <inheritdoc />
    public partial class Ojalamorirme : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "ToDos");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "Users",
                newName: "IsActive");

            migrationBuilder.RenameColumn(
                name: "isActive",
                table: "ToDos",
                newName: "IsActive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "Users",
                newName: "isActive");

            migrationBuilder.RenameColumn(
                name: "IsActive",
                table: "ToDos",
                newName: "isActive");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "ToDos",
                type: "TEXT",
                nullable: true);
        }
    }
}
