using MyImdb.Business.Repositories;
using MyImdb.Entities;

namespace MyImdb.Business.Services {
	public class MovieActorService {
		private readonly MovieActorRepository actorRepository;
		private readonly AppDbContext dbContext;

		public MovieActorService(MovieActorRepository actorRepository, AppDbContext dbContext) {
			this.actorRepository = actorRepository;
			this.dbContext = dbContext;
		}

		public async Task LinkMovieToActor(Guid movieId, Guid actorId) {
			await actorRepository.LinkMovieToActor(movieId, actorId);

			await dbContext.SaveChangesAsync();
		}

		public async Task UnlinkMovieFromActor(Guid movieId, Guid actorId) {
			await actorRepository.UnlinkMovieFromActor(movieId, actorId);

			await dbContext.SaveChangesAsync();
		}
	}
}
