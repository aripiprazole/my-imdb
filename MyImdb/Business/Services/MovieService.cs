using Api;
using Microsoft.EntityFrameworkCore;
using MyImdb.Business.Repositories;
using MyImdb.Entities;

namespace MyImdb.Business.Services {
	public class MovieService {
		private readonly AppDbContext dbContext;
		private readonly ExceptionBuilder exceptionBuilder;
		private readonly GenreRepository genreRepository;
		private readonly MovieRepository movieRepository;

		public MovieService(
			AppDbContext dbContext,
			ExceptionBuilder exceptionBuilder,
			GenreRepository genreRepository,
			MovieRepository movieRepository
		) {
			this.dbContext = dbContext;
			this.exceptionBuilder = exceptionBuilder;
			this.genreRepository = genreRepository;
			this.movieRepository = movieRepository;
		}

		public async Task<Movie> CreateAsync(int rank, string title, int year, string storyLine, Guid genreId) {
			var genre = await genreRepository.SelectByIdAsync(genreId);

			var movie = await movieRepository.SelectByTitleAsync(title);
			if (movie != null) {
				throw exceptionBuilder.Api(ErrorCodes.MovieAlreadyExists, new { title });
			}

			movie = await movieRepository.CreateAsync(rank, title, year, storyLine, genre);

			await dbContext.SaveChangesAsync();

			return movie;
		}

		public async Task UpdateAsync(Movie target, int rank, string title, int year, string storyLine, Guid genreId) {
			var movieExists = await dbContext.Movies.AnyAsync(movie => movie.Title == title && movie.Id != target.Id);
			if (movieExists) {
				throw exceptionBuilder.Api(ErrorCodes.MovieAlreadyExists, new { title });
			}

			target.Rank = rank;
			target.Title = title;
			target.Year = year;
			target.StoryLine = storyLine;
			target.GenreId = genreId;

			await dbContext.SaveChangesAsync();
		}

		public async Task DeleteAsync(Movie movie) {
			dbContext.Remove(movie);

			foreach (var movieActor in movie.MovieActors) {
				dbContext.Remove(movieActor);
			}

			await dbContext.SaveChangesAsync();
		}
	}
}
