using EgBlazorComponents.Args;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;
using WordsManager.Shared;
using Wwg.Services;
using Wwg.Services.Models;

namespace WordsManager.Pages
{
	public partial class Words : ComponentBase
	{
		private const int pageSize = 20;
		private int page = 0;
		private int totalWords = 0;
		private string filter;
		private IReadOnlyList<WordModel> words;

		[Inject] IJSRuntime JSRuntime { get; set; }
		[Inject] private WordService wordService { get; set; }
		[Inject] NavigationManager NavigationManager { get; set; }

		public async Task OnReadAsync(TableReadEventArgs args)
		{
			GetPagedAll(args.Page, args.Filter);
			page = args.Page;
			await Task.CompletedTask;
		}

		protected override async Task OnInitializedAsync()
		{
			GetPagedAll(page, filter);
			await Task.CompletedTask;
		}

		private void GetPagedAll(int page, string filter)
		{
			words = wordService.GetPagedAll(page, pageSize, filter);
		}

		private async Task OnDeleteCommandAsync(TableCommandButtonArgs args)
		{
			if (await JSRuntime.ConfirmAsync("Are you sure?") && wordService.Remove((args.Data as WordModel).Name))
				GetPagedAll(page, filter);
		}

		private void OnEditCommand(TableCommandButtonArgs args) =>
			NavigationManager.NavigateTo($"editword/{(args.Data as WordModel).Name}");
	}
}
