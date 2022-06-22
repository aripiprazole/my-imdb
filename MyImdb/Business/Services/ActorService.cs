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

	public async Task<Actor> SelectByIdAsync(Guid id) {
		return await actorRepository.SelectByIdAsync(id) ??
		       throw ApiException.Builder().Build(ErrorCode.ActorNotFound);
	}

	public async Task<List<Actor>> SelectTopNAsync(int n = 20) {
		return await actorRepository.SelectTopNAsync(n);
	}

	public async Task<Actor> CreateAsync(string name, string birthplace) {
		var movie = await actorRepository.CreateAsync(name, birthplace);

		await dbContext.SaveChangesAsync();

		return movie;
	}

	public async Task UpdateAsync(Actor target, string name, string birthplace) {
		target.Name = name;
		target.Birthplace = birthplace;

		await dbContext.SaveChangesAsync();
	}

	public async Task DeleteAsync(Actor actor) {
		dbContext.Remove(actor);

		foreach (var movieActor in actor.MovieActors) {
			dbContext.Remove(movieActor);
		}

		await dbContext.SaveChangesAsync();
	}
}
