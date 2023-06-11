using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace tfg.Migrations
{
    /// <inheritdoc />
    public partial class _7migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_book_state_StateId",
                table: "book");

            migrationBuilder.AddColumn<string>(
                name: "Direction",
                table: "user",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColorCode",
                table: "state",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "state",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ColorCode",
                table: "category",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "category",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "book",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_book_state_StateId",
                table: "book",
                column: "StateId",
                principalTable: "state",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_book_state_StateId",
                table: "book");

            migrationBuilder.DropColumn(
                name: "Direction",
                table: "user");

            migrationBuilder.DropColumn(
                name: "ColorCode",
                table: "state");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "state");

            migrationBuilder.DropColumn(
                name: "ColorCode",
                table: "category");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "category");

            migrationBuilder.AlterColumn<int>(
                name: "StateId",
                table: "book",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_book_state_StateId",
                table: "book",
                column: "StateId",
                principalTable: "state",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
