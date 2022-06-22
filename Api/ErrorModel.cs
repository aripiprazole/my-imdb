using Newtonsoft.Json;

namespace Api;

public class ErrorModel {
	[JsonProperty("code")]
	private string codeStr { get; set; } = default!;

	[JsonIgnore]
	public ErrorCode Code {
		get {
			ErrorCode code;
			try {
				code = Enum.Parse<ErrorCode>(codeStr);
			} catch {
				code = ErrorCode.Unknown;
			}

			return code;
		}
		set => codeStr = value.ToString();
	}

	public string Message { get; set; } = default!;

	public Dictionary<string, string> Details { get; set; } = default!;

	public override string ToString() {
		return "ErrorModel{" +
		       "Code='" + Code + "'" +
		       ", Message='" + Message + "'" +
		       ", Details=" + Details +
		       '}';
	}
}
