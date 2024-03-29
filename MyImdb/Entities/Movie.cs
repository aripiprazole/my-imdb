﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyImdb.Entities {
	public class Movie {
		public Guid Id { get; set; }

		public int Rank { get; set; }

		[MaxLength(100)]
		[Required]
		public string Title { get; set; } = default!;

		public int Year { get; set; }

		[MaxLength(200)]
		public string StoryLine { get; set; } = default!;

		private DateTime creationDateUtc { get; set; }

		[NotMapped]
		public DateTimeOffset CreationDate {
			get => new(creationDateUtc, TimeSpan.Zero);
			set => creationDateUtc = value.UtcDateTime;
		}

		public Guid GenreId { get; set; }

		public Genre Genre { get; set; } = default!;

		public List<MovieActor> MovieActors { get; set; } = default!;
	}
}
