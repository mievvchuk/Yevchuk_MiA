using LW4_Task2_MiA.Models;
using FluentValidation;
using System.Text.RegularExpressions;

namespace LW4_Task2_MiA.Validators
{
    public class RecipeValidator : AbstractValidator<Recipe>
    {
        public RecipeValidator()
        {
            // назва
            RuleFor(x => x.Title).NotEmpty().MinimumLength(3).MaximumLength(80);

            // slug тільки малі букви, цифри, тире
            RuleFor(x => x.Slug).NotEmpty().Must(s => Regex.IsMatch(s, "^[a-z0-9-]+$")).WithMessage("Slug має містити лише малі латиницю, цифри та тире.");

            // опис
            RuleFor(x => x.Description).NotEmpty().MinimumLength(10).MaximumLength(1000);

            // складність
            RuleFor(x => x.Difficulty).IsInEnum();

            //після переходу на Mongo — це рядкові ObjectId
            RuleFor(x => x.CategoryId).NotEmpty().Length(24).WithMessage("CategoryId має бути валідним Mongo ObjectId (24 символи).");

            RuleFor(x => x.AuthorUserId).NotEmpty().Length(24).WithMessage("AuthorUserId має бути валідним Mongo ObjectId (24 символи).");
        }
    }
}
