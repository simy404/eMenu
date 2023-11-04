using eMenu.Models.ViewModels;
using FluentValidation;

namespace eMenu.Models.Validators
{
	public class AppUserValidator : AbstractValidator<AppUserViewModel>
	{
		public AppUserValidator() 
		{
			RuleFor(user => user.UserName)
				.NotEmpty().WithMessage("Your username cannot be empty...")
				.Length(4, 15).WithMessage("Your password length must be at least 4 and exceed 15");

			RuleFor(user => user.Email)
				.NotEmpty().WithMessage("Your email cannot be empty...")
				.EmailAddress().WithMessage("Please enter a value in email format...");

			RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty")
				.MinimumLength(8).WithMessage("Your password length must be at least 8.")
				.MaximumLength(16).WithMessage("Your password length must not exceed 16.")
				.Matches(@"[A-Z]+").WithMessage("Your password must contain at least one uppercase letter.")
				.Matches(@"[a-z]+").WithMessage("Your password must contain at least one lowercase letter.")
				.Matches(@"[0-9]+").WithMessage("Your password must contain at least one number.")
				.Matches(@"[\!\?\*\.]+").WithMessage("Your password must contain at least one (!? *.).");
		}
	}	
}
