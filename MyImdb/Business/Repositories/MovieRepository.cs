using Api;
using Microsoft.EntityFrameworkCore;
using MyImdb.Entities;

namespace MyImdb.Business.Repositories {
	public class MovieRepository {
		private readonly AppDbContext dbContext;
		private readonly ExceptionBuilder exceptionBuilder;

		public MovieRepository(AppDbContext dbContext, ExceptionBuilder exceptionBuilder) {
			this.dbContext = dbContext;
			this.exceptionBuilder = exceptionBuilder;
		}

		public Task<List<Movie>> SelectByGenreId(Guid genreId) {
			return dbContext.Movies.Include(movie => movie.Genre)
				.Include(movie => movie.MovieActors)
				.Where(movie => movie.GenreId == genreId)
				.ToListAsync();
		}

		public async Task<List<Movie>> SelectTopN(int n = 20) {
			return await dbContext.Movies.Include(movie => movie.Genre)
				.Include(movie => movie.MovieActors)
				.OrderBy(movie => movie.Title)
				.Take(n)
				.ToListAsync();
		}

		public async Task<Movie?> SelectByTitle(string title) {
			return await dbContext.Movies.Include(movie => movie.Genre)
				.Include(movie => movie.MovieActors)
				.FirstOrDefaultAsync(movie => movie.Title == title);
		}

		public async Task<Movie> SelectById(Guid id) {
			var movie = await dbContext.Movies.Include(movie => movie.Genre)
				.Include(movie => movie.MovieActors)
				.FirstOrDefaultAsync(movie => movie.Id == id);

			return movie ?? throw exceptionBuilder.Api(ErrorCodes.MovieNotFound, new { id });
		}

		public async Task<Movie> Create(int rank, string title, int year, string storyLine, Genre genre) {
			var movie = new Movie() {
				Id = Guid.NewGuid(),
				Rank = rank,
				Title = title,
				Year = year,
				StoryLine = storyLine,
				CreationDate = DateTimeOffset.Now,
				GenreId = genre.Id,
			};

			await dbContext.AddAsync(movie);

			return movie;
		}
	}
}
