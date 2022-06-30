using MyImdb.Entities;
using MyImdb.Models;

namespace MyImdb.Business.Services;

public class ModelConverter {
	public GenreModel ToModel(Genre genre) {
		return new GenreModel {
			Id = genre.Id,
			Name = genre.Name,
		};
	}

	public MovieModel ToModel(Movie movie) {
		return new MovieModel {
			Id = movie.Id,
			Title = movie.Title,
			Rank = movie.Rank,
			Year = movie.Year,
			Genre = ToModel(movie.Genre),
		};
	}

	public ActorModel ToModel(Actor actor) {
		return new ActorModel {
			Id = actor.Id,
			Name = actor.Name,
			Birthplace = actor.Birthplace,
		};
	}
}
