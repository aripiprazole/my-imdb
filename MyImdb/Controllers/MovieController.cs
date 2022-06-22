using Api.Movies;
using Microsoft.AspNetCore.Mvc;
using MyImdb.Business.Services;
using MyImdb.Models;

namespace MyImdb.Controllers;

[ApiController]
[Route("api/movies")]
public class MovieController {
	private readonly ModelConverter modelConverter;
	private readonly MovieActorService movieActorService;
	private readonly MovieService movieService;

	public MovieController(
		MovieService movieService,
		MovieActorService movieActorService,
		ModelConverter modelConverter
	) {
		this.movieService = movieService;
		this.movieActorService = movieActorService;
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

	[HttpGet("{id:guid}/actors")]
	public async Task<List<ActorModel>> ListActors(Guid id, int n = 20) {
		var actors = await movieActorService.SelectActorsByMovieIdAsync(id, n);

		return actors.ConvertAll(modelConverter.ToModel);
	}

	[HttpPost("{id:guid}/actors")]
	public async Task LinkMovie(Guid id, LinkActorAndMovieData request) {
		await movieActorService.LinkMovieToActor(id, request.TargetActorId);
	}

	[HttpDelete("{id:guid}/actors")]
	public async Task UnlinkMovie(Guid id, LinkActorAndMovieData request) {
		await movieActorService.UnlinkMovieFromActor(id, request.TargetActorId);
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

	[HttpDelete("{id:guid}")]
	public async Task Delete(Guid id) {
		var movie = await movieService.SelectByIdAsync(id);

		await movieService.DeleteAsync(movie);
	}
}
