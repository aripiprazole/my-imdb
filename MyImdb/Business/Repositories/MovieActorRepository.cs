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

		public async Task<MovieActor> SelectByIdAsync(Guid id) {
			var movieActor = await dbContext.MovieActors.FirstOrDefaultAsync(movieActor => movieActor.Id == id);

			return movieActor ?? throw exceptionBuilder.Api(ErrorCodes.MovieActorNotFound, new { id });
		}

		public async Task<List<MovieActor>> SelectByMovieIdAsync(Guid movieId, int n = 20) {
			var movieActors = await dbContext.MovieActors
				.Where(movieActor => movieActor.MovieId == movieId)
				.ToListAsync();

			return movieActors;
		}

		public async Task<List<MovieActor>> SelectByActorIdAsync(Guid actorId, int n = 20) {
			var movieActors = await dbContext.MovieActors
				.Where(movieActor => movieActor.ActorId == actorId)
				.ToListAsync();

			return movieActors;
		}

		public async Task<MovieActor> CreateAsync(Guid movieId, Guid actorId) {
			var movieActor = new MovieActor() {
				Id = Guid.NewGuid(),
				MovieId = movieId,
				ActorId = actorId,
			};

			await dbContext.AddAsync(movieActor);

			return movieActor;
		}

		public async Task DeleteAsync(Guid id) {
			var movieActor = await dbContext.MovieActors.FirstOrDefaultAsync(pivot => pivot.Id == id);

			if (movieActor == null) {
				return;
			}

			dbContext.Remove(movieActor);
		}
	}
}
