using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AnnoBibLibrary.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AnnotationLinks",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LibraryId = table.Column<int>(nullable: false),
                    AnnotationId = table.Column<int>(nullable: false),
                    KeywordValues = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnotationLinks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Libraries",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(maxLength: 255, nullable: false),
                    Description = table.Column<string>(maxLength: 511, nullable: true),
                    KeywordGroups = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Libraries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Sources",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Type = table.Column<string>(nullable: false),
                    Fields = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sources", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Annotations",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Notes = table.Column<string>(nullable: false),
                    QuoteData = table.Column<string>(nullable: false),
                    SourceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Annotations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Annotations_Sources_SourceId",
                        column: x => x.SourceId,
                        principalTable: "Sources",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AnnotationLinks",
                columns: new[] { "Id", "AnnotationId", "KeywordValues", "LibraryId" },
                values: new object[,]
                {
                    { 1, 1, "", 1 },
                    { 2, 2, "", 1 },
                    { 3, 3, "", 1 },
                    { 4, 4, "", 2 },
                    { 5, 5, "", 2 },
                    { 6, 6, "", 2 }
                });

            migrationBuilder.InsertData(
                table: "Libraries",
                columns: new[] { "Id", "Description", "KeywordGroups", "Title" },
                values: new object[,]
                {
                    { 1, "The collected works of J.R.R. Tolkien, a 20th century fantasy writer.", "['Places', 'People', 'Concepts']", "J.R.R. Tolkien Library" },
                    { 2, "The collected works of C.S. Lewis, a 20th century fantasy/science-fiction/non-fiction writer.", "['Places', 'People', 'Concepts']", "C.S. Lewis Library" }
                });

            migrationBuilder.InsertData(
                table: "Sources",
                columns: new[] { "Id", "Fields", "Type" },
                values: new object[,]
                {
                    { 1, "{\"list\": [{ \"name\": \"title\", \"type\": \"proper\", \"values\": [\"The Lord of the Rings: the Fellowship of the Ring\"] }, { \"name\": \"author\", \"type\": \"name\", \"values\": [\"J.R.R. Tolkien\"] }]}", "Simple" },
                    { 2, "{\"list\": [{ \"name\": \"title\", \"type\": \"proper\", \"values\": [\"The Lord of the Rings: the Two Towers\"] }, { \"name\": \"author\", \"type\": \"name\", \"values\": [\"J.R.R. Tolkien\"] }]}", "Simple" },
                    { 3, "{\"list\": [{ \"name\": \"title\", \"type\": \"proper\", \"values\": [\"The Lord of the Rings: the Return of the King\"] }, { \"name\": \"author\", \"type\": \"name\", \"values\": [\"J.R.R. Tolkien\"] }]}", "Simple" },
                    { 4, "{\"list\": [{ \"name\": \"title\", \"type\": \"proper\", \"values\": [\"The Chronicles of Narnia: the Lion, the Witch, and the Wardrobe\"] }, { \"name\": \"author\", \"type\": \"name\", \"values\": [\"C.S. Lewis\"] }]}", "Simple" },
                    { 5, "{\"list\": [{ \"name\": \"title\", \"type\": \"proper\", \"values\": [\"The Chronicles of Narnia: Prince Caspian\"] }, { \"name\": \"author\", \"type\": \"name\", \"values\": [\"C.S. Lewis\"] }]}", "Simple" },
                    { 6, "{\"list\": [{ \"name\": \"title\", \"type\": \"proper\", \"values\": [\"The Chronicles of Narnia: the Magician's Nephew\"] }, { \"name\": \"author\", \"type\": \"name\", \"values\": [\"C.S. Lewis\"] }]}", "Simple" }
                });

            migrationBuilder.InsertData(
                table: "Annotations",
                columns: new[] { "Id", "Notes", "QuoteData", "SourceId" },
                values: new object[,]
                {
                    { 1, "Fellowship", "[]", 1 },
                    { 2, "Towers", "[]", 2 },
                    { 3, "King", "[]", 3 },
                    { 4, "Lion", "[]", 4 },
                    { 5, "Prince", "[]", 5 },
                    { 6, "Magician", "[]", 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Annotations_SourceId",
                table: "Annotations",
                column: "SourceId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnnotationLinks");

            migrationBuilder.DropTable(
                name: "Annotations");

            migrationBuilder.DropTable(
                name: "Libraries");

            migrationBuilder.DropTable(
                name: "Sources");
        }
    }
}
