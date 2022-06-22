using Api;
using MyImdb.Business.Repositories;
using MyImdb.Entities;

namespace MyImdb.Business.Services;

public class MovieService {
	private readonly GenreRepository genreRepository;
	private readonly MovieRepository movieRepository;
	private readonly AppDbContext dbContext;

	public MovieService(GenreRepository genreRepository, MovieRepository movieRepository, AppDbContext dbContext) {
		this.genreRepository = genreRepository;
		this.movieRepository = movieRepository;
		this.dbContext = dbContext;
	}

	public async Task<Movie> CreateAsync(int rank, string title, int year, string storyLine, Guid genreId) {
		var genre = await genreRepository.SelectIdAsync(genreId) ??
		            throw ApiException.Builder().Build(ErrorCode.GenreNotFound);

		var movie = await movieRepository.SelectByTitleAsync(title);
		if (movie != null) {
			throw ApiException.Builder().Build(ErrorCode.MovieTitleAlreadyExists, new { title });
		}

		movie = await movieRepository.CreateAsync(rank, title, year, storyLine, genre);

		await dbContext.SaveChangesAsync();

		return movie;
	}
}
