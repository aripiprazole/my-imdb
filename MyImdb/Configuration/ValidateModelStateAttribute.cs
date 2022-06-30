using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MyImdb.Configuration;

public class ValidateModelStateAttribute : ActionFilterAttribute {
	private static readonly DefaultContractResolver sharedContractResolver = new() {
		NamingStrategy = new CamelCaseNamingStrategy {
			ProcessDictionaryKeys = true,
		},
	};

	private static readonly JsonSerializerSettings serializerSettings;

	static ValidateModelStateAttribute() {
		serializerSettings = new JsonSerializerSettings {
			ContractResolver = sharedContractResolver,
		};
	}

	public override void OnActionExecuting(ActionExecutingContext context) {
		if (context.ModelState.IsValid) {
			return;
		}

		context.Result = new JsonResult(new SerializableError(context.ModelState), serializerSettings) {
			StatusCode = (int)HttpStatusCode.BadRequest,
		};
	}
}
