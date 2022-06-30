using MyImdb.Business.Repositories;
using MyImdb.Entities;

namespace MyImdb.Business.Services {
	public class ActorService {
		private readonly ActorRepository actorRepository;
		private readonly AppDbContext dbContext;

		public ActorService(ActorRepository actorRepository, AppDbContext dbContext) {
			this.actorRepository = actorRepository;
			this.dbContext = dbContext;
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
}
