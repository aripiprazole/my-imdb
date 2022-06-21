using MyImdb.Entities;
using MyImdb.Models;

namespace MyImdb.Business.Services;

public class ModelConverter {
	public GenreModel ToModel(Genre genre) {
		return new GenreModel { Id = genre.Id, Name = genre.Name };
	}
}
