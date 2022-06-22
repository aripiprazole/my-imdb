using MyImdb.Entities;

namespace MyImdb.Business.Repositories;

public class MovieActorRepository {
	private readonly AppDbContext dbContext;

	public MovieActorRepository(AppDbContext dbContext) {
		this.dbContext = dbContext;
	}
}
