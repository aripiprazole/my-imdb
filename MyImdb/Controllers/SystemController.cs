using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace MyImdb.Controllers {
	[ApiController]
	public class SystemController : Controller {
		[HttpGet("/api/system")]
		public object GetSystemInfo() {
			var assembly = Assembly.GetEntryAssembly()!;
			var name = assembly.GetName();

			var version = name.Version!;

			return new { version = $"{version.Major}.{version.Minor}.{version.Build}" };
		}
	}
}
