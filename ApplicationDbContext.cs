using Microsoft.EntityFrameworkCore;
using PeliculasAPI_Udemy.Entidades;

namespace PeliculasAPI_Udemy
{
    public class ApplicationDbContext: DbContext
    {
        private readonly DbContextOptions options;

        public ApplicationDbContext( DbContextOptions options ) :base (options)
        {
            this.options = options;
        }

        public DbSet<Genero> Generos { get; set;}
        public DbSet<Actor> Actores { get; set; }
    }
}
