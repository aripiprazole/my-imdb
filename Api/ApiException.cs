namespace Api;

public class ApiException {
	public ErrorModel Error { get; }

	public ApiException(ErrorModel error) {
		Error = error;
	}

	public static ExceptionBuilder Builder() {
		return new ExceptionBuilder();
	}
}
