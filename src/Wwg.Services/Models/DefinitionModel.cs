using System.ComponentModel.DataAnnotations;
using Wwg.Core.Entities;
using Wwg.Services.Validations;

namespace Wwg.Services.Models
{
	public class DefinitionModel
	{
		[Required]
		public PartOfSpeech? PartOfSpeech { get; set; }
		[Required]
		public string Define { get; set; }
		[Required]
		public string Example { get; set; }
		[CommaSeparatedFormat]
		public string Synonyms { get; set; }
		[CommaSeparatedFormat]
		public string Antonyms { get; set; }
	}
}
