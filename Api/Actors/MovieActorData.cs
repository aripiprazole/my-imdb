namespace Api.Actors {
	public class MovieActorData {
		public Guid MovieId { get; set; }

		public Guid ActorId { get; set; }

		public string Character { get; set; } = default!;
	}
}
