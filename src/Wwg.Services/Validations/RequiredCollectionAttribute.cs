using System;
using System.Collections;
using System.ComponentModel.DataAnnotations;

namespace Wwg.Services.Validations
{
	public sealed class RequiredCollectionAttribute : ValidationAttribute
	{
		public int MinCount { get; set; }

		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			if (MinCount < 1)
				throw new ArgumentOutOfRangeException($"{validationContext.MemberName} should be larger than 0.");

			var e = value as IList;

			if (e == null)
				throw new NotSupportedException($"Failed to support type: {value.GetType()}");

			return e.Count >= MinCount
				? ValidationResult.Success
				: new ValidationResult(ErrorMessage ?? $"At least {(MinCount == 1 ? "one entry" : MinCount + " entries")} required.", new[] { validationContext.MemberName });
		}
	}
}
