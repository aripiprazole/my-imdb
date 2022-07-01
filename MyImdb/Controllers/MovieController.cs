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
		private readonly MovieActorRepository movieActorRepository;
		private readonly MovieRepository movieRepository;
		private readonly MovieService movieService;

		public MovieController(
			ModelConverter modelConverter,
			MovieService movieService,
			MovieRepository movieRepository,
			MovieActorRepository movieActorRepository
		) {
			this.modelConverter = modelConverter;
			this.movieService = movieService;
			this.movieRepository = movieRepository;
			this.movieActorRepository = movieActorRepository;
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

		[HttpGet("{id:guid}/characters")]
		public async Task<List<MovieActorModel>> ListCharacters(Guid id, int n = 20) {
			var movies = await movieActorRepository.SelectByMovieId(id, n);

			return movies.ConvertAll(modelConverter.ToModel);
		}

		[HttpPost]
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
