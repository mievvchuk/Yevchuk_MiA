using LW4_Task2_MiA.Models;
using FluentValidation;
using System.Text.RegularExpressions;
namespace LW4_Task2_MiA.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        private const string EmailRegex =
            @"^[A-Za-z0-9._%+\-]+@[A-Za-z0-9.\-]+\.[A-Za-z]{2,}$";

        public UserValidator()
        {
            RuleFor(x => x.DisplayName).NotEmpty().MinimumLength(2).MaximumLength(50);

            RuleFor(x => x.Email).NotEmpty().Must(v => Regex.IsMatch(v, EmailRegex)).WithMessage("Невірний формат email.");

            RuleFor(x => x.Role).IsInEnum();
        }
    }
}
