namespace Api {
	public class ExceptionBuilder {
		public ApiException Build(ErrorCodes code, object? details = null) {
			var message = code switch {
				ErrorCodes.Unknown => "An unknown error occurred",
				ErrorCodes.GenreNotFound => "Genre not found",
				ErrorCodes.GenreAlreadyExists => "Genre already exists",
				ErrorCodes.MovieNotFound => "Movie not found",
				ErrorCodes.MovieAlreadyExists => "Movie already exists",
				ErrorCodes.ActorNotFound => "Actor not found",
				_ => "",
			};

			return new ApiException(
				new ErrorModel() {
					Code = code,
					Message = message,
					Details = details == null ? new Dictionary<string, string>() : getDetailsDictionary(details),
				}
			);
		}

		private static Dictionary<string, string> getDetailsDictionary(object details) {
			var dictionary = new Dictionary<string, string>();

			foreach (var property in details.GetType().GetProperties()) {
				dictionary[property.Name] = property.GetValue(details, null)?.ToString() ?? "null";
			}

			return dictionary;
		}
	}
}
