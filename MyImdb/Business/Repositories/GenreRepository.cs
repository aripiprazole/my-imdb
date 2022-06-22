using Microsoft.EntityFrameworkCore;
using MyImdb.Entities;

namespace MyImdb.Business.Repositories;

public class GenreRepository {
	private readonly AppDbContext dbContext;

	public GenreRepository(AppDbContext dbContext) {
		this.dbContext = dbContext;
	}

	public async Task<Genre?> SelectIdAsync(Guid id) {
		return await dbContext.Genres.FirstOrDefaultAsync(genre => genre.Id == id);
	}

	public async Task<List<Genre>> SelectTopNAsync(int n = 20) {
		return await dbContext.Genres
			.OrderBy(genre => genre.Name)
			.AsQueryable()
			.Take(n)
			.ToListAsync();
	}

	public async Task<Genre> CreateAsync(string name) {
		var genre = new Genre { Id = Guid.NewGuid(), Name = name };

		await dbContext.AddAsync(genre);

		return genre;
	}
}
