using Api;
using Microsoft.EntityFrameworkCore;
using MyImdb.Entities;

namespace MyImdb.Business.Repositories {
	public class MovieActorRepository {
		private readonly AppDbContext dbContext;
		private readonly ExceptionBuilder exceptionBuilder;

		public MovieActorRepository(AppDbContext dbContext, ExceptionBuilder exceptionBuilder) {
			this.dbContext = dbContext;
			this.exceptionBuilder = exceptionBuilder;
		}

		public async Task<List<Actor>> SelectActorsByMovieId(Guid id, int n = 20) {
			var pivots = await dbContext.MovieActors.Include(pivot => pivot.Actor)
				.Include(pivot => pivot.Actor.MovieActors)
				.Include(pivot => pivot.Movie)
				.Include(pivot => pivot.Movie.Genre)
				.Include(pivot => pivot.Movie.MovieActors)
				.Where(pivot => pivot.MovieId == id)
				.ToListAsync();

			return pivots.ConvertAll(pivot => pivot.Actor);
		}

		public async Task<List<Movie>> SelectMoviesByActorId(Guid id, int n = 20) {
			var pivots = await dbContext.MovieActors.Include(pivot => pivot.Actor)
				.Include(pivot => pivot.Actor.MovieActors)
				.Include(pivot => pivot.Movie)
				.Include(pivot => pivot.Movie.Genre)
				.Include(pivot => pivot.Movie.MovieActors)
				.Where(pivot => pivot.ActorId == id)
				.ToListAsync();

			return pivots.ConvertAll(pivot => pivot.Movie);
		}

		public async Task LinkMovieToActor(Guid movieId, Guid actorId) {
			var movieActor = new MovieActor() {
				MovieId = movieId,
				ActorId = actorId
			};

			await dbContext.AddAsync(movieActor);
		}

		public async Task UnlinkMovieFromActor(Guid movieId, Guid actorId) {
			var movieActor =
				await dbContext.MovieActors.FirstOrDefaultAsync(
					pivot => pivot.MovieId == movieId && pivot.ActorId == actorId
				);

			if (movieActor == null) {
				return;
			}

			dbContext.Remove(movieActor);
		}
	}
}
