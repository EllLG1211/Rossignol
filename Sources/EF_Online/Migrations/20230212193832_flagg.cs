using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EFOnline.Migrations
{
    /// <inheritdoc />
    public partial class flagg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReferencedUsers",
                columns: table => new
                {
                    Uid = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    Mail = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReferencedUsers", x => x.Uid);
                });

            migrationBuilder.CreateTable(
                name: "EntriesSet",
                columns: table => new
                {
                    Uid = table.Column<string>(type: "TEXT", nullable: false),
                    OwnerUid = table.Column<string>(type: "TEXT", nullable: false),
                    Login = table.Column<string>(type: "TEXT", nullable: false),
                    Password = table.Column<string>(type: "TEXT", nullable: false),
                    App = table.Column<string>(type: "TEXT", nullable: false),
                    Note = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EntriesSet", x => x.Uid);
                    table.ForeignKey(
                        name: "FK_EntriesSet_ReferencedUsers_OwnerUid",
                        column: x => x.OwnerUid,
                        principalTable: "ReferencedUsers",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConnectedUserEntityEntryEntity",
                columns: table => new
                {
                    SharedWithUid = table.Column<string>(type: "TEXT", nullable: false),
                    SharedWithUid1 = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectedUserEntityEntryEntity", x => new { x.SharedWithUid, x.SharedWithUid1 });
                    table.ForeignKey(
                        name: "FK_ConnectedUserEntityEntryEntity_EntriesSet_SharedWithUid1",
                        column: x => x.SharedWithUid1,
                        principalTable: "EntriesSet",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConnectedUserEntityEntryEntity_ReferencedUsers_SharedWithUid",
                        column: x => x.SharedWithUid,
                        principalTable: "ReferencedUsers",
                        principalColumn: "Uid",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ConnectedUserEntityEntryEntity_SharedWithUid1",
                table: "ConnectedUserEntityEntryEntity",
                column: "SharedWithUid1");

            migrationBuilder.CreateIndex(
                name: "IX_EntriesSet_OwnerUid",
                table: "EntriesSet",
                column: "OwnerUid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ConnectedUserEntityEntryEntity");

            migrationBuilder.DropTable(
                name: "EntriesSet");

            migrationBuilder.DropTable(
                name: "ReferencedUsers");
        }
    }
}
