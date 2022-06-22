using MyImdb.Business.Repositories;
using MyImdb.Entities;

namespace MyImdb.Business.Services;

public class MovieActorService {
	private readonly MovieActorRepository actorRepository;
	private readonly AppDbContext dbContext;

	public MovieActorService(MovieActorRepository actorRepository, AppDbContext dbContext) {
		this.actorRepository = actorRepository;
		this.dbContext = dbContext;
	}

	public async Task<List<Movie>> SelectMoviesByActorId(Guid id, int n = 20) {
		return await actorRepository.SelectMoviesByActorId(id, n);
	}

	public async Task<List<Actor>> SelectActorsByMovieId(Guid id, int n = 20) {
		return await actorRepository.SelectActorsByMovieId(id, n);
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
