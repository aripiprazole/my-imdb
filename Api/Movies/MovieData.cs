using System.ComponentModel.DataAnnotations;

namespace Api.Movies;

public class MovieData {
	[Required(ErrorMessage = "Rank is required")]
	[Range(1, 10, ErrorMessage = "Rank should be in between {1} and {2}")]
	public int Rank { get; set; }

	[Required(ErrorMessage = "Title of the movie is required")]
	[MaxLength(100, ErrorMessage = "Title can't be greater than {1} characters")]
	public string Title { get; set; } = default!;

	[Required(ErrorMessage = "Release year is required")]
	[Range(1900, 2020, ErrorMessage = "Year of release should be in between {1} and {2}")]
	public int Year { get; set; }

	[Required(ErrorMessage = "The storyline of the movie is required")]
	[MaxLength(200, ErrorMessage = "Storyline can't be greater than {1} characters")]
	public string StoryLine { get; set; } = default!;

	[Required(ErrorMessage = "Genre of movie is required")]
	public Guid GenreId { get; set; } = default!;
}
