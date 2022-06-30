using Api.Movies;
using Microsoft.AspNetCore.Mvc;
using MyImdb.Business.Repositories;
using MyImdb.Business.Services;
using MyImdb.Models;

namespace MyImdb.Controllers {
	[ApiController]
	[Route("api/movies")]
	public class MovieController {
		private readonly ModelConverter modelConverter;
		private readonly MovieService movieService;
		private readonly MovieRepository movieRepository;
		private readonly MovieActorService movieActorService;
		private readonly MovieActorRepository movieActorRepository;
		private readonly ActorRepository actorRepository;

		public MovieController(
			ModelConverter modelConverter,
			MovieService movieService,
			MovieRepository movieRepository,
			MovieActorService movieActorService,
			MovieActorRepository movieActorRepository,
			ActorRepository actorRepository
		) {
			this.modelConverter = modelConverter;
			this.movieService = movieService;
			this.movieRepository = movieRepository;
			this.movieActorService = movieActorService;
			this.movieActorRepository = movieActorRepository;
			this.actorRepository = actorRepository;
		}

		[HttpGet("{id:guid}")]
		public async Task<MovieModel> Get(Guid id) {
			var movie = await movieRepository.SelectById(id);

			return modelConverter.ToModel(movie);
		}

		[HttpGet]
		public async Task<List<MovieModel>> List(int n = 20) {
			var movies = await movieRepository.SelectTopN(n);

			return movies.ConvertAll(modelConverter.ToModel);
		}

		[HttpGet("{id:guid}/actors")]
		public async Task<List<ActorModel>> ListActors(Guid id, int n = 20) {
			var actors = await movieActorRepository.SelectActorsByMovieId(id, n);

			return actors.ConvertAll(modelConverter.ToModel);
		}

		[HttpPost("{id:guid}/actors")]
		public async Task LinkMovie(Guid id, LinkActorAndMovieData request) {
			var movie = await movieRepository.SelectById(id);
			var actor = await actorRepository.SelectById(request.TargetActorId);

			await movieActorService.LinkMovieToActor(movie.Id, actor.Id);
		}

		[HttpDelete("{id:guid}/actors")]
		public async Task UnlinkMovie(Guid id, LinkActorAndMovieData request) {
			var movie = await movieRepository.SelectById(id);
			var actor = await actorRepository.SelectById(request.TargetActorId);

			await movieActorService.UnlinkMovieFromActor(movie.Id, actor.Id);
		}

		public async Task<MovieModel> Create(MovieData request) {
			var movie = await movieService.Create(
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
			var movie = await movieRepository.SelectById(id);

			await movieService.Update(
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
			var movie = await movieRepository.SelectById(id);

			await movieService.Delete(movie);
		}
	}
}
