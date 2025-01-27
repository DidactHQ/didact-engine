using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DidactEngine.Migrations
{
    /// <inheritdoc />
    public partial class smallchange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockRun_State",
                table: "BlockRun");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowRun_Organization",
                table: "FlowRun");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowRun_State",
                table: "FlowRun");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowRun_TriggerType",
                table: "FlowRun");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowSchedule_Organization_OrganizationId",
                table: "FlowSchedule");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowVersion_Organization_OrganizationId",
                table: "FlowVersion");

            migrationBuilder.DropIndex(
                name: "IX_FlowVersion_OrganizationId",
                table: "FlowVersion");

            migrationBuilder.DropIndex(
                name: "IX_FlowSchedule_OrganizationId",
                table: "FlowSchedule");

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
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
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
                name: "FK_BlockRun_State_StateId",
                table: "BlockRun",
                column: "StateId",
                principalTable: "State",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Cascade);

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

            migrationBuilder.AddForeignKey(
                name: "FK_FlowRun_Organization_OrganizationId",
                table: "FlowRun",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "OrganizationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlowRun_State_StateId",
                table: "FlowRun",
                column: "StateId",
                principalTable: "State",
                principalColumn: "StateId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlowRun_TriggerType_TriggerTypeId",
                table: "FlowRun",
                column: "TriggerTypeId",
                principalTable: "TriggerType",
                principalColumn: "TriggerTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BlockRun_State_StateId",
                table: "BlockRun");

            migrationBuilder.DropForeignKey(
                name: "FK_Flow_ExecutionMode_ExecutionModeId",
                table: "Flow");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowRun_ExecutionMode_ExecutionModeId",
                table: "FlowRun");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowRun_Organization_OrganizationId",
                table: "FlowRun");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowRun_State_StateId",
                table: "FlowRun");

            migrationBuilder.DropForeignKey(
                name: "FK_FlowRun_TriggerType_TriggerTypeId",
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

            migrationBuilder.CreateIndex(
                name: "IX_FlowVersion_OrganizationId",
                table: "FlowVersion",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowSchedule_OrganizationId",
                table: "FlowSchedule",
                column: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_BlockRun_State",
                table: "BlockRun",
                column: "StateId",
                principalTable: "State",
                principalColumn: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowRun_Organization",
                table: "FlowRun",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "OrganizationId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowRun_State",
                table: "FlowRun",
                column: "StateId",
                principalTable: "State",
                principalColumn: "StateId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowRun_TriggerType",
                table: "FlowRun",
                column: "TriggerTypeId",
                principalTable: "TriggerType",
                principalColumn: "TriggerTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_FlowSchedule_Organization_OrganizationId",
                table: "FlowSchedule",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "OrganizationId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FlowVersion_Organization_OrganizationId",
                table: "FlowVersion",
                column: "OrganizationId",
                principalTable: "Organization",
                principalColumn: "OrganizationId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
