using FluentValidation;
using MyPortfolioAspNetMvc5.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortfolioAspNetMvc5.ValidationResults.EducationValidators
{
    public class EducationValidator : AbstractValidator<Educations>
    {
        public EducationValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık alanı boş geçeilmez");
            RuleFor(x => x.Title).MaximumLength(50).WithMessage("Başlık alanı 50 karakterden kısa olmalıdır.");

            RuleFor(x => x.SubTitle1).NotEmpty().WithMessage("1. Alt başlık alanı boş geçeilmez");
            RuleFor(x => x.SubTitle1).MaximumLength(50).WithMessage("1. Alt başlık alanı 50 karakterden kısa olmalıdır");

            RuleFor(x => x.SubTitle2).NotEmpty().WithMessage("2. Alt başlık alanı boş geçeilmez");
            RuleFor(x => x.SubTitle2).MaximumLength(50).WithMessage("2. Alt başlık alanı 50 karakterden kısa olmalıdır");

            RuleFor(x => x.EducationGNO).NotEmpty().WithMessage("GNO Alanı boş geçilemez.");
            RuleFor(x => x.EducationGNO).MaximumLength(50).WithMessage("GNO Alanı 50 karakterden kısa olmalıdır.");

            RuleFor(x => x.Date).MaximumLength(50).WithMessage("Tarih alanı 50 karakterden kısa olmalıdır.");
            RuleFor(x => x.Date).NotEmpty().WithMessage("Tarih alanı boş geçilemez.");
        }
    }
}