using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace WordsManager.Shared
{
	public static class JSRuntimeHelper
	{
		public static async Task<bool> ConfirmAsync(this IJSRuntime jsRuntime, string message)
			=> await jsRuntime.InvokeAsync<bool>("confirm", message);

		public static async Task AlertAsync(this IJSRuntime jsRuntime, string message)
			=> await jsRuntime.InvokeVoidAsync("alert", message);
	}
}
