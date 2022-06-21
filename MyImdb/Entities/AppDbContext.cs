﻿using Microsoft.EntityFrameworkCore;

namespace MyImdb.Entities;

public class AppDbContext : DbContext {
	public DbSet<Movie> Movies { get; set; }

	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {
	}
}
