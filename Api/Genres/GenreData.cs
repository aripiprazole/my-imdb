using System.ComponentModel.DataAnnotations;

namespace Api.Genres; 

public class GenreData {
	[Required(ErrorMessage = "The name of the genre is required")]
	[MaxLength(100, ErrorMessage = "The name can't be greater than {1} characters")]
	public string Name { get; set; } = default!;
}
