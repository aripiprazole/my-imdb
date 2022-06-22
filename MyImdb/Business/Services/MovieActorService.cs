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
}
