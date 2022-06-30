namespace MyImdb.ViewModels {
	public class ErrorViewModel {
		public string? RequestId { get; set; }

		public bool ShowRequestId {
			get => !string.IsNullOrEmpty(RequestId);
		}
	}
}
