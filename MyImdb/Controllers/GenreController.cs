using Api.Genres;
using Microsoft.AspNetCore.Mvc;
using MyImdb.Business.Repositories;
using MyImdb.Business.Services;
using MyImdb.Models;

namespace MyImdb.Controllers {
	[ApiController]
	[Route("api/genres")]
	public class GenreController {
		private readonly GenreService genreService;
		private readonly GenreRepository genreRepository;
		private readonly ModelConverter modelConverter;

		public GenreController(
			GenreService genreService,
			GenreRepository genreRepository,
			ModelConverter modelConverter
		) {
			this.genreService = genreService;
			this.genreRepository = genreRepository;
			this.modelConverter = modelConverter;
		}

		[HttpGet("{id:guid}")]
		public async Task<GenreModel> Get(Guid id) {
			var genre = await genreRepository.SelectById(id);

			return modelConverter.ToModel(genre);
		}

		[HttpGet]
		public async Task<List<GenreModel>> List(int n = 20) {
			var genres = await genreRepository.SelectTopN(n);

			return genres.ConvertAll(modelConverter.ToModel);
		}

		[HttpPost]
		public async Task<GenreModel> Create(GenreData request) {
			var genre = await genreService.Create(request.Name);

			return modelConverter.ToModel(genre);
		}

		[HttpPut("{id:guid}")]
		public async Task<GenreModel> Update(Guid id, GenreData request) {
			var genre = await genreRepository.SelectById(id);

			await genreService.Update(genre, request.Name);

			return modelConverter.ToModel(genre);
		}

		[HttpDelete("{id:guid}")]
		public async Task Delete(Guid id) {
			var genre = await genreRepository.SelectById(id);

			await genreService.Delete(genre);
		}
	}
}
