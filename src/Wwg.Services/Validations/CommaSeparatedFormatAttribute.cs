using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Wwg.Services.Validations
{
	public sealed class CommaSeparatedFormatAttribute : ValidationAttribute
	{
		protected override ValidationResult IsValid(object value, ValidationContext validationContext)
		{
			// TODO Refactor with the regular expression.

			var e = value as string;

			/*
			if (string.IsNullOrEmpty(e) || string.IsNullOrWhiteSpace(e))
				return new ValidationResult(ErrorMessage ?? $"The {validationContext.MemberName} field is required.", new[] { validationContext.MemberName });

			var split = e.Split(",").Select(s => s.Trim()).ToList();

			if (split.Count < 0)
				return new ValidationResult(ErrorMessage ?? $"The {validationContext.MemberName} field is required.", new[] { validationContext.MemberName });
			*/

			if (string.IsNullOrEmpty(e))
				return ValidationResult.Success;

			var split = e.Split(",").Select(s => s.Trim()).ToList();

			return split.All(s => s.All(char.IsLetter) || s.Contains(' '))
				? ValidationResult.Success
				: new ValidationResult(string.IsNullOrEmpty(ErrorMessage) ? $"Invalid format." : ErrorMessage, new[] { validationContext.MemberName });
		}
	}
}
