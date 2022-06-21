using Microsoft.EntityFrameworkCore;

namespace MyImdb.Entities;

public class AppDbContext : DbContext {
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
	}

	public DbSet<Movie> Movies { get; set; }
	public DbSet<Movie> Genres { get; set; }
}
