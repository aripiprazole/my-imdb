using System.Reflection.PortableExecutable;
using Microsoft.EntityFrameworkCore;
using MyImdb.Business.Repositories;
using MyImdb.Entities;

namespace MyImdb.Business.Services {
	public class MovieActorService {
		private readonly MovieActorRepository movieActorRepository;
		private readonly AppDbContext dbContext;

		public MovieActorService(MovieActorRepository movieActorRepository, AppDbContext dbContext) {
			this.movieActorRepository = movieActorRepository;
			this.dbContext = dbContext;
		}

		public async Task<MovieActor> Create(Guid movieId, Guid actorId, string character) {
			var movieActor = await movieActorRepository.Create(movieId, actorId);

			await dbContext.SaveChangesAsync();

			return movieActor;
		}

		public async Task Update(MovieActor movieActor, string character) {
			movieActor.Character = character;

			await dbContext.SaveChangesAsync();
		}

		public async Task Delete(MovieActor movieActor) {
			await movieActorRepository.Delete(movieActor.Id);

			await dbContext.SaveChangesAsync();
		}
	}
}
