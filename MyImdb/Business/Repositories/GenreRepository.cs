using Api;
using Microsoft.EntityFrameworkCore;
using MyImdb.Entities;

namespace MyImdb.Business.Repositories {
	public class GenreRepository {
		private readonly AppDbContext dbContext;
		private readonly ExceptionBuilder exceptionBuilder;

		public GenreRepository(AppDbContext dbContext, ExceptionBuilder exceptionBuilder) {
			this.dbContext = dbContext;
			this.exceptionBuilder = exceptionBuilder;
		}

		public async Task<Genre> SelectById(Guid id) {
			var genre = await dbContext.Genres.FirstOrDefaultAsync(genre => genre.Id == id);

			return genre ?? throw exceptionBuilder.Api(ErrorCodes.GenreNotFound, new { id });
		}

		public async Task<List<Genre>> SelectTopN(int n = 20) {
			return await dbContext.Genres.OrderBy(genre => genre.Name).Take(n).ToListAsync();
		}

		public async Task<Genre> Create(string name) {
			var genre = new Genre() {
				Id = Guid.NewGuid(), Name = name,
			};

			await dbContext.AddAsync(genre);

			return genre;
		}

		public async Task<Genre?> SelectByName(string name) {
			return await dbContext.Genres.FirstOrDefaultAsync(genre => genre.Name == name);
		}
	}
}
