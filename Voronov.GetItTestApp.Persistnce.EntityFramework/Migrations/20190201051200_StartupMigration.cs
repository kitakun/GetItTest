using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Voronov.GetItTestApp.Persistence.EntityFramework.Migrations
{
    public partial class StartupMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ErrorRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    InputDate = table.Column<DateTime>(nullable: false),
                    ShortDescription = table.Column<string>(nullable: true),
                    FullDescription = table.Column<string>(nullable: true),
                    Status = table.Column<int>(nullable: false),
                    Urgency = table.Column<int>(nullable: false),
                    ImportanceType = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Login = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ErrorChangeRecords",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Date = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(nullable: true),
                    Action = table.Column<int>(nullable: false),
                    ChangedById = table.Column<int>(nullable: false),
                    ErrorRecordId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorChangeRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ErrorChangeRecords_Users_ChangedById",
                        column: x => x.ChangedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ErrorChangeRecords_ErrorRecords_ErrorRecordId",
                        column: x => x.ErrorRecordId,
                        principalTable: "ErrorRecords",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ErrorRecords",
                columns: new[] { "Id", "FullDescription", "ImportanceType", "InputDate", "ShortDescription", "Status", "Urgency" },
                values: new object[,]
                {
                    { -1, "side bar 1 side bar side bar side bar 1 side bar side bar side bar1  side bar ", 0, new DateTime(2019, 2, 1, 12, 13, 0, 299, DateTimeKind.Local), "side bar 1 ...", 0, 0 },
                    { -2, "side bar 2 side bar side bar side bar 2 side bar side bar side bar2  side bar ", 0, new DateTime(2019, 2, 1, 12, 14, 0, 299, DateTimeKind.Local), "side bar 2 ...", 0, 0 },
                    { -3, "side bar 3 side bar side bar side bar 3 side bar side bar side bar3  side bar ", 0, new DateTime(2019, 2, 1, 12, 15, 0, 299, DateTimeKind.Local), "side bar 3 ...", 0, 0 },
                    { -4, "side bar 4 side bar side bar side bar 4 side bar side bar side bar4  side bar ", 0, new DateTime(2019, 2, 1, 12, 16, 0, 299, DateTimeKind.Local), "side bar 4 ...", 0, 0 },
                    { -5, "side bar 5 side bar side bar side bar 5 side bar side bar side bar5  side bar ", 0, new DateTime(2019, 2, 1, 12, 17, 0, 299, DateTimeKind.Local), "side bar 5 ...", 0, 0 },
                    { -6, "side bar 6 side bar side bar side bar 6 side bar side bar side bar6  side bar ", 0, new DateTime(2019, 2, 1, 12, 18, 0, 299, DateTimeKind.Local), "side bar 6 ...", 0, 0 },
                    { -7, "side bar 7 side bar side bar side bar 7 side bar side bar side bar7  side bar ", 0, new DateTime(2019, 2, 1, 12, 19, 0, 299, DateTimeKind.Local), "side bar 7 ...", 0, 0 },
                    { -8, "side bar 8 side bar side bar side bar 8 side bar side bar side bar8  side bar ", 0, new DateTime(2019, 2, 1, 12, 20, 0, 299, DateTimeKind.Local), "side bar 8 ...", 0, 0 },
                    { -9, "side bar 9 side bar side bar side bar 9 side bar side bar side bar9  side bar ", 0, new DateTime(2019, 2, 1, 12, 21, 0, 299, DateTimeKind.Local), "side bar 9 ...", 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Login", "Password" },
                values: new object[,]
                {
                    { -1, "SuperUser", "Wabwabwba", "admin", "adm" },
                    { -2, "Kesha", "Karpik", "ksh", "ksh" },
                    { -3, "Zegerman", "Underfatch", "zg", "zg" }
                });

            migrationBuilder.InsertData(
                table: "ErrorChangeRecords",
                columns: new[] { "Id", "Action", "ChangedById", "Comment", "Date", "ErrorRecordId" },
                values: new object[,]
                {
                    { -1, 0, -1, "Temp Comment", new DateTime(2019, 1, 31, 12, 12, 0, 298, DateTimeKind.Local), -1 },
                    { -2, 0, -1, "Temp Comment", new DateTime(2019, 1, 30, 12, 12, 0, 298, DateTimeKind.Local), -2 },
                    { -3, 0, -1, "Temp Comment", new DateTime(2019, 1, 29, 12, 12, 0, 298, DateTimeKind.Local), -3 },
                    { -4, 0, -1, "Temp Comment", new DateTime(2019, 1, 28, 12, 12, 0, 298, DateTimeKind.Local), -4 },
                    { -5, 0, -1, "Temp Comment", new DateTime(2019, 1, 27, 12, 12, 0, 298, DateTimeKind.Local), -5 },
                    { -6, 0, -1, "Temp Comment", new DateTime(2019, 1, 26, 12, 12, 0, 298, DateTimeKind.Local), -6 },
                    { -7, 0, -1, "Temp Comment", new DateTime(2019, 1, 25, 12, 12, 0, 298, DateTimeKind.Local), -7 },
                    { -8, 0, -1, "Temp Comment", new DateTime(2019, 1, 24, 12, 12, 0, 298, DateTimeKind.Local), -8 },
                    { -9, 0, -1, "Temp Comment", new DateTime(2019, 1, 23, 12, 12, 0, 298, DateTimeKind.Local), -9 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorChangeRecords_ChangedById",
                table: "ErrorChangeRecords",
                column: "ChangedById");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorChangeRecords_ErrorRecordId",
                table: "ErrorChangeRecords",
                column: "ErrorRecordId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorChangeRecords");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "ErrorRecords");
        }
    }
}
