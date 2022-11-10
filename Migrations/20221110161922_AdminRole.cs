using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TaskManager.Migrations
{
    /// <inheritdoc />
    public partial class AdminRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var query = "IF NOT EXISTS(SELECT Id " +
                "FROM AspNetRoles WHERE Id = 'e87ca77a-6386-43dd-974c-0a370127ae05') " +
                "BEGIN " +
                "INSERT INTO AspNetRoles(Id, Name, NormalizedName) " +
                "VALUES('e87ca77a-6386-43dd-974c-0a370127ae05', 'admin', 'ADMIN') " +
                "END";
            migrationBuilder.Sql(query);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var query = "DELETE AspNetRoles WHERE Id = 'e87ca77a-6386-43dd-974c-0a370127ae05'";
            migrationBuilder.Sql(query);
        }
    }
}
