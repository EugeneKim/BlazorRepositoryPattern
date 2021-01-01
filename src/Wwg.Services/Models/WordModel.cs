using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Wwg.Core.Entities;
using Wwg.Services.Validations;

namespace Wwg.Services.Models
{
	public class WordModel
	{
		[Required]
		public string Name { get; set; }

		[Required]
		public Level? Level { get; set; }

		[ValidateComplexType]
		[RequiredCollection(MinCount = 1)]
		public List<DefinitionModel> Definitions { get; set; } = new List<DefinitionModel>();
	}
}
