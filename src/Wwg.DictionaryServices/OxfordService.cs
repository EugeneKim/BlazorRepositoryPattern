using System;
using System.Threading.Tasks;

namespace Wwg.DictionaryServices
{
	public class OxfordService : IDictionaryService
	{
#pragma warning disable CS1998
		public async Task<WordResult> FindAsync(string word) => throw new NotImplementedException();
#pragma warning restore CS1998
	}
}
