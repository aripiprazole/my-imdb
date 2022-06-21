using Microsoft.EntityFrameworkCore;
using MyImdb.Entities;

namespace MyImdb.Business.Repositories; 

public class MovieRepository {
	private readonly AppDbContext dbContext;

	public MovieRepository(AppDbContext dbContext) {
		this.dbContext = dbContext;
	}
	
	public async Task<List<Movie>> SelectAllAsync() {
		return await dbContext.Movies.ToListAsync();
	}
}
