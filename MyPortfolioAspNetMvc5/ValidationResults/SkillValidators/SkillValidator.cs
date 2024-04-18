using FluentValidation;
using MyPortfolioAspNetMvc5.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortfolioAspNetMvc5.ValidationResults.SkillValidators
{
    public class SkillValidator : AbstractValidator<Skills>
    {
        public SkillValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Yetenek adı boş geçilemez");
            RuleFor(x => x.Name).MaximumLength(100).WithMessage("Yetenek adı 100 karakterden kısa olmalıdır.");

            RuleFor(x => x.SkillValue).NotEmpty().WithMessage("Derece alanı boş geçilemez.");
            RuleFor(x => x.SkillValue).LessThanOrEqualTo((byte)100).WithMessage("Derece alanı 100'den kücük olmalıdır.");
            RuleFor(x => x.SkillValue).GreaterThanOrEqualTo((byte)0).WithMessage("Derece alanı 0'dan büyük olmalıdır.");

        }
    }
}