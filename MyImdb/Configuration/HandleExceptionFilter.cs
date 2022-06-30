using System.Net;
using System.Security.Cryptography;
using Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace MyImdb.Configuration;

public class HandleExceptionFilter : IExceptionFilter {
	private readonly ILogger<HandleExceptionFilter> logger;

	public HandleExceptionFilter(ILogger<HandleExceptionFilter> logger) {
		this.logger = logger;
	}

	public void OnException(ExceptionContext context) {
		if (context.Exception is ApiException apiException) {
			logger.LogWarning(apiException, "API Exception caught");

			context.Result = new JsonResult(apiException.Error, JsonConvert.DefaultSettings) {
				StatusCode = (int)HttpStatusCode.UnprocessableEntity,
			};
			context.ExceptionHandled = true;
		} else {
			var rawCode = new byte[3];
			using (var rng = RandomNumberGenerator.Create()) {
				rng.GetBytes(rawCode);
			}

			var code = BitConverter.ToString(rawCode).Replace("-", "");

			logger.LogError(context.Exception, "Unhandled exception --- {}", code);

			var error = new {
				Message = $"An unexpected exception occurred. Please contact the support and provide the code {code}",
				ExceptionCode = code,
			};

			context.Result = new JsonResult(error, JsonConvert.DefaultSettings) {
				StatusCode = (int)HttpStatusCode.InternalServerError,
			};
		}
	}
}
