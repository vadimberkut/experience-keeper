using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ExperienceKeeper.Data.Migrations.ApplicationDb
{
    public partial class Add_Category_UserCategory_Record_RecordUserCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AspNetUsers",
                type: "character varying(39)",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedOnUtc",
                table: "AspNetUsers",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CAST(NOW() at time zone 'utc' AS timestamp)");

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedOnUtc",
                table: "AspNetUsers",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOnUtc",
                table: "AspNetUsers",
                type: "timestamp",
                nullable: false,
                defaultValueSql: "CAST(NOW() at time zone 'utc' AS timestamp)");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(39)", nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CAST(NOW() at time zone 'utc' AS timestamp)"),
                    DeletedOnUtc = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UpdatedOnUtc = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CAST(NOW() at time zone 'utc' AS timestamp)")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceRecords",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(39)", nullable: false),
                    Content = table.Column<string>(nullable: true),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CAST(NOW() at time zone 'utc' AS timestamp)"),
                    DeletedOnUtc = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    UpdatedOnUtc = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CAST(NOW() at time zone 'utc' AS timestamp)"),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ExperienceRecords_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserCategories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "character varying(39)", nullable: false),
                    CategoryId = table.Column<string>(nullable: false),
                    CreatedOnUtc = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CAST(NOW() at time zone 'utc' AS timestamp)"),
                    DeletedOnUtc = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UpdatedOnUtc = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "CAST(NOW() at time zone 'utc' AS timestamp)"),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserCategories_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ExperienceRecordUserCategories",
                columns: table => new
                {
                    RecordId = table.Column<string>(nullable: false),
                    UserCategoryId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExperienceRecordUserCategories", x => new { x.RecordId, x.UserCategoryId });
                    table.ForeignKey(
                        name: "FK_ExperienceRecordUserCategories_ExperienceRecords_RecordId",
                        column: x => x.RecordId,
                        principalTable: "ExperienceRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExperienceRecordUserCategories_UserCategories_UserCategoryId",
                        column: x => x.UserCategoryId,
                        principalTable: "UserCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceRecords_UserId",
                table: "ExperienceRecords",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ExperienceRecordUserCategories_UserCategoryId",
                table: "ExperienceRecordUserCategories",
                column: "UserCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCategories_CategoryId",
                table: "UserCategories",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_UserCategories_UserId",
                table: "UserCategories",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExperienceRecordUserCategories");

            migrationBuilder.DropTable(
                name: "ExperienceRecords");

            migrationBuilder.DropTable(
                name: "UserCategories");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropColumn(
                name: "CreatedOnUtc",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "DeletedOnUtc",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UpdatedOnUtc",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "AspNetUsers",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(39)");
        }
    }
}
