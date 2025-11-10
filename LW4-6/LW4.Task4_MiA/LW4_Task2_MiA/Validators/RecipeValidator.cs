using LW4_Task2_MiA.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace LW4_Task2_MiA.Validators
{
    public class RecipeValidator : AbstractValidator<Recipe>
    {
        public RecipeValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MinimumLength(3).MaximumLength(80);

            RuleFor(x => x.Slug).NotEmpty().Must(s => Regex.IsMatch(s, "^[a-z0-9-]+$")).WithMessage("Slug має містити лише малі латиницю, цифри та тире");

            RuleFor(x => x.Description).NotEmpty().MinimumLength(10).MaximumLength(1000);

            RuleFor(x => x.Difficulty).IsInEnum();

            //рядкові ObjectId
            RuleFor(x => x.CategoryId).NotEmpty().Length(24).WithMessage("CategoryId має бути валідним Mongo ");

            RuleFor(x => x.AuthorUserId).NotEmpty().Length(24).WithMessage("AuthorUserId має бути валідним Mongo ");
        }
    }
}
