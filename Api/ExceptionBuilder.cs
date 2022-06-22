namespace Api;

public class ExceptionBuilder {
	public ApiException Build(ErrorCode code, object? details = null) {
		var message = code switch {
			ErrorCode.Unknown => "An unknown error occurred",
			ErrorCode.GenreNotFound => "Genre not found",
			ErrorCode.GenreAlreadyExists => "Genre already exists",
			ErrorCode.MovieNotFound => "Movie not found",
			ErrorCode.MovieAlreadyExists => "Movie already exists",
			ErrorCode.ActorNotFound => "Actor not found",
			_ => ""
		};

		return new ApiException(new ErrorModel {
			Code = code,
			Message = message,
			Details = details == null ? new Dictionary<string, string>() : getDetailsDictionary(details)
		});
	}

	private static Dictionary<string, string> getDetailsDictionary(object details) {
		var dictionary = new Dictionary<string, string>();

		foreach (var property in details.GetType().GetProperties())
			dictionary[property.Name] = property.GetValue(details, null)?.ToString() ?? "null";

		return dictionary;
	}
}
