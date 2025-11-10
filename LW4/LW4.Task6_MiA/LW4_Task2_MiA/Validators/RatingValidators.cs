using LW4_Task2_MiA.Models;
using FluentValidation;
using System;

namespace LW4_Task2_MiA.Validators
{
    public class RatingValidator : AbstractValidator<Rating>
    {
        public RatingValidator()
        {
            RuleFor(x => x.RecipeId).NotEmpty().Length(24).WithMessage("RecipeId must be a valid Mongo ");

            RuleFor(x => x.UserId).NotEmpty().Length(24).WithMessage("UserId must be a valid Mongo ");

            RuleFor(x => x.Value).InclusiveBetween(1, 5);

            RuleFor(x => x.Comment).MaximumLength(500);
        }
    }
}
