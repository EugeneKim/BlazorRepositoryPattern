using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Wwg.Core.Entities;

namespace Wwg.DictionaryServices
{
	/// <summary>
	/// Google dictionary.
	/// </summary>
	/// <remarks>
	/// Use the following service to use Google dictionaty service.
	/// https://dictionaryapi.dev/
	/// </remarks>
	public class GoogleService : IDictionaryService
	{
		private static readonly HttpClient client = new HttpClient();
		private static readonly string baseUrl = "https://api.dictionaryapi.dev/api/v2/entries/en/";

		public async Task<WordResult> FindAsync(string word)
		{
			if (string.IsNullOrEmpty(word) || string.IsNullOrWhiteSpace(word))
				return new WordResult(word, null);

			client.DefaultRequestHeaders.Accept.Clear();

			try
			{
				string w = null;
				List<Meaning> meanings = null;

				foreach(var r in JsonSerializer.Deserialize<ResultPoco[]>(await client.GetStringAsync($"{baseUrl}{word}")))
				{
					if (string.IsNullOrEmpty(w))
						w = r.Word;

					meanings ??= new List<Meaning>();

					foreach (var m in r.Meanings)
						meanings.Add(new Meaning(ConvertTo(m.PartOfSpeech), m.Definitions.Select(d => new Definition(d.Definition, d.Example, d.Synonyms ?? new List<string>(), d.Antonyms ?? new List<string>())).ToList()));
				}

				return new WordResult(word, meanings);
			}
			catch(HttpRequestException ex)
			{
				// HTTP Cde 404 returned if not found.
				if (HttpStatusCode.NotFound == ex.StatusCode)
					return new WordResult(word, null);

				throw;
			}
		}

		private static PartOfSpeech ConvertTo(string partOfSpeechAsString) =>
			partOfSpeechAsString switch
			{
				"abbreviation" => PartOfSpeech.Abbreviation,
				"adjective" => PartOfSpeech.Adjective,
				"exclamation" => PartOfSpeech.Exclamation,
				"intransitive verb" => PartOfSpeech.Verb,
				"verb" => PartOfSpeech.Verb,
				"plural noun" => PartOfSpeech.Noun,
				"noun" => PartOfSpeech.Noun,
				"transitive verb" => PartOfSpeech.Verb,
				_ => throw new ArgumentException("unknown argument.", partOfSpeechAsString)
			};

		private class ResultPoco
		{
			[JsonPropertyName("word")]
			public string Word { get; set; }
			[JsonPropertyName("phonetics")]
			public List<PhoneticPoco> Phonetics {get; set; }
			[JsonPropertyName("meanings")]
			public List<MeaningPoco> Meanings { get; set; }
		}

		private class PhoneticPoco
		{
			[JsonPropertyName("text")]
			public string Text { get; set; }
			[JsonPropertyName("audio")]
			public string Audio { get; set; }
		}

		private class MeaningPoco
		{
			[JsonPropertyName("partOfSpeech")]
			public string PartOfSpeech { get; set; }
			[JsonPropertyName("definitions")]
			public List<DefinitionPoco> Definitions { get; set; }
		}

		private class DefinitionPoco
		{
			[JsonPropertyName("definition")]
			public string Definition { get; set; }
			[JsonPropertyName("example")]
			public string Example { get; set; }
			[JsonPropertyName("synonyms")]
			public List<string> Synonyms { get; set; }
			[JsonPropertyName("antonyms")]
			public List<string> Antonyms { get; set; }
		}
	}
}
