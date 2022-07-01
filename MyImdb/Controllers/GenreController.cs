using Api.Genres;
using Microsoft.AspNetCore.Mvc;
using MyImdb.Business.Repositories;
using MyImdb.Business.Services;
using MyImdb.Models;

namespace MyImdb.Controllers {
	[ApiController]
	[Route("api/genres")]
	public class GenreController {
		private readonly GenreRepository genreRepository;
		private readonly GenreService genreService;
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
			var genre = await genreRepository.SelectByIdAsync(id);

			return modelConverter.ToModel(genre);
		}

		[HttpGet]
		public async Task<List<GenreModel>> List(int n = 20) {
			var genres = await genreRepository.SelectTopNAsync(n);

			return genres.ConvertAll(modelConverter.ToModel);
		}

		[HttpPost]
		public async Task<GenreModel> Create(GenreData request) {
			var genre = await genreService.CreateAsync(request.Name);

			return modelConverter.ToModel(genre);
		}

		[HttpPut("{id:guid}")]
		public async Task<GenreModel> Update(Guid id, GenreData request) {
			var genre = await genreRepository.SelectByIdAsync(id);

			await genreService.UpdateAsync(genre, request.Name);

			return modelConverter.ToModel(genre);
		}

		[HttpDelete("{id:guid}")]
		public async Task Delete(Guid id) {
			var genre = await genreRepository.SelectByIdAsync(id);

			await genreService.DeleteAsync(genre);
		}
	}
}
