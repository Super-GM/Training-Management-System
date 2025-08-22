using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Training_Management_System.Migrations
{
    /// <inheritdoc />
    public partial class FinalMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Courses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Category = table.Column<int>(type: "int", nullable: false),
                    instructorid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courses", x => x.id);
                    table.ForeignKey(
                        name: "FK_Courses_Users_instructorid",
                        column: x => x.instructorid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    courseid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.id);
                    table.CheckConstraint("CK_Session_EndDate", "[EndDate] > [StartDate]");
                    table.CheckConstraint("ck_Session_StartDate", "[StartDate] > GETDATE()");
                    table.ForeignKey(
                        name: "FK_Sessions_Courses_courseid",
                        column: x => x.courseid,
                        principalTable: "Courses",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Grades",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Sessionid = table.Column<int>(type: "int", nullable: false),
                    Traineeid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grades", x => x.id);
                    table.CheckConstraint("CK_Grade_Value", "[Value] BETWEEN 0 AND 100");
                    table.ForeignKey(
                        name: "FK_Grades_Sessions_Sessionid",
                        column: x => x.Sessionid,
                        principalTable: "Sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Grades_Users_Traineeid",
                        column: x => x.Traineeid,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "Email", "Name", "Role" },
                values: new object[,]
                {
                    { 1, "mohamed@gmail.com", "Mohamed Adel", "instructor" },
                    { 2, "Moheb@gmail.com", "Moheb", "Trainer" },
                    { 3, "Omar@gmail.com", "Omar", "instructor" }
                });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "id", "Category", "Name", "instructorid" },
                values: new object[,]
                {
                    { 1, 0, "ASP MVC", 1 },
                    { 2, 1, "Biochemistry", 3 }
                });

            migrationBuilder.InsertData(
                table: "Sessions",
                columns: new[] { "id", "EndDate", "StartDate", "courseid" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2025, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 8, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 }
                });

            migrationBuilder.InsertData(
                table: "Grades",
                columns: new[] { "id", "Sessionid", "Traineeid", "Value" },
                values: new object[] { 1, 1, 2, 90m });

            migrationBuilder.CreateIndex(
                name: "IX_Courses_instructorid",
                table: "Courses",
                column: "instructorid");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_Name",
                table: "Courses",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Grades_Sessionid",
                table: "Grades",
                column: "Sessionid");

            migrationBuilder.CreateIndex(
                name: "IX_Grades_Traineeid",
                table: "Grades",
                column: "Traineeid");

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_courseid",
                table: "Sessions",
                column: "courseid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Grades");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropTable(
                name: "Courses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
