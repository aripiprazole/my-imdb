using Api.Actors;
using Microsoft.AspNetCore.Mvc;
using MyImdb.Business.Repositories;
using MyImdb.Business.Services;
using MyImdb.Models;

namespace MyImdb.Controllers {
	[ApiController]
	[Route("api/characters")]
	public class MovieActorController {
		private readonly ModelConverter modelConverter;
		private readonly MovieActorService movieActorService;
		private readonly MovieActorRepository movieActorRepository;

		public MovieActorController(
			ModelConverter modelConverter,
			MovieActorService movieActorService,
			MovieActorRepository movieActorRepository
		) {
			this.modelConverter = modelConverter;
			this.movieActorService = movieActorService;
			this.movieActorRepository = movieActorRepository;
		}

		[HttpPost]
		public async Task<MovieActorModel> Create(MovieActorData request) {
			var movieActor = await movieActorService.Create(request.MovieId, request.ActorId, request.Character);

			return modelConverter.ToModel(movieActor);
		}

		[HttpPut("{id:guid}")]
		public async Task<MovieActorModel> Update(Guid id, MovieActorData request) {
			var actor = await movieActorRepository.SelectById(id);

			await movieActorService.Update(actor, request.Character);

			return modelConverter.ToModel(actor);
		}

		[HttpDelete("{id:guid}")]
		public async Task Delete(Guid id) {
			var actor = await movieActorRepository.SelectById(id);

			await movieActorService.Delete(actor);
		}
	}
}
