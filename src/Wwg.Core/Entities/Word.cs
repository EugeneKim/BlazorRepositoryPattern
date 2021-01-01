using System.Collections.Generic;

namespace Wwg.Core.Entities
{
	public class Word : BaseEntity
	{
		public string Name { get; set; }
		public Level Level { get; set; }
		public List<Meaning> Meanings {get; set; }
	}
}
