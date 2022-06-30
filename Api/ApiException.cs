﻿namespace Api {
	public class ApiException : Exception {
		public ApiException(ErrorModel error) {
			Error = error;
		}

		public ErrorModel Error { get; }

		public static ExceptionBuilder Builder() {
			return new ExceptionBuilder();
		}
	}
}
