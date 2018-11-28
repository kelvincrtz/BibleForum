using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BibleForum.Data.Migrations
{
    public partial class AddedanewcelltoPostReplySupportingBibleVerseBibleBook : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "BibleBook",
                table: "PostReplySupportingBibleVerses",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BibleBook",
                table: "PostReplySupportingBibleVerses");
        }
    }
}
