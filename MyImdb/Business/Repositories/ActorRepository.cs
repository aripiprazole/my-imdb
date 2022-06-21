using MyImdb.Entities;

namespace MyImdb.Business.Repositories; 

public class ActorRepository {
	private readonly AppDbContext dbContext;

	public ActorRepository(AppDbContext dbContext) {
		this.dbContext = dbContext;
	}
}
