using System.Threading.Tasks;
using Xunit;
using Ds = Wwg.DictionaryServices.DictionaryService;
using Dst = Wwg.DictionaryServices.DictionaryServiceType;

namespace Wwg.DictionaryServices.Test
{
	public class DictionaryServicesTest
	{
		[Fact]
		public async Task FindWordExistingInGoogleDictionaryTest()
		{
			var wordResult = await Ds.Create(Dst.GoogleDictionary).FindAsync("walk");

			Assert.True(wordResult.IsFound);
			Assert.Equal("walk", wordResult.Word);
		}

		[Fact]
		public async Task FindWordNonExistingInGoogleDictionaryTest()
		{
			var wordResult = await Ds.Create(Dst.GoogleDictionary).FindAsync("walk2");

			Assert.False(wordResult.IsFound);
		}
	}
}
