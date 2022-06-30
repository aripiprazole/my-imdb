using Api.Actors;
using Microsoft.AspNetCore.Mvc;
using MyImdb.Business.Repositories;
using MyImdb.Business.Services;
using MyImdb.Models;

namespace MyImdb.Controllers {
	[ApiController]
	[Route("api/actors")]
	public class ActorController {
		private readonly ActorRepository actorRepository;
		private readonly ActorService actorService;
		private readonly ModelConverter modelConverter;
		private readonly MovieActorRepository movieActorRepository;
		private readonly MovieActorService movieActorService;
		private readonly MovieRepository movieRepository;

		public ActorController(
			ModelConverter modelConverter,
			ActorService actorService,
			ActorRepository actorRepository,
			MovieRepository movieRepository,
			MovieActorService movieActorService,
			MovieActorRepository movieActorRepository
		) {
			this.modelConverter = modelConverter;
			this.actorService = actorService;
			this.actorRepository = actorRepository;
			this.movieRepository = movieRepository;
			this.movieActorService = movieActorService;
			this.movieActorRepository = movieActorRepository;
		}

		[HttpGet("{id:guid}")]
		public async Task<ActorModel> Get(Guid id) {
			var actor = await actorRepository.SelectById(id);

			return modelConverter.ToModel(actor);
		}

		[HttpGet]
		public async Task<List<ActorModel>> List(int n = 20) {
			var actors = await actorRepository.SelectTopN(n);

			return actors.ConvertAll(modelConverter.ToModel);
		}

		[HttpGet("{id:guid}/movies")]
		public async Task<List<MovieModel>> ListMovies(Guid id, int n = 20) {
			var movies = await movieActorRepository.SelectMoviesByActorId(id, n);

			return movies.ConvertAll(modelConverter.ToModel);
		}

		[HttpPost("{id:guid}/movies")]
		public async Task LinkMovie(Guid id, LinkMovieAndActorData request) {
			var movie = await movieRepository.SelectById(request.TargetMovieId);
			var actor = await actorRepository.SelectById(id);

			await movieActorService.LinkMovieToActor(movie.Id, actor.Id);
		}

		[HttpDelete("{id:guid}/movies")]
		public async Task UnlinkMovie(Guid id, LinkMovieAndActorData request) {
			var movie = await movieRepository.SelectById(request.TargetMovieId);
			var actor = await actorRepository.SelectById(id);

			await movieActorService.UnlinkMovieFromActor(movie.Id, actor.Id);
		}

		[HttpPost]
		public async Task<ActorModel> Create(ActorData request) {
			var actor = await actorService.Create(request.Name, request.Birthplace);

			return modelConverter.ToModel(actor);
		}

		[HttpPut("{id:guid}")]
		public async Task<ActorModel> Update(Guid id, ActorData request) {
			var actor = await actorRepository.SelectById(id);

			await actorService.Update(actor, request.Name, request.Birthplace);

			return modelConverter.ToModel(actor);
		}

		[HttpDelete("{id:guid}")]
		public async Task Delete(Guid id) {
			var actor = await actorRepository.SelectById(id);

			await actorService.Delete(actor);
		}
	}
}
