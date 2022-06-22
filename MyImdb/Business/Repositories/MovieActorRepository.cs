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
			.Include(pivot => pivot.Movie.Genre)
			.Where(pivot => pivot.ActorId == id)
			.ToListAsync();

		return pivots.ConvertAll(pivot => pivot.Movie);
	}

	public async Task LinkMovieToActorAsync(Guid movieId, Guid actorId) {
		var movieActor = new MovieActor { MovieId = movieId, ActorId = actorId };

		await dbContext.AddAsync(movieActor);
	}

	public async Task UnlinkMovieFromActorAsync(Guid movieId, Guid actorId) {
		var movieActor = await dbContext.MovieActors
			.FirstOrDefaultAsync(pivot => pivot.MovieId == movieId && pivot.ActorId == actorId);

		if (movieActor == null) {
			return;
		}

		dbContext.Remove(movieActor);
	}
}
