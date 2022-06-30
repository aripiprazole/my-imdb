using System.ComponentModel.DataAnnotations;

namespace MyImdb.Entities {
	public class Actor {
		public Guid Id { get; set; }

		[MaxLength(100)]
		[Required]
		public string Name { get; set; } = default!;

		[MaxLength(100)]
		[Required]
		public string Birthplace { get; set; } = default!;

		public List<MovieActor> MovieActors { get; set; } = default!;
	}

	public class MovieActor {
		public Guid Id { get; set; }

		[MaxLength(100)]
		[Required]
		public string Character { get; set; } = default!;

		[Required]
		public Guid MovieId { get; set; }

		public Movie Movie { get; set; } = default!;

		[Required]
		public Guid ActorId { get; set; }

		public Actor Actor { get; set; } = default!;
	}
}
