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

		public ActorController(
			ModelConverter modelConverter,
			ActorService actorService,
			ActorRepository actorRepository,
			MovieActorRepository movieActorRepository
		) {
			this.modelConverter = modelConverter;
			this.actorService = actorService;
			this.actorRepository = actorRepository;
			this.movieActorRepository = movieActorRepository;
		}

		[HttpGet("{id:guid}")]
		public async Task<ActorModel> Get(Guid id) {
			var actor = await actorRepository.SelectByIdAsync(id);

			return modelConverter.ToModel(actor);
		}

		[HttpGet]
		public async Task<List<ActorModel>> List(int n = 20) {
			var actors = await actorRepository.SelectTopNAsync(n);

			return actors.ConvertAll(modelConverter.ToModel);
		}

		[HttpGet("{id:guid}/characters")]
		public async Task<List<MovieActorModel>> ListCharacters(Guid id, int n = 20) {
			var movies = await movieActorRepository.SelectByActorIdAsync(id, n);

			return movies.ConvertAll(modelConverter.ToModel);
		}

		[HttpPost]
		public async Task<ActorModel> Create(ActorData request) {
			var actor = await actorService.CreateAsync(request.Name, request.Birthplace);

			return modelConverter.ToModel(actor);
		}

		[HttpPut("{id:guid}")]
		public async Task<ActorModel> Update(Guid id, ActorData request) {
			var actor = await actorRepository.SelectByIdAsync(id);

			await actorService.UpdateAsync(actor, request.Name, request.Birthplace);

			return modelConverter.ToModel(actor);
		}

		[HttpDelete("{id:guid}")]
		public async Task Delete(Guid id) {
			var actor = await actorRepository.SelectByIdAsync(id);

			await actorService.DeleteAsync(actor);
		}
	}
}
