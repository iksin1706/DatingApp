using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class MessegeEntityAddedFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messeges_Users_RecipientId",
                table: "Messeges");

            migrationBuilder.DropForeignKey(
                name: "FK_Messeges_Users_SenderId",
                table: "Messeges");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messeges",
                table: "Messeges");

            migrationBuilder.RenameTable(
                name: "Messeges",
                newName: "Messages");

            migrationBuilder.RenameIndex(
                name: "IX_Messeges_SenderId",
                table: "Messages",
                newName: "IX_Messages_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Messeges_RecipientId",
                table: "Messages",
                newName: "IX_Messages_RecipientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_RecipientId",
                table: "Messages",
                column: "RecipientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_RecipientId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "Messeges");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_SenderId",
                table: "Messeges",
                newName: "IX_Messeges_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_RecipientId",
                table: "Messeges",
                newName: "IX_Messeges_RecipientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messeges",
                table: "Messeges",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Messeges_Users_RecipientId",
                table: "Messeges",
                column: "RecipientId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Messeges_Users_SenderId",
                table: "Messeges",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
