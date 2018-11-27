using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace BibleForum.Data.Migrations
{
    public partial class NewTablePostReplySupportingVerse : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostReplySupportingBibleVerses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    BibleChapter = table.Column<string>(nullable: true),
                    BibleTranslation = table.Column<string>(nullable: true),
                    BibleVerse = table.Column<string>(nullable: true),
                    Created = table.Column<DateTime>(nullable: false),
                    PostId = table.Column<int>(nullable: true),
                    PostReplyId = table.Column<int>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReplySupportingBibleVerses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostReplySupportingBibleVerses_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostReplySupportingBibleVerses_PostReplies_PostReplyId",
                        column: x => x.PostReplyId,
                        principalTable: "PostReplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PostReplySupportingBibleVerses_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PostReplySupportingBibleVerses_PostId",
                table: "PostReplySupportingBibleVerses",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReplySupportingBibleVerses_PostReplyId",
                table: "PostReplySupportingBibleVerses",
                column: "PostReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReplySupportingBibleVerses_UserId",
                table: "PostReplySupportingBibleVerses",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostReplySupportingBibleVerses");
        }
    }
}
