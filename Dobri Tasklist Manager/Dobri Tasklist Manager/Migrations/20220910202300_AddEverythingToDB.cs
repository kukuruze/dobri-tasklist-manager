using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dobri_Tasklist_Manager.Migrations
{
    public partial class AddEverythingToDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaskLists",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    DateOfCreation = table.Column<DateTime>(nullable: false),
                    DateOfLastEdit = table.Column<DateTime>(nullable: false),
                    IdCreator = table.Column<int>(nullable: false),
                    IdLastEditor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaskLists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ToDos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdTaskList = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsCompleted = table.Column<bool>(nullable: false),
                    DateOfCreation = table.Column<DateTime>(nullable: false),
                    DateOfLastChange = table.Column<DateTime>(nullable: false),
                    IdCreator = table.Column<int>(nullable: false),
                    IdLastEditor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToDos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    DateOfCreation = table.Column<DateTime>(nullable: false),
                    DateOfLastEdit = table.Column<DateTime>(nullable: false),
                    IdLastEditor = table.Column<int>(nullable: false),
                    IdCreator = table.Column<int>(nullable: false),
                    ListsCreatedByMe = table.Column<string>(nullable: true),
                    ListsSharedWithMe = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaskLists");

            migrationBuilder.DropTable(
                name: "ToDos");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
