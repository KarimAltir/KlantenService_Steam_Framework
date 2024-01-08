using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KlantenService_Steam_Framework.Data.Migrations
{
    /// <inheritdoc />
    public partial class Parameters : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_Game_GameId",
                table: "Complaint");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaint_ProblemType_ProblemTypeId",
                table: "Complaint");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProblemType",
                table: "ProblemType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Complaint",
                table: "Complaint");

            migrationBuilder.RenameTable(
                name: "ProblemType",
                newName: "ProblemTypes");

            migrationBuilder.RenameTable(
                name: "Game",
                newName: "Games");

            migrationBuilder.RenameTable(
                name: "Complaint",
                newName: "Complaints");

            migrationBuilder.RenameIndex(
                name: "IX_Complaint_ProblemTypeId",
                table: "Complaints",
                newName: "IX_Complaints_ProblemTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Complaint_GameId",
                table: "Complaints",
                newName: "IX_Complaints_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProblemTypes",
                table: "ProblemTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Complaints",
                table: "Complaints",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Parameters",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastChanged = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Obsolete = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parameters", x => x.Name);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Games_GameId",
                table: "Complaints",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_ProblemTypes_ProblemTypeId",
                table: "Complaints",
                column: "ProblemTypeId",
                principalTable: "ProblemTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Games_GameId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_ProblemTypes_ProblemTypeId",
                table: "Complaints");

            migrationBuilder.DropTable(
                name: "Parameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProblemTypes",
                table: "ProblemTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Complaints",
                table: "Complaints");

            migrationBuilder.RenameTable(
                name: "ProblemTypes",
                newName: "ProblemType");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Game");

            migrationBuilder.RenameTable(
                name: "Complaints",
                newName: "Complaint");

            migrationBuilder.RenameIndex(
                name: "IX_Complaints_ProblemTypeId",
                table: "Complaint",
                newName: "IX_Complaint_ProblemTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Complaints_GameId",
                table: "Complaint",
                newName: "IX_Complaint_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProblemType",
                table: "ProblemType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Complaint",
                table: "Complaint",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_Game_GameId",
                table: "Complaint",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaint_ProblemType_ProblemTypeId",
                table: "Complaint",
                column: "ProblemTypeId",
                principalTable: "ProblemType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
