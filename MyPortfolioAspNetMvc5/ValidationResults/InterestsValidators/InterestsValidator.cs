using FluentValidation;
using MyPortfolioAspNetMvc5.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortfolioAspNetMvc5.ValidationResults.InterestsValidators
{
    public class InterestsValidator:AbstractValidator<Interests>
    {
        public InterestsValidator()
        {
            RuleFor(x => x.Description).NotEmpty().WithMessage("1.Açıklama alanı boş geçilemez");
            RuleFor(x => x.Description).MaximumLength(500).WithMessage("1.Açıklama alanı 500 karakterden kısa olmalıdır");

            RuleFor(x => x.Description2).NotEmpty().WithMessage("2.Açıklama alanı boş geçilemez");
            RuleFor(x => x.Description2).MaximumLength(500).WithMessage("2.Açıklama alanı 500 karakterden kısa olmalıdır");
        }
    }
}