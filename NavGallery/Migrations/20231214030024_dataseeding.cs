using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NavGallery.Migrations
{
    public partial class dataseeding : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
          table: "Marca",
          columns: new[] { "Id", "Name" },
          values: new object[,]
         {
                 { 20, "Volkswagen" },
                 { 21, "Honda" },
                 { 22, "Ford"},
         });

            migrationBuilder.InsertData(
                table: "Carros",
                columns: new[] { "Id", "Modelo", "AnoCar", "Sobre", "FotoCar", "MarcaId", "DataInsert" },
                values: new object[,]
                {
                 { 6, "Fusca", 1970, "Um clássico carro compacto.", "fusca.jpg", 20, DateTime.Now },
                 { 7, "Civic", 2022, "Um sedan popular e confiável.", "civic.jpg", 21, DateTime.Now },
                 { 8, "Mustang", 1967, "Um ícone dos muscle cars americanos.", "mustang.jpg", 22, DateTime.Now },
                 { 9, "Gol", 2015, "Um hatchback popular no Brasil.", "gol.jpg", 20, DateTime.Now },
                 { 10, "Polo", 2020, "Um hatchback moderno e seguro.", "polo.jpg", 20, DateTime.Now }
                });

        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
