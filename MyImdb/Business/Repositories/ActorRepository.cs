using Microsoft.EntityFrameworkCore;
using MyImdb.Entities;

namespace MyImdb.Business.Repositories;

public class ActorRepository {
	private readonly AppDbContext dbContext;

	public ActorRepository(AppDbContext dbContext) {
		this.dbContext = dbContext;
	}

	public async Task<Actor?> SelectById(Guid id) {
		return await dbContext.Actors
			.Include(actor => actor.MovieActors)
			.FirstOrDefaultAsync(actor => actor.Id == id);
	}

	public async Task<List<Actor>> SelectTopN(int n = 20) {
		return await dbContext.Actors
			.Include(actor => actor.MovieActors)
			.OrderBy(actor => actor.Name)
			.Take(n)
			.ToListAsync();
	}

	public async Task<Actor> Create(string name, string birthplace) {
		var actor = new Actor { Id = Guid.NewGuid(), Name = name, Birthplace = birthplace };

		await dbContext.AddAsync(actor);

		return actor;
	}
}
