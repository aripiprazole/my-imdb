using Api;
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

	public async Task<Actor> SelectById(Guid id) {
		return await actorRepository.SelectById(id) ??
		       throw ApiException.Builder().Build(ErrorCode.ActorNotFound, new { id });
	}

	public async Task<List<Actor>> SelectTopN(int n = 20) {
		return await actorRepository.SelectTopN(n);
	}

	public async Task<Actor> Create(string name, string birthplace) {
		var movie = await actorRepository.Create(name, birthplace);

		await dbContext.SaveChangesAsync();

		return movie;
	}

	public async Task Update(Actor target, string name, string birthplace) {
		target.Name = name;
		target.Birthplace = birthplace;

		await dbContext.SaveChangesAsync();
	}

	public async Task Delete(Actor actor) {
		dbContext.Remove(actor);

		foreach (var movieActor in actor.MovieActors) {
			dbContext.Remove(movieActor);
		}

		await dbContext.SaveChangesAsync();
	}
}
