using Microsoft.AspNetCore.Mvc;
using MyImdb.Models;
using MyImdb.ViewModels;

namespace MyImdb.Controllers;

public class MovieController : Controller {
	// GET
	public IActionResult Index(string? message = null) {
		var movies = Movie
			.SelectAll()
			.ConvertAll(movie => new MovieListViewModel { Rank = movie.Rank, Title = movie.Title, Year = movie.Year, });

		ViewBag.Message = message;

		return View(movies);
	}

	// GET
	public IActionResult Create() {
		return View();
	}

	// POST
	[HttpPost]
	public IActionResult Create(CreateMovieViewModel model) {
		// Aqui usa-se nameof(Index) porque a função RedirectToAction, requer um parametro string, e usar apenas Index,
		// resultaria numa referência de função.
		return ModelState.IsValid
			? RedirectToAction(nameof(Index), new { message = "Movie created successfully!" })
			: View(model);
	}
}
