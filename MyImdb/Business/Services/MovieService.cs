using Api;
using Microsoft.EntityFrameworkCore;
using MyImdb.Business.Repositories;
using MyImdb.Entities;

namespace MyImdb.Business.Services;

public class MovieService {
	private readonly AppDbContext dbContext;
	private readonly GenreRepository genreRepository;
	private readonly MovieRepository movieRepository;

	public MovieService(GenreRepository genreRepository, MovieRepository movieRepository, AppDbContext dbContext) {
		this.genreRepository = genreRepository;
		this.movieRepository = movieRepository;
		this.dbContext = dbContext;
	}

	public async Task<Movie> SelectById(Guid id) {
		return await movieRepository.SelectById(id) ??
		       throw ApiException.Builder().Build(ErrorCodes.MovieNotFound, new { id });
	}

	public async Task<List<Movie>> SelectTopN(int n = 20) {
		return await movieRepository.SelectTopN(n);
	}

	public async Task<Movie> Create(int rank, string title, int year, string storyLine, Guid genreId) {
		var genre = await genreRepository.SelectById(genreId) ??
		            throw ApiException.Builder().Build(ErrorCodes.GenreNotFound, new { id = genreId });

		var movie = await movieRepository.SelectByTitle(title);
		if (movie != null) {
			throw ApiException.Builder().Build(ErrorCodes.MovieAlreadyExists, new { title });
		}

		movie = await movieRepository.Create(rank, title, year, storyLine, genre);

		await dbContext.SaveChangesAsync();

		return movie;
	}

	public async Task Update(Movie target, int rank, string title, int year, string storyLine, Guid genreId) {
		var movieExists = await dbContext.Movies.AnyAsync(movie => movie.Title == title && movie.Id != target.Id);
		if (movieExists) {
			throw ApiException.Builder().Build(ErrorCodes.MovieAlreadyExists, new { title });
		}

		target.Rank = rank;
		target.Title = title;
		target.Year = year;
		target.StoryLine = storyLine;
		target.GenreId = genreId;

		await dbContext.SaveChangesAsync();
	}

	public async Task Delete(Movie movie) {
		dbContext.Remove(movie);

		foreach (var movieActor in movie.MovieActors) {
			dbContext.Remove(movieActor);
		}

		await dbContext.SaveChangesAsync();
	}
}
