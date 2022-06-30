namespace MyImdb.Models {
	public class ActorModel {
		public Guid Id { get; set; }

		public string Name { get; set; } = default!;

		public string Birthplace { get; set; } = default!;
	}
}
