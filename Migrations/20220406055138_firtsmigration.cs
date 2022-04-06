using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace aspnetapp.Migrations
{
    public partial class firtsmigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Experiments",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    enabled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experiments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ForemResponses",
                columns: table => new
                {
                    Id = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    QuestionAnswers = table.Column<string>(type: "TEXT", nullable: true),
                    ExpId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ForemResponses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionId = table.Column<long>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    QuestionName = table.Column<string>(type: "TEXT", nullable: true),
                    QuestionType = table.Column<string>(type: "TEXT", nullable: true),
                    QuestionItemList = table.Column<string>(type: "TEXT", nullable: true),
                    ExpId = table.Column<long>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.QuestionId);
                    table.ForeignKey(
                        name: "FK__QUestion__Experiment_id__asdE",
                        column: x => x.ExpId,
                        principalTable: "Experiments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Experiments",
                columns: new[] { "Id", "Description", "Name", "enabled" },
                values: new object[] { 1L, "This is description for Experiment 1", "Experiment 1", true });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "ExpId", "QuestionItemList", "QuestionName", "QuestionType" },
                values: new object[] { 1L, 1L, "Answer Here", "Make up Sum Question 1", "text" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "ExpId", "QuestionItemList", "QuestionName", "QuestionType" },
                values: new object[] { 2L, 1L, "Answer Here", "Make up Sum Question 2", "text/multi" });

            migrationBuilder.InsertData(
                table: "Questions",
                columns: new[] { "QuestionId", "ExpId", "QuestionItemList", "QuestionName", "QuestionType" },
                values: new object[] { 3L, 1L, "item 1, item 2, item 3", "Make up Sum Question 3", "text/select" });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_ExpId",
                table: "Questions",
                column: "ExpId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ForemResponses");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Experiments");
        }
    }
}
