using Microsoft.AspNetCore.Mvc;
using MyImdb.Models;
using MyImdb.ViewModels;

namespace MyImdb.Controllers;

public class MovieController : Controller {
	// GET
	public IActionResult Index() {
		var movies = Movie
			.SelectAll()
			.ConvertAll(movie => new MovieListViewModel { Rank = movie.Rank, Title = movie.Title, Year = movie.Year, });

		return View(movies);
	}

	// GET
	public IActionResult Create() {
		return View();
	}

	// POST
	[HttpPost]
	public IActionResult Create(CreateMovieViewModel model) {
		return ModelState.IsValid ? View(model) : View();
	}
}
