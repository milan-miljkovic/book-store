using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Data.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[,]
                {
                    { 1, "Test category 1" },
                    { 2, "Test category 2" },
                    { 3, "Test category 3" },
                    { 4, "Test category 4" }
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "BookId", "Author", "CategoryId", "Description", "ImageUrl", "Price", "Title" },
                values: new object[,]
                {
                    { 1, "Test author 1", 1, "test description 1", "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png", 10.1, "Test title 1" },
                    { 2, "Test author 2", 1, "test description 2", "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png", 5.5, "Test title 2" },
                    { 3, "Test author 3", 1, "test description 3", "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png", 12.0, "Test title 3" },
                    { 4, "Test author 4", 2, "test description 4", "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png", 10.99, "Test title 4" },
                    { 5, "Test author 5", 2, "test description 5", "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png", 11.0, "Test title 5" },
                    { 6, "Test author 6", 3, "test description 6", "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png", 10.0, "Test title 6" },
                    { 7, "Test author 7", 3, "test description 7", "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png", 7.7, "Test title 7" },
                    { 8, "Test author 8", 3, "test description 8", "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png", 7.0, "Test title 8" },
                    { 9, "Test author 9", 3, "test description 9", "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png", 10.0, "Test title 9" },
                    { 10, "Test author 10", 3, "test description 10", "http://beenandgoing.com/wp-content/uploads/2015/04/LFTTVPBookImage.png", 9.8, "Test title 10" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Books",
                keyColumn: "BookId",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "CategoryId",
                keyValue: 3);
        }
    }
}
