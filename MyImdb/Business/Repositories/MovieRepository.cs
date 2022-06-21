using Microsoft.EntityFrameworkCore;
using MyImdb.Entities;

namespace MyImdb.Business.Repositories;

public class MovieRepository {
	private readonly AppDbContext dbContext;

	public MovieRepository(AppDbContext dbContext) {
		this.dbContext = dbContext;
	}

	public async Task<List<Movie>> SelectTopNAsync(int n = 20) {
		return await dbContext.Movies
			.OrderBy(movie => movie.Title)
			.AsQueryable()
			.Take(n)
			.ToListAsync();
	}
}
