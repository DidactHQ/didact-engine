using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DidactEngine.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Organization",
                columns: table => new
                {
                    OrganizationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organization", x => x.OrganizationId);
                });

            migrationBuilder.CreateTable(
                name: "ScheduleType",
                columns: table => new
                {
                    ScheduleTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScheduleType", x => x.ScheduleTypeId);
                });

            migrationBuilder.CreateTable(
                name: "State",
                columns: table => new
                {
                    StateId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_State", x => x.StateId);
                });

            migrationBuilder.CreateTable(
                name: "TriggerType",
                columns: table => new
                {
                    TriggerTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TriggerType", x => x.TriggerTypeId);
                });

            migrationBuilder.CreateTable(
                name: "Engine",
                columns: table => new
                {
                    EngineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    UniqueName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Engine", x => x.EngineId);
                    table.ForeignKey(
                        name: "FK_Engine_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId");
                });

            migrationBuilder.CreateTable(
                name: "FifoQueue",
                columns: table => new
                {
                    FifoQueueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FifoQueue", x => x.FifoQueueId);
                    table.ForeignKey(
                        name: "FK_FifoQueue_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Flow",
                columns: table => new
                {
                    FlowId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssemblyName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConcurrencyLimit = table.Column<int>(type: "int", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flow", x => x.FlowId);
                    table.ForeignKey(
                        name: "FK_Flow_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId");
                });

            migrationBuilder.CreateTable(
                name: "HyperQueue",
                columns: table => new
                {
                    HyperQueueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HyperQueue", x => x.HyperQueueId);
                    table.ForeignKey(
                        name: "FK_HyperQueue_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlowSchedule",
                columns: table => new
                {
                    FlowScheduleId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    FlowId = table.Column<long>(type: "bigint", nullable: false),
                    ScheduleTypeId = table.Column<int>(type: "int", nullable: false),
                    CronExpression = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastRunTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    NextRunTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowSchedule", x => x.FlowScheduleId);
                    table.ForeignKey(
                        name: "FK_FlowSchedule_Flow_FlowId",
                        column: x => x.FlowId,
                        principalTable: "Flow",
                        principalColumn: "FlowId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowSchedule_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowSchedule_ScheduleType_ScheduleTypeId",
                        column: x => x.ScheduleTypeId,
                        principalTable: "ScheduleType",
                        principalColumn: "ScheduleTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlowVersion",
                columns: table => new
                {
                    FlowVersionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowId = table.Column<long>(type: "bigint", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    AssemblyVersion = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Version = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false),
                    RowVersion = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowVersion", x => x.FlowVersionId);
                    table.ForeignKey(
                        name: "FK_FlowVersion_Flow_FlowId",
                        column: x => x.FlowId,
                        principalTable: "Flow",
                        principalColumn: "FlowId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowVersion_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FlowRun",
                columns: table => new
                {
                    FlowRunId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowId = table.Column<long>(type: "bigint", nullable: false),
                    FlowVersionId = table.Column<int>(type: "int", nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    ParentFlowRunId = table.Column<long>(type: "bigint", nullable: true),
                    TriggerTypeId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JsonPayload = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecuteAfter = table.Column<DateTime>(type: "datetime2", nullable: true),
                    TimeoutSeconds = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    StateLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StateLastUpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    ExecutionStarted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExecutionEnded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlowRun", x => x.FlowRunId);
                    table.ForeignKey(
                        name: "FK_FlowRun_Flow",
                        column: x => x.FlowId,
                        principalTable: "Flow",
                        principalColumn: "FlowId");
                    table.ForeignKey(
                        name: "FK_FlowRun_FlowVersion_FlowVersionId",
                        column: x => x.FlowVersionId,
                        principalTable: "FlowVersion",
                        principalColumn: "FlowVersionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FlowRun_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId");
                    table.ForeignKey(
                        name: "FK_FlowRun_State",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "StateId");
                    table.ForeignKey(
                        name: "FK_FlowRun_TriggerType",
                        column: x => x.TriggerTypeId,
                        principalTable: "TriggerType",
                        principalColumn: "TriggerTypeId");
                });

            migrationBuilder.CreateTable(
                name: "BlockRun",
                columns: table => new
                {
                    BlockRunId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FlowRunId = table.Column<long>(type: "bigint", nullable: false),
                    BlockName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ExecutionStarted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ExecutionEnded = table.Column<DateTime>(type: "datetime2", nullable: true),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    StateLastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StateLastUpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    Active = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    OrganizationId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlockRun", x => x.BlockRunId);
                    table.ForeignKey(
                        name: "FK_BlockRun_FlowRun",
                        column: x => x.FlowRunId,
                        principalTable: "FlowRun",
                        principalColumn: "FlowRunId");
                    table.ForeignKey(
                        name: "FK_BlockRun_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId");
                    table.ForeignKey(
                        name: "FK_BlockRun_State",
                        column: x => x.StateId,
                        principalTable: "State",
                        principalColumn: "StateId");
                });

            migrationBuilder.CreateTable(
                name: "FifoQueueInbound",
                columns: table => new
                {
                    FifoQueueInboundId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    FifoQueueId = table.Column<int>(type: "int", nullable: false),
                    FlowRunId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FifoQueueInbound", x => x.FifoQueueInboundId);
                    table.ForeignKey(
                        name: "FK_FifoQueueInbound_FifoQueue",
                        column: x => x.FifoQueueId,
                        principalTable: "FifoQueue",
                        principalColumn: "FifoQueueId");
                    table.ForeignKey(
                        name: "FK_FifoQueueInbound_FlowRun",
                        column: x => x.FlowRunId,
                        principalTable: "FlowRun",
                        principalColumn: "FlowRunId");
                    table.ForeignKey(
                        name: "FK_FifoQueueInbound_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId");
                });

            migrationBuilder.CreateTable(
                name: "HyperQueueInbound",
                columns: table => new
                {
                    HyperQueueInboundId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OrganizationId = table.Column<int>(type: "int", nullable: false),
                    HyperQueueId = table.Column<int>(type: "int", nullable: false),
                    FlowRunId = table.Column<long>(type: "bigint", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    LastUpdated = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastUpdatedBy = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    RowVersion = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HyperQueueInbound", x => x.HyperQueueInboundId);
                    table.ForeignKey(
                        name: "FK_HyperQueueInbound_$HyperQueue",
                        column: x => x.HyperQueueId,
                        principalTable: "HyperQueue",
                        principalColumn: "HyperQueueId");
                    table.ForeignKey(
                        name: "FK_HyperQueueInbound_FlowRun",
                        column: x => x.FlowRunId,
                        principalTable: "FlowRun",
                        principalColumn: "FlowRunId");
                    table.ForeignKey(
                        name: "FK_HyperQueueInbound_Organization",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "OrganizationId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BlockRun_FlowRunId",
                table: "BlockRun",
                column: "FlowRunId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockRun_OrganizationId",
                table: "BlockRun",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_BlockRun_StateId",
                table: "BlockRun",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_Engine_OrganizationId",
                table: "Engine",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Engine_UniqueName",
                table: "Engine",
                column: "UniqueName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FifoQueue_OrganizationId",
                table: "FifoQueue",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FifoQueueInbound_FifoQueueId",
                table: "FifoQueueInbound",
                column: "FifoQueueId");

            migrationBuilder.CreateIndex(
                name: "IX_FifoQueueInbound_FlowRunId",
                table: "FifoQueueInbound",
                column: "FlowRunId");

            migrationBuilder.CreateIndex(
                name: "IX_FifoQueueInbound_OrganizationId",
                table: "FifoQueueInbound",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_Flow_Name",
                table: "Flow",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Flow_OrganizationId",
                table: "Flow",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowRun_FlowId",
                table: "FlowRun",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowRun_FlowVersionId",
                table: "FlowRun",
                column: "FlowVersionId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowRun_OrganizationId",
                table: "FlowRun",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowRun_StateId",
                table: "FlowRun",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowRun_TriggerTypeId",
                table: "FlowRun",
                column: "TriggerTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowSchedule_FlowId",
                table: "FlowSchedule",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowSchedule_OrganizationId",
                table: "FlowSchedule",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowSchedule_ScheduleTypeId",
                table: "FlowSchedule",
                column: "ScheduleTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowVersion_FlowId",
                table: "FlowVersion",
                column: "FlowId");

            migrationBuilder.CreateIndex(
                name: "IX_FlowVersion_OrganizationId",
                table: "FlowVersion",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_HyperQueue_OrganizationId",
                table: "HyperQueue",
                column: "OrganizationId");

            migrationBuilder.CreateIndex(
                name: "IX_HyperQueueInbound_FlowRunId",
                table: "HyperQueueInbound",
                column: "FlowRunId");

            migrationBuilder.CreateIndex(
                name: "IX_HyperQueueInbound_HyperQueueId",
                table: "HyperQueueInbound",
                column: "HyperQueueId");

            migrationBuilder.CreateIndex(
                name: "IX_HyperQueueInbound_OrganizationId",
                table: "HyperQueueInbound",
                column: "OrganizationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BlockRun");

            migrationBuilder.DropTable(
                name: "Engine");

            migrationBuilder.DropTable(
                name: "FifoQueueInbound");

            migrationBuilder.DropTable(
                name: "FlowSchedule");

            migrationBuilder.DropTable(
                name: "HyperQueueInbound");

            migrationBuilder.DropTable(
                name: "FifoQueue");

            migrationBuilder.DropTable(
                name: "ScheduleType");

            migrationBuilder.DropTable(
                name: "HyperQueue");

            migrationBuilder.DropTable(
                name: "FlowRun");

            migrationBuilder.DropTable(
                name: "FlowVersion");

            migrationBuilder.DropTable(
                name: "State");

            migrationBuilder.DropTable(
                name: "TriggerType");

            migrationBuilder.DropTable(
                name: "Flow");

            migrationBuilder.DropTable(
                name: "Organization");
        }
    }
}
