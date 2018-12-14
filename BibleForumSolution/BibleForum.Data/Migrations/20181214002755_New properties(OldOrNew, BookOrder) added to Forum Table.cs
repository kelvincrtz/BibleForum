﻿using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BibleForum.Data.Migrations
{
    public partial class NewpropertiesOldOrNewBookOrderaddedtoForumTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookOrder",
                table: "Forums",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "OldOrNew",
                table: "Forums",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BookOrder",
                table: "Forums");

            migrationBuilder.DropColumn(
                name: "OldOrNew",
                table: "Forums");
        }
    }
}
