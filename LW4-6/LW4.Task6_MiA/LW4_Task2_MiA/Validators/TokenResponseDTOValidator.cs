using FluentValidation;
using LW4_Task6_MiA.DTO;
namespace LW4_Task6_MiA.Validators
{
    public class TokenResponseDTOValidator : AbstractValidator<TokenResponseDto>
    {
        public TokenResponseDTOValidator()
        {
            RuleFor(x => x.AccessToken).NotEmpty().WithMessage("AccessToken не може бути порожнім.");

            RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("RefreshToken не може бути порожнім.");
        }
    }
}
