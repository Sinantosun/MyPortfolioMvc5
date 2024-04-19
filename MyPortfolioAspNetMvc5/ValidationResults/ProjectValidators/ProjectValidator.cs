using FluentValidation;
using MyPortfolioAspNetMvc5.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortfolioAspNetMvc5.ValidationResults.ProjectValidators
{
    public class ProjectValidator : AbstractValidator<Projects>
    {
        public ProjectValidator()
        {
            RuleFor(x => x.ProjectName).MaximumLength(50).WithMessage("Proje adı 50 karakterden kısa olmalıdır.");
            RuleFor(x => x.ProjectName).NotEmpty().WithMessage("Proje adı boş geçilemez.");

            RuleFor(x => x.ProjectTitle).MaximumLength(50).WithMessage("Proje başlığı 50 karakterden kısa olmalıdır.");
            RuleFor(x => x.ProjectTitle).NotEmpty().WithMessage("Proje başlığı boş geçilemez.");

            RuleFor(x => x.ProjectDescription).MaximumLength(200).WithMessage("Proje açıklaması 200 karakterden kısa olmalıdır.");
            RuleFor(x => x.ProjectDescription).NotEmpty().WithMessage("Proje açıklaması boş geçilemez.");

            RuleFor(x => x.ProjectGithubURL).NotEmpty().WithMessage("Proje github url alanı boş geçilemez.");

           



        }
    }
}