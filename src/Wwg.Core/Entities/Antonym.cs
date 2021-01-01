namespace Wwg.Core.Entities
{
	public class Antonym : BaseEntity
	{
		public string Word { get; set; }
		private Antonym()
		{

		}

		public Antonym(string word) => Word = word;
	}
}
