using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FluentValidation.Validators;
using MyPortfolioAspNetMvc5.Models.Entity;

namespace MyPortfolioAspNetMvc5.ValidationResults.ExperinceValidators
{
    public class ExperinceValidator : AbstractValidator<Experinces>
    {
        public ExperinceValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık alanı boş geçilemez.");
            RuleFor(x => x.Title).MinimumLength(5).WithMessage("Başlık alanı 5 karakterden uzun olmalıdır.");
            RuleFor(x => x.Title).MaximumLength(50).WithMessage("Başlık alanı 50 karakterden kısa olmalıdır.");

            RuleFor(x => x.SubTitle).NotEmpty().WithMessage("Alt başlık alanı boş geçilemez.");
            RuleFor(x => x.SubTitle).MaximumLength(50).WithMessage("Alt başlık alanı 50 karakterden kısa olmalıdır.");

            RuleFor(x => x.Description).NotEmpty().WithMessage("Açıklama alanı boş geçilemez.");
            RuleFor(x => x.Description).MaximumLength(1000).WithMessage("Açıklama alanı 1000 karakterden kısa tutulmalıdır.");

            RuleFor(x => x.Date).NotEmpty().WithMessage("Tarih alanı boş geçilemez.");
            RuleFor(x => x.Date).MaximumLength(100).WithMessage("Tarih alanı 100 karakterden kısa olmalıdır.");




        }
    }
}