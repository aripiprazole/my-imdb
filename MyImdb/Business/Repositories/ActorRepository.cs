using Api;
using Microsoft.EntityFrameworkCore;
using MyImdb.Entities;

namespace MyImdb.Business.Repositories {
	public class ActorRepository {
		private readonly AppDbContext dbContext;
		private readonly ExceptionBuilder exceptionBuilder;

		public ActorRepository(AppDbContext dbContext, ExceptionBuilder exceptionBuilder) {
			this.dbContext = dbContext;
			this.exceptionBuilder = exceptionBuilder;
		}

		public async Task<Actor> SelectByIdAsync(Guid id) {
			var actor = await dbContext.Actors.Include(actor => actor.MovieActors)
				.FirstOrDefaultAsync(actor => actor.Id == id);

			return actor ?? throw exceptionBuilder.Api(ErrorCodes.ActorNotFound, new { id });
		}

		public async Task<List<Actor>> SelectTopNAsync(int n = 20) {
			return await dbContext.Actors.Include(actor => actor.MovieActors)
				.OrderBy(actor => actor.Name)
				.Take(n)
				.ToListAsync();
		}

		public async Task<Actor> CreateAsync(string name, string birthplace) {
			var actor = new Actor() {
				Id = Guid.NewGuid(),
				Name = name,
				Birthplace = birthplace,
			};

			await dbContext.AddAsync(actor);

			return actor;
		}
	}
}
