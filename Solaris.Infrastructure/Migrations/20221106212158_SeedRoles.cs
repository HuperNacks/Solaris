using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Solaris.Infrastructure.Migrations
{
    public partial class SeedRoles : Migration
    {
        private string MasterRoleId = Guid.NewGuid().ToString();
        private string AdminRoleId = Guid.NewGuid().ToString(); 

        protected override void Up(MigrationBuilder migrationBuilder)
        {
            SeedRolesSQL(migrationBuilder);
        }

        private void SeedRolesSQL(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql($@"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{MasterRoleId}', 'Master', 'MASTER', null);");
            migrationBuilder.Sql($@"INSERT INTO [dbo].[AspNetRoles] ([Id],[Name],[NormalizedName],[ConcurrencyStamp])
            VALUES ('{AdminRoleId}', 'Admin', 'ADMIN', null);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
