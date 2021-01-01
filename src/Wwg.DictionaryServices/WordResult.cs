using System.Collections.Generic;
using Wwg.Core.Entities;

namespace Wwg.DictionaryServices
{
	public record WordResult
	{
		public bool IsFound { get; }
		public string Word { get; }
		public IReadOnlyList<Meaning> Meanings { get; }

		public WordResult(string word, List<Meaning> meanings) =>
			(IsFound, Word, Meanings) = (meanings != null, word, meanings);
	}

	public record Meaning
	{
		public PartOfSpeech PartOfSpeech { get; }
		public IReadOnlyList<Definition> Definitions { get; }

		public Meaning(PartOfSpeech partOfSpeech, List<Definition> definitions) =>
			(PartOfSpeech, Definitions) = (partOfSpeech, definitions);
	}

	public record Definition
	{
		public string Define { get; }
		public string Example { get; }
		public IReadOnlyList<string> Synonyms { get; }
		public IReadOnlyList<string> Antonyms { get; }

		public Definition(string define, string example, List<string> synonyms, List<string> antonyms)
			=> (Define, Example, Synonyms, Antonyms) = (define, example, synonyms, antonyms);
	}
}
