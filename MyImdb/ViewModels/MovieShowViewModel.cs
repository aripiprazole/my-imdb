namespace MyImdb.ViewModels; 

public class MovieShowViewModel {
	public int Rank { get; set; }
	public string Title { get; set; } = default!;
	public int Year { get; set; }
	public string StoryLine { get; set; } = default!;
}
