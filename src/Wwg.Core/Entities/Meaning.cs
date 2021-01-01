
using System.Collections.Generic;

namespace Wwg.Core.Entities
{
	public class Meaning : BaseEntity
	{
		public PartOfSpeech PartOfSpeech { get; set; }
		public List<Definition> Definitions { get; set; }
	}
}
