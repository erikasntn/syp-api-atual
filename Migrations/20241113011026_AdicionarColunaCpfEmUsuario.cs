using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjetoPI.Migrations
{
    public partial class AdicionarColunaCpfEmUsuario : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cpf",
                table: "usuarios",  
                nullable: true); 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cpf",
                table: "usuarios");
        }
    }
}
