using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SendMessage.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "InformEmails",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Subject = table.Column<string>(type: "text", nullable: true),
                    Body = table.Column<string>(type: "text", nullable: true),
                    Recipients = table.Column<List<string>>(type: "text[]", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Result = table.Column<string>(type: "text", nullable: true),
                    FailedMessage = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InformEmails", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "InformEmails",
                columns: new[] { "Id", "Body", "CreatedDate", "FailedMessage", "Recipients", "Result", "Subject" },
                values: new object[] { new Guid("d7d68390-65ac-465c-a6e1-02721847854e"), "root", new DateTime(2024, 1, 17, 10, 40, 53, 771, DateTimeKind.Utc).AddTicks(7290), "root", new List<string> { "root", "root1" }, "root", "root" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InformEmails");
        }
    }
}
