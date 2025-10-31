using LW4_Task2_MiA.Models;
using FluentValidation;
using System;

namespace LW4_Task2_MiA.Validators
{
    public class RatingValidator : AbstractValidator<Rating>
    {
        public RatingValidator()
        {
            // обов’язково вказати рецепт
            RuleFor(x => x.RecipeId)
                .NotEmpty()
                .Length(24) // стандартна довжина Mongo ObjectId
                .WithMessage("RecipeId must be a valid Mongo ObjectId (24 chars).");

            // обов’язково вказати користувача
            RuleFor(x => x.UserId)
                .NotEmpty()
                .Length(24)
                .WithMessage("UserId must be a valid Mongo ObjectId (24 chars).");

            // рейтинг від 1 до 5
            RuleFor(x => x.Value)
                .InclusiveBetween(1, 5);

            // коментар опціональний, але не довший
            RuleFor(x => x.Comment)
                .MaximumLength(500);
        }
    }
}
