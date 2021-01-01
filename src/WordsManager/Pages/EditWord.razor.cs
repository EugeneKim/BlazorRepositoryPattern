using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Threading.Tasks;
using Wwg.DictionaryServices;
using Wwg.Services;
using Wwg.Services.Models;
using EgBlazorComponents.Spinner;
using Microsoft.JSInterop;
using WordsManager.Shared;

namespace WordsManager.Pages
{
	public partial class EditWord : ComponentBase
	{
		[Inject] IJSRuntime JSRuntime { get; set; }
		[Inject] private IDictionaryService dictionaryService { get; set; }
		[Inject] private WordService wordService { get; set; }
		[Inject] NavigationManager NavigationManager { get; set; }

		[Parameter] public string Word { get; set; }

		private EgSpinner spinner;

		private bool updateMode => !string.IsNullOrEmpty(Word);
		private WordModel wordModel;

		protected override void OnInitialized()
		{
			wordModel = new WordModel();

			wordModel = updateMode ? wordService.GetWord(Word) : new WordModel();

			base.OnInitialized();
		}
		public async Task OnSubmitAsync(EditContext context)
		{
			if (!context.Validate())
				return;

			if (updateMode)
				wordService.UpdateWord((WordModel)context.Model);
			else
				wordService.AddWord((WordModel)context.Model);

			if (updateMode)
			{
				await JSRuntime.AlertAsync("Successfully updated.");
			}
			else
			{
				if (await JSRuntime.ConfirmAsync("Successfully added. Do you want to add more?"))
				{
					NavigationManager.NavigateTo("editword", true);
					return;
				}
			}

			NavigationManager.NavigateTo("words", true);
		}

		private async Task OnSearchWordAsync(string wordToSearch)
		{
			await spinner.ShowAsync(async () =>
			{
				var wordResult = await dictionaryService.FindAsync(wordToSearch);

				if (wordResult.IsFound)
				{
					wordModel.Name = wordResult.Word;

					wordModel.Definitions.Clear();

					foreach (var meaning in wordResult.Meanings)
					{
						var definitionModel = new DefinitionModel { PartOfSpeech = meaning.PartOfSpeech };

						foreach (var definition in meaning.Definitions)
						{
							definitionModel.Antonyms = string.Join(",", definition.Antonyms);
							definitionModel.Synonyms = string.Join(",", definition.Synonyms);
							definitionModel.Define = definition.Define;
							definitionModel.Example = definition.Example;
						}

						wordModel.Definitions.Add(definitionModel);
					}
				}
			});
		}

		private void OnAddDefinition() => wordModel.Definitions.Add(new DefinitionModel());

		private void OnDeleteDefinition(DefinitionModel definition) => wordModel.Definitions.Remove(definition);
	}
}
