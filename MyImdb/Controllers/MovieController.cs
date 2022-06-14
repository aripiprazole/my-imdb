using Microsoft.AspNetCore.Mvc;
using MyImdb.ViewModels;

namespace MyImdb.Controllers;

public class MovieController : Controller {
	// GET
	public IActionResult Index() {
		return RedirectToPage("/");
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
