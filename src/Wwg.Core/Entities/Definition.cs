using System.Collections.Generic;

namespace Wwg.Core.Entities
{
	public class Definition : BaseEntity
	{
		public Language Language { get; set; }
		public string Define { get; set; }
		public string Example { get; set; }
		public List<Synonym> Synonyms { get; set; }
		public List<Antonym> Antonyms { get; set; }
	}
}
