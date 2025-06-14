﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevHabit.Api.Migrations.Application;

/// <inheritdoc />
public partial class Add_HabitTags : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Tags",
            schema: "dev_habit",
            columns: table => new
            {
                id = table.Column<string>(type: "text", nullable: false),
                name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
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
                habit_id = table.Column<string>(type: "character varying(500)", nullable: false),
                tag_id = table.Column<string>(type: "text", nullable: false),
                created_at_utc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
            });

        migrationBuilder.CreateIndex(
            name: "ix_habit_tags_tag_id",
            schema: "dev_habit",
            table: "habit_tags",
            column: "tag_id");

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
            name: "Tags",
            schema: "dev_habit");
    }
}
