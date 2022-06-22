using Microsoft.EntityFrameworkCore;
using MyImdb.Entities;

namespace MyImdb.Business.Repositories;

public class MovieRepository {
	private readonly AppDbContext dbContext;

	public MovieRepository(AppDbContext dbContext) {
		this.dbContext = dbContext;
	}

	public Task<List<Movie>> SelectByGenreId(Guid genreId) {
		return dbContext.Movies.Where(movie => movie.GenreId == genreId).ToListAsync();
	}

	public async Task<List<Movie>> SelectTopNAsync(int n = 20) {
		return await dbContext.Movies
			.OrderBy(movie => movie.Title)
			.AsQueryable()
			.Take(n)
			.ToListAsync();
	}

	public async Task<Movie?> SelectByTitleAsync(string title) {
		return await dbContext.Movies.FirstOrDefaultAsync(movie => movie.Title == title);
	}

	public async Task<Movie?> SelectByIdAsync(Guid id) {
		return await dbContext.Movies.FirstOrDefaultAsync(movie => movie.Id == id);
	}

	public async Task<Movie> CreateAsync(int rank, string title, int year, string storyLine, Genre genre) {
		var movie = new Movie {
			Id = Guid.NewGuid(),
			Rank = rank,
			Title = title,
			Year = year,
			StoryLine = storyLine,
			CreationDate = DateTimeOffset.Now
		};

		await dbContext.AddAsync(movie);

		return movie;
	}
}
