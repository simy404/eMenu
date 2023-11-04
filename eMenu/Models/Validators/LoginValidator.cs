using eMenu.Models.ViewModels;
using FluentValidation;

namespace eMenu.Models.Validators
{
	public class LoginValidator : AbstractValidator<LoginViewModel>
	{
		public LoginValidator() 
		{
			RuleFor(user => user.Email)
				.NotEmpty().WithMessage("Your email cannot be empty...")
				.EmailAddress().WithMessage("Please enter a value in email format...");

			RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty")
				.MinimumLength(8).WithMessage("Your password length must be at least 8.")
				.MaximumLength(16).WithMessage("Your password length must not exceed 16.");	
		}
	}
}
