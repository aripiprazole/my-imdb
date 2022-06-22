using Api.Genres;
using Microsoft.AspNetCore.Mvc;
using MyImdb.Business.Services;
using MyImdb.Models;

namespace MyImdb.Controllers;

[ApiController]
[Route("api/movies")]
public class MovieController {
	private readonly MovieService movieService;
	private readonly ModelConverter modelConverter;

	public MovieController(ModelConverter modelConverter) {
		this.movieService = movieService;
		this.modelConverter = modelConverter;
	}

	[HttpGet("{id:guid}")]
	public async Task<MovieModel> Get(Guid id) {
		var movie = await movieService.SelectByIdAsync(id);

		return modelConverter.ToModel(movie);
	}

	[HttpGet]
	public async Task<List<MovieModel>> List(int n = 20) {
		var movies = await movieService.SelectTopNAsync(n);

		return movies.ConvertAll(modelConverter.ToModel);
	}

	public async Task<MovieModel> Create(MovieData request) {
		var movie = await movieService.CreateAsync(
			rank: request.Rank,
			title: request.Title,
			year: request.Year,
			storyLine: request.StoryLine,
			genreId: request.GenreId
		);

		return modelConverter.ToModel(movie);
	}

	[HttpPut("{id:guid}")]
	public async Task<MovieModel> Update(Guid id, MovieData request) {
		var movie = await movieService.SelectByIdAsync(id);

		await movieService.UpdateAsync(
			movie,
			rank: request.Rank,
			title: request.Title,
			year: request.Year,
			storyLine: request.StoryLine,
			genreId: request.GenreId
		);

		return modelConverter.ToModel(movie);
	}

	[HttpPut("{id:guid}")]
	public async Task Delete(Guid id) {
		var movie = await movieService.SelectByIdAsync(id);

		await movieService.DeleteAsync(movie);
	}
}
