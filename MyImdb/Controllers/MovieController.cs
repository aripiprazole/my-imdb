using Api.Genres;
using Microsoft.AspNetCore.Mvc;
using MyImdb.Business.Services;
using MyImdb.Models;

namespace MyImdb.Controllers;

[ApiController]
[Route("api/movies")]
public class MovieController {
	private readonly ModelConverter modelConverter;
	private readonly MovieService movieService;

	public MovieController(ModelConverter modelConverter) {
		movieService = movieService;
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
			request.Rank,
			request.Title,
			request.Year,
			request.StoryLine,
			request.GenreId
		);

		return modelConverter.ToModel(movie);
	}

	[HttpPut("{id:guid}")]
	public async Task<MovieModel> Update(Guid id, MovieData request) {
		var movie = await movieService.SelectByIdAsync(id);

		await movieService.UpdateAsync(
			movie,
			request.Rank,
			request.Title,
			request.Year,
			request.StoryLine,
			request.GenreId
		);

		return modelConverter.ToModel(movie);
	}

	[HttpPut("{id:guid}")]
	public async Task Delete(Guid id) {
		var movie = await movieService.SelectByIdAsync(id);

		await movieService.DeleteAsync(movie);
	}
}
