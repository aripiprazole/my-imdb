namespace MyImdb.Models;

public class Movie {
	public int Id { get; set; }
	public int Rank { get; set; }
	public string Title { get; set; }
	public int Year { get; set; }
	public string StoryLine { get; set; }

	#region RETRIEVE

	public static List<Movie> SelectAll() {
		var movies = new List<Movie> {
			new() {
				Id = 1,
				Rank = 9,
				Title = "The Shawshank Redemption",
				Year = 1994,
				StoryLine = "Two imprisoned men bond over a number of years..."
			},
			new() {
				Id = 2,
				Rank = 9,
				Title = "The Godfather",
				Year = 1972,
				StoryLine = "The aging patriarch of an organized crime dynasty..."
			},
			new() {
				Id = 3,
				Rank = 9,
				Title = "The Godfather - Part II",
				Year = 1974,
				StoryLine = "The early life and career of Vito Corleone in 1920s..."
			},
			new() {
				Id = 4,
				Rank = 9,
				Title = "The Dark Knight",
				Year = 2008,
				StoryLine = "When the menace known as the Joker wreaks havoc..."
			}
		};

		return movies;
	}

	#endregion
}
