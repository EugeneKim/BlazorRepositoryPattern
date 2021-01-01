namespace Wwg.Core.Entities
{
	public class Synonym : BaseEntity
	{
		public string Word { get; set; }

		private Synonym()
		{
		}

		public Synonym(string word) => Word = word;
	}
}
