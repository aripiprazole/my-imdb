using System.ComponentModel.DataAnnotations;

namespace MyImdb.Entities; 

public class Genre {
	public Guid Id { get; set; }

	[MaxLength(100)]
	[Required] //
	public string Name { get; set; } = default!;

	public List<Movie> Movies { get; set; } = default!;
}
