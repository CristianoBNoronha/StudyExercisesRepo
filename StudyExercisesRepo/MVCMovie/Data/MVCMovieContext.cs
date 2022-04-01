#nullable disable
using Microsoft.EntityFrameworkCore;
using MVCMovie.Models;

namespace MVCMovie.Data
{
    public class MvcMovieContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlServer("Server=localhost,1433;Database=MVCMovie;User ID=sa;Password=password");
        }

        public MvcMovieContext (DbContextOptions<MvcMovieContext> options)
            : base(options)
        {
        }
        
        public DbSet<Movie> Movie { get; set; }
    }
}
