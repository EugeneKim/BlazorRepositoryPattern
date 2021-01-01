using System.Threading.Tasks;

namespace Wwg.DictionaryServices
{
	public interface IDictionaryService
	{
		Task<WordResult> FindAsync(string word);
	}
}
