using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BibleForum.Data.Migrations
{
    public partial class NewpropertiesinsidePostReplyLikeTableIsLikedandPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsLiked",
                table: "PostReplyLikes",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "PostId",
                table: "PostReplyLikes",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostReplyLikes_PostId",
                table: "PostReplyLikes",
                column: "PostId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostReplyLikes_Posts_PostId",
                table: "PostReplyLikes",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostReplyLikes_Posts_PostId",
                table: "PostReplyLikes");

            migrationBuilder.DropIndex(
                name: "IX_PostReplyLikes_PostId",
                table: "PostReplyLikes");

            migrationBuilder.DropColumn(
                name: "IsLiked",
                table: "PostReplyLikes");

            migrationBuilder.DropColumn(
                name: "PostId",
                table: "PostReplyLikes");
        }
    }
}
