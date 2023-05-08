using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RoyalFinalApp.Migrations
{
    /// <inheritdoc />
    public partial class m4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HallType",
                table: "Halls");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Topbars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPuplished",
                table: "Topbars",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Topbars",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Testimonials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPuplished",
                table: "Testimonials",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Testimonials",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Planners",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPuplished",
                table: "Planners",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Planners",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Halls",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "HallName",
                table: "Halls",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Halls",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Halls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Halls",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPuplished",
                table: "Halls",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Halls",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsPuplished",
                table: "Categories",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Halls_CategoryId",
                table: "Halls",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Halls_Categories_CategoryId",
                table: "Halls",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "CategoryId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Halls_Categories_CategoryId",
                table: "Halls");

            migrationBuilder.DropIndex(
                name: "IX_Halls_CategoryId",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Topbars");

            migrationBuilder.DropColumn(
                name: "IsPuplished",
                table: "Topbars");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Topbars");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Testimonials");

            migrationBuilder.DropColumn(
                name: "IsPuplished",
                table: "Testimonials");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Testimonials");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Planners");

            migrationBuilder.DropColumn(
                name: "IsPuplished",
                table: "Planners");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Planners");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "IsPuplished",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Halls");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "IsPuplished",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Categories");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Halls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HallName",
                table: "Halls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "Halls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HallType",
                table: "Halls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
