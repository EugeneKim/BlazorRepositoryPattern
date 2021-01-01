using System;

namespace Wwg.DictionaryServices
{
	/// <summary>
	/// A factory to provide the dictionary services.
	/// </summary>
	public static class DictionaryService
	{
		/// <summary>
		/// Returns the dictionary service instance.
		/// </summary>
		/// <param name="type">Type of the service provider.</param>
		/// <returns>Dictinary service instance.</returns>
		public static IDictionaryService Create(DictionaryServiceType type) =>
			type switch
			{
				DictionaryServiceType.GoogleDictionary => new GoogleService(),
				DictionaryServiceType.MacmillanDictionary => new MacmillanService(),
				DictionaryServiceType.OxfordEnglishDictionary => new OxfordService(),
				_ => throw new ArgumentException("invalid argument.", nameof(type))
			};
	}
}
