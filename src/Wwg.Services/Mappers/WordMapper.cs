using Wwg.Core.Entities;
using Wwg.Services.Models;
using System.Collections.Generic;
using System.Linq;

namespace Wwg.Services.Mappers
{
	/// <summary>
	/// A mapping library to the DTO (<see cref="Word"/>) and the model (<see cref="WordModel"/>), and vice-versa.
	/// </summary>
	/// <remarks>
	/// AutoMapper (https://automapper.org/) can be an option to replace the manual mapping.
	/// </remarks>
	internal static class WordMapper
	{
		public static WordModel ConvertEntityToModel(Word source)
		{
			var wordModel = new WordModel
			{
				Name = source.Name,
				Level = source.Level,
				Definitions = new List<DefinitionModel>()
			};

			if (source.Meanings != null)
			{
				foreach (var meaningEntity in source.Meanings)
				{
					var definitionModel = new DefinitionModel
					{
						PartOfSpeech = meaningEntity.PartOfSpeech
					};

					if (meaningEntity.Definitions != null)
					{
						foreach (var definitionEntity in meaningEntity.Definitions.Where(d => d.Language == Language.English))
						{
							definitionModel.Define = definitionEntity.Define;
							definitionModel.Example = definitionEntity.Example;
							definitionModel.Antonyms = definitionEntity.Antonyms?.AsString();
							definitionModel.Synonyms = definitionEntity.Synonyms?.AsString();
						}
					}

					wordModel.Definitions.Add(definitionModel);
				}
			}

			return wordModel;
		}

		public static Word ConvertModelToEntity(WordModel source)
		{
			var wordEntity = new Word { Meanings = new List<Meaning>() };

			UpdateEntityWithModel(source, wordEntity);

			return wordEntity;
		}

		public static void UpdateEntityWithModel(WordModel source, Word target)
		{
			target.Name = source.Name;
			target.Level = source.Level.Value;
			target.Meanings.Clear();

			foreach (var group in source.Definitions.GroupBy(d => d.PartOfSpeech))
			{
				var meaning = new Meaning
				{
					PartOfSpeech = group.Key.Value,
					Definitions = new List<Definition>()
				};

				foreach (var value in group)
				{
					var definition = new Definition
					{
						Language = Language.English,
						Define = value.Define,
						Example = value.Example,
						Antonyms = value.Antonyms.AsAntonyms(),
						Synonyms = value.Synonyms.AsSynonyms()
					};

					meaning.Definitions.Add(definition);
				}

				target.Meanings.Add(meaning);
			}
		}

		public static List<Antonym> AsAntonyms(this string value) => value.Split(",").Select(a => new Antonym(a.Trim())).ToList();

		public static List<Synonym> AsSynonyms(this string value) => value.Split(",").Select(a => new Synonym(a.Trim())).ToList();

		public static string AsString(this List<Antonym> value) => string.Join(",", value.Select(v => v.Word));

		public static string AsString(this List<Synonym> value) => string.Join(",", value.Select(v => v.Word));
	}
}
