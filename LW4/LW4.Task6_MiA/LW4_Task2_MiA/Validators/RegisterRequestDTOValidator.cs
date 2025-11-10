using FluentValidation;
using LW4_Task6_MiA.DTO;

namespace LW4_Task6_MiA.Validators
{
    public class RegisterRequestDtoValidator : AbstractValidator<RegisterRequestDto>
    {
        public RegisterRequestDtoValidator()
        {
            RuleFor(x => x.DisplayName).NotEmpty().WithMessage("Display Name не може бути порожнім.").Length(2, 50).WithMessage("Display Name має бути від 2 до 50 символів.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email не може бути порожнім.").EmailAddress().WithMessage("Введіть коректний email.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Пароль не може бути порожнім.").MinimumLength(8).WithMessage("Пароль має бути не менше 8 символів.");

            RuleFor(x => x.ConfirmPassword).Equal(x => x.Password).WithMessage("Паролі не співпадають.");
        }
    }
}
