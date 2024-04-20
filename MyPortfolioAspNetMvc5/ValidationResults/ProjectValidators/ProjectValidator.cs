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
            RuleFor(x => x.ProjectName).MaximumLength(100).WithMessage("Proje adı 100 karakterden kısa olmalıdır.");
            RuleFor(x => x.ProjectName).NotEmpty().WithMessage("Proje adı boş geçilemez.");

            RuleFor(x => x.ProjectTitle).MaximumLength(100).WithMessage("Proje başlığı 100 karakterden kısa olmalıdır.");
            RuleFor(x => x.ProjectTitle).NotEmpty().WithMessage("Proje başlığı boş geçilemez.");

            RuleFor(x => x.ProjectDescription).NotEmpty().WithMessage("Proje açıklaması boş geçilemez.");

            RuleFor(x => x.ProjectGithubURL).NotEmpty().WithMessage("Proje github url alanı boş geçilemez.");

           



        }
    }
}