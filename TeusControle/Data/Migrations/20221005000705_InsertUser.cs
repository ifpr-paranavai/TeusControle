using Microsoft.EntityFrameworkCore.Migrations;

namespace Data.Migrations
{
    public partial class InsertUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
				INSERT INTO
					teuscontroledb.users
				(
					Name,
					BirthDate,
					ProfileImage,
					ProfileType,
					Password,
					Email,
					Active,
					Deleted,
					CreatedDate,
					LastChange,
					CreatedBy
				) VALUES(
					'Usuario Administrador',
					'2001-11-27 00:00:00',
					'https://ciclovivo.com.br/wp-content/uploads/2018/10/iStock-536613027-696x464.jpg',
					'Admin',
					'AQAAAAEAACcQAAAAEBk6g+HOnrf1xQioBXKKBwj2MK8JtYAx3UUsWI31fz5dUQO2ZY/B8GTI90AhS0YXfw==',
					'admin@admin.com',
					1,
					0,
					'2022-10-04 22:52:24.064144',
					null,
					null
				);");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
			migrationBuilder.Sql(@"
				DELETE FROM teuscontroledb.users WHERE Name = 'Usuario Administrador';
			");
		}
    }
}
