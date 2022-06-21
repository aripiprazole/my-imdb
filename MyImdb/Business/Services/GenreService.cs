using Api;
using MyImdb.Business.Repositories;
using MyImdb.Entities;

namespace MyImdb.Business.Services;

public class GenreService {
	private readonly GenreRepository genreRepository;
	private readonly AppDbContext dbContext;

	public GenreService(GenreRepository genreRepository, AppDbContext dbContext) {
		this.genreRepository = genreRepository;
		this.dbContext = dbContext;
	}

	public async Task<Genre> GetByIdAsync(Guid id) {
		return await genreRepository.SelectIdAsync(id)
		       ?? throw ApiException.Builder().Build(ErrorCode.GenreNotFound);
	}
}
