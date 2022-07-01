namespace MyImdb.Models {
	public class MovieActorModel {
		public string Character { get; set; } = default!;

		public Guid MovieId { get; set; }

		public Guid ActorId { get; set; }
	}
}
