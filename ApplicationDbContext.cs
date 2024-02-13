using Microsoft.EntityFrameworkCore;
using PeliculasAPI_ProyectoUdemy.Entidades;

namespace PeliculasAPI_ProyectoUdemy
{
    public class ApplicationDbContext: DbContext
    {
        private readonly DbContextOptions options;

        public ApplicationDbContext( DbContextOptions options ) :base (options)
        {
            this.options = options;
        }

        public DbSet<Genero> Generos { get; set;}
    }
}
