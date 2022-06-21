using Api;
using MyImdb.Business.Repositories;
using MyImdb.Entities;

namespace MyImdb.Business.Services;

public class MovieService {
	private readonly GenreService genreService;
	private readonly MovieRepository movieRepository;
	private readonly AppDbContext dbContext;

	public MovieService(GenreService genreService, MovieRepository movieRepository, AppDbContext dbContext) {
		this.genreService = genreService;
		this.movieRepository = movieRepository;
		this.dbContext = dbContext;
	}

	public async Task<Movie> CreateAsync(int rank, string title, int year, string storyLine, Guid genreId) {
		var genre = await genreService.GetByIdAsync(genreId);
		var movie = await movieRepository.SelectByTitleAsync(title);
		if (movie != null) {
			throw ApiException.Builder().Build(ErrorCode.MovieTitleAlreadyExists, new { title });
		}

		movie = await movieRepository.CreateAsync(rank, title, year, storyLine, genre);

		await dbContext.SaveChangesAsync();

		return movie;
	}
}
