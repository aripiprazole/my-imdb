using Microsoft.AspNetCore.Mvc;
using MyImdb.Business.Services;
using MyImdb.Models;

namespace MyImdb.Controllers;

[ApiController]
[Route("api/genres")]
public class GenreController {
	private readonly GenreService genreService;
	private readonly ModelConverter modelConverter;

	public GenreController(GenreService genreService, ModelConverter modelConverter) {
		this.genreService = genreService;
		this.modelConverter = modelConverter;
	}

	[HttpGet("{id:guid}")]
	public async Task<GenreModel> Get(Guid id) {
		var genre = await genreService.GetByIdAsync(id);

		return modelConverter.ToModel(genre);
	}
}
