using MyImdb.Business.Repositories;
using MyImdb.Entities;

namespace MyImdb.Business.Services;

public class ActorService {
	private readonly ActorRepository actorRepository;
	private readonly AppDbContext dbContext;

	public ActorService(ActorRepository actorRepository, AppDbContext dbContext) {
		this.actorRepository = actorRepository;
		this.dbContext = dbContext;
	}
}
