using FluentValidation;
using MyPortfolioAspNetMvc5.Models.Entity;


namespace MyPortfolioAspNetMvc5.ValidationResults.ContactValidations
{
    public class ContactValidator : AbstractValidator<Contacts>
    {
        public ContactValidator()
        {
            RuleFor(x => x.Mail).NotEmpty().WithMessage("Mail alanı boş geçilemez");
            RuleFor(x => x.Mail).EmailAddress().WithMessage("Mail adresi geçerli değil lütfen geçerli bir mail adresi girin.");

            RuleFor(x => x.MessageContent).NotEmpty().WithMessage("Mesaj içeriği boş geçilemez.");
            RuleFor(x => x.MessageContent).MaximumLength(1000).WithMessage("Mesaj içeriği 1000 karakterden kısa tutulmalıdır.");

            RuleFor(x => x.NameSurname).MaximumLength(200).WithMessage("Ad soyad alanı 200 karakterden kısa tutulmalıdır.");
            RuleFor(x => x.NameSurname).MinimumLength(5).WithMessage("Ad soyad alanı 5 karakterden daha uzun tutulmalıdır.");
            RuleFor(x => x.NameSurname).NotEmpty().WithMessage("Ad soyad alanı boş geçilemez.");

            RuleFor(x => x.Title).NotEmpty().WithMessage("Başlık alanı boş geçilemez.");
            RuleFor(x => x.Title).MaximumLength(50).WithMessage("Konu başlığı 50 karakterden daha kısa tutulmalıdır..");
            RuleFor(x => x.Title).MinimumLength(5).WithMessage("Konu Başlığı  5 karakterden daha uzun tutulmalıdır.");
  
        }
    }
}