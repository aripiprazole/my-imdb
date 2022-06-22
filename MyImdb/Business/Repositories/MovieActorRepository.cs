using Microsoft.EntityFrameworkCore;
using MyImdb.Entities;

namespace MyImdb.Business.Repositories;

public class MovieActorRepository {
	private readonly AppDbContext dbContext;

	public MovieActorRepository(AppDbContext dbContext) {
		this.dbContext = dbContext;
	}

	public async Task<List<Actor>> SelectActorsByMovieIdAsync(Guid id, int n = 20) {
		var pivots = await dbContext.MovieActors
			.Include(pivot => pivot.Actor)
			.Where(pivot => pivot.MovieId == id)
			.ToListAsync();

		return pivots.ConvertAll(pivot => pivot.Actor);
	}

	public async Task<List<Movie>> SelectMoviesByActorIdAsync(Guid id, int n = 20) {
		var pivots = await dbContext.MovieActors
			.Include(pivot => pivot.Movie)
			.Where(pivot => pivot.ActorId == id)
			.ToListAsync();

		return pivots.ConvertAll(pivot => pivot.Movie);
	}
}
