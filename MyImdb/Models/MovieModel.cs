namespace MyImdb.Models;

public class MovieModel {
	public Guid Id { get; set; }

	public int Rank { get; set; }

	public string Title { get; set; } = default!;

	public int Year { get; set; }

	public GenreModel Genre { get; set; } = default!;
}
