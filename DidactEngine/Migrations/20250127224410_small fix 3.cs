using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DidactEngine.Migrations
{
    /// <inheritdoc />
    public partial class smallfix3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Flow_ExecutionMode_ExecutionModeId",
                table: "Flow");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowRun_ExecutionMode_ExecutionModeId",
                table: "FlowRun");

            migrationBuilder.DropTable(
                name: "ExecutionMode");

            migrationBuilder.DropIndex(
                name: "IX_FlowRun_ExecutionModeId",
                table: "FlowRun");

            migrationBuilder.DropIndex(
                name: "IX_Flow_ExecutionModeId",
                table: "Flow");

            migrationBuilder.DropColumn(
                name: "ExecutionModeId",
                table: "FlowRun");

            migrationBuilder.DropColumn(
                name: "ExecutionModeId",
                table: "Flow");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ExecutionModeId",
                table: "FlowRun",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ExecutionModeId",
                table: "Flow",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ExecutionMode",
                columns: table => new
                {
                    ExecutionModeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExecutionMode", x => x.ExecutionModeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FlowRun_ExecutionModeId",
                table: "FlowRun",
                column: "ExecutionModeId");

            migrationBuilder.CreateIndex(
                name: "IX_Flow_ExecutionModeId",
                table: "Flow",
                column: "ExecutionModeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Flow_ExecutionMode_ExecutionModeId",
                table: "Flow",
                column: "ExecutionModeId",
                principalTable: "ExecutionMode",
                principalColumn: "ExecutionModeId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlowRun_ExecutionMode_ExecutionModeId",
                table: "FlowRun",
                column: "ExecutionModeId",
                principalTable: "ExecutionMode",
                principalColumn: "ExecutionModeId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
