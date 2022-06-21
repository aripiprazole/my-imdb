using Api;
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
}
