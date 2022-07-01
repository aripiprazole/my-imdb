using Api;
using Microsoft.EntityFrameworkCore;
using MyImdb.Business.Repositories;
using MyImdb.Entities;

namespace MyImdb.Business.Services {
	public class GenreService {
		private readonly AppDbContext dbContext;
		private readonly ExceptionBuilder exceptionBuilder;
		private readonly GenreRepository genreRepository;
		private readonly MovieRepository movieRepository;

		public GenreService(
			MovieRepository movieRepository,
			GenreRepository genreRepository,
			AppDbContext dbContext,
			ExceptionBuilder exceptionBuilder
		) {
			this.movieRepository = movieRepository;
			this.genreRepository = genreRepository;
			this.dbContext = dbContext;
			this.exceptionBuilder = exceptionBuilder;
		}

		public async Task<Genre> CreateAsync(string name) {
			var genre = await genreRepository.SelectByNameAsync(name);
			if (genre != null) {
				throw exceptionBuilder.Api(ErrorCodes.GenreNotFound, new { });
			}

			genre = await genreRepository.CreateAsync(name);

			await dbContext.SaveChangesAsync();

			return genre;
		}

		public async Task UpdateAsync(Genre target, string name) {
			var genreExists = await dbContext.Genres.AnyAsync(genre => genre.Name == name && genre.Id != target.Id);
			if (genreExists) {
				throw exceptionBuilder.Api(ErrorCodes.GenreAlreadyExists, new { name });
			}

			target.Name = name;

			await dbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(Genre genre) {
			var movies = await movieRepository.SelectByGenreId(genre.Id);

			dbContext.RemoveRange(movies);
			dbContext.Remove(genre);

			await dbContext.SaveChangesAsync();
		}
	}
}
