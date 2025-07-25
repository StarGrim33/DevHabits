﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevHabit.Api.Database.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Microsoft.EntityFrameworkCore.Migrations.Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "dev_habit");

        migrationBuilder.CreateTable(
            name: "habits",
            schema: "dev_habit",
            columns: table => new
            {
                id = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                type = table.Column<int>(type: "integer", nullable: false),
                frequency_type = table.Column<int>(type: "integer", nullable: false),
                frequency_times_per_period = table.Column<int>(type: "integer", nullable: false),
                target_value = table.Column<int>(type: "integer", nullable: false),
                target_unit = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                status = table.Column<int>(type: "integer", nullable: false),
                is_archived = table.Column<bool>(type: "boolean", nullable: false),
                end_date = table.Column<DateOnly>(type: "date", nullable: true),
                milestone_target = table.Column<int>(type: "integer", nullable: true),
                milestone_current = table.Column<int>(type: "integer", nullable: true),
                created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                updated_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                last_completed_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_habits", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "Tags",
            schema: "dev_habit",
            columns: table => new
            {
                id = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                description = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                updated_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_tags", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "habit_tags",
            schema: "dev_habit",
            columns: table => new
            {
                habit_id = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                tag_id = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                tag_id1 = table.Column<string>(type: "character varying(500)", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("pk_habit_tags", x => new { x.habit_id, x.tag_id });
                table.ForeignKey(
                    name: "fk_habit_tags_habits_habit_id",
                    column: x => x.habit_id,
                    principalSchema: "dev_habit",
                    principalTable: "habits",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_habit_tags_tags_tag_id",
                    column: x => x.tag_id,
                    principalSchema: "dev_habit",
                    principalTable: "Tags",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "fk_habit_tags_tags_tag_id1",
                    column: x => x.tag_id1,
                    principalSchema: "dev_habit",
                    principalTable: "Tags",
                    principalColumn: "id");
            });

        migrationBuilder.CreateIndex(
            name: "ix_habit_tags_tag_id",
            schema: "dev_habit",
            table: "habit_tags",
            column: "tag_id");

        migrationBuilder.CreateIndex(
            name: "ix_habit_tags_tag_id1",
            schema: "dev_habit",
            table: "habit_tags",
            column: "tag_id1");

        migrationBuilder.CreateIndex(
            name: "ix_tags_name",
            schema: "dev_habit",
            table: "Tags",
            column: "name",
            unique: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "habit_tags",
            schema: "dev_habit");

        migrationBuilder.DropTable(
            name: "habits",
            schema: "dev_habit");

        migrationBuilder.DropTable(
            name: "Tags",
            schema: "dev_habit");
    }
}
