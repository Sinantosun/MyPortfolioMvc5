using FluentValidation;
using MyPortfolioAspNetMvc5.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyPortfolioAspNetMvc5.ValidationResults.AboutValidators
{
    public class AboutValidator : AbstractValidator<Abouts>
    {
        public AboutValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ad alanı boş geçilemez");
            RuleFor(x => x.Name).MaximumLength(50).WithMessage("Ad alanı 50 karakterden kısa olmalıdır");

            RuleFor(x => x.Surname).NotEmpty().WithMessage("Soyad alanı boş geçilemez");
            RuleFor(x => x.Surname).MaximumLength(50).WithMessage("Soyad alanı 50 karakterden kısa olmalıdır");

            RuleFor(x => x.Adress).NotEmpty().WithMessage("Adress alanı boş geçilemez");
            RuleFor(x => x.Adress).MaximumLength(150).WithMessage("Adress alanı 150 karakterden kısa olmalıdır");

            RuleFor(x => x.PhoneNumber).NotEmpty().WithMessage("Telefon numarası boş geçilemez");
            RuleFor(x => x.PhoneNumber).MaximumLength(50).WithMessage("Telefon numarası  50 karakterden kısa olmalıdır");

            RuleFor(x => x.Mail).NotEmpty().WithMessage("Mail alanı boş geçilemez");
            RuleFor(x => x.Mail).MaximumLength(50).WithMessage("Mail alanı  50 karakterden kısa olmalıdır");
            RuleFor(x => x.Mail).EmailAddress().WithMessage("Geçersiz mail adresi girdiniz lütfen mail adresinizi kontrol edin");

        }
    }
}