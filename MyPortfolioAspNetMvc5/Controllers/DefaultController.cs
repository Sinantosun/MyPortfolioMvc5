using FluentValidation.Results;
using MyPortfolioAspNetMvc5.DAL;
using MyPortfolioAspNetMvc5.Models.Entity;
using MyPortfolioAspNetMvc5.ValidationResults.ContactValidations;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolioAspNetMvc5.Controllers
{
    [AllowAnonymous]
    public class DefaultController : Controller
    {

        DbCvEntities _context = new DbCvEntities();
        [HttpGet]
        public ActionResult Index()
        {

            return View();
        }
        public PartialViewResult ExperincePartial()
        {
            var value = _context.Experinces.ToList();
            return PartialView(value);
        }

        public PartialViewResult SocaialMediaPartial()
        {
            var value = _context.SocialMedias.ToList();
            return PartialView(value);
        }
        public PartialViewResult AboutPartial()
        {
            var value = _context.Abouts.ToList();
            return PartialView(value);
        }

        public PartialViewResult ProjectPartial()
        {
            var value = _context.Projects.Where(x => x.IsActive == true).ToList();
            return PartialView(value);
        }
        public PartialViewResult EducationsPartial()
        {
            var value = _context.Educations.ToList();
            return PartialView(value);
        }
        public PartialViewResult SkillsPartial()
        {
            var value = _context.Skills.ToList();
            return PartialView(value);
        }
        public PartialViewResult InterestsPartial()
        {
            var value = _context.Interests.ToList();
            return PartialView(value);
        }

        public PartialViewResult CertificatePartial()
        {
            var value = _context.Certifications.ToList();
            return PartialView(value);
        }

        [HttpGet]
        public PartialViewResult ContactPartial()
        {

            return PartialView();
        }

        [HttpPost]
        public async Task<ActionResult> Index(Contacts contacts)
        {

            var captchaImage = HttpContext.Request.Form["g-recaptcha-response"];
            if (captchaImage == null)
            {
                TempData["Result"] = "hata, captcha alanını doldurun.";
                TempData["Icon"] = "danger";
                TempData["Color"] = "#red";
            }

            var verifed = await CheckCatpcha();
            if (verifed)
            {

                ContactValidator validationRules = new ContactValidator();
                ValidationResult validationResult = validationRules.Validate(contacts);
                if (validationResult.IsValid)
                {
                    contacts.IsRead = false;
                    contacts.MessageDate = Convert.ToDateTime(DateTime.Now.ToString("g"));
                    _context.Contacts.Add(contacts);
                    _context.SaveChanges();
                    TempData["Result"] = "Mesajınız başarıyla iletilidi.";
                    TempData["Icon"] = "success";
                    TempData["Color"] = "#6c757d";
                    MailGenerator mailGenerator = new MailGenerator();
                    mailGenerator.SendMail(contacts.NameSurname, contacts.Title, contacts.MessageContent);


                }
                else
                {

                    string err = string.Join("<br>", validationResult.Errors.Select(y => y.ErrorMessage));
                    TempData["Result"] = err;
                    TempData["Icon"] = "danger";
                    TempData["Color"] = "red";

                }
            }
            else
            {
                TempData["Result"] = "Güvenlik doğrulaması başarısız.";
                TempData["Icon"] = "danger";
                TempData["Color"] = "#red";
            }
            return Redirect("/Default/Index#contact");
        }

        public async Task<bool> CheckCatpcha()
        {
            var postData = new List<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("secret", "6Lf58tUpAAAAALjJVJHH-dwKpb2L20-m6VdKZMYC"),
                new KeyValuePair<string, string>("response", HttpContext.Request.Form["g-recaptcha-response"])
            };

            var client = new HttpClient();
            var respones = await client.PostAsync("https://www.google.com/recaptcha/api/siteverify", new FormUrlEncodedContent(postData));

            var _object = (JObject)JsonConvert.DeserializeObject(await respones.Content.ReadAsStringAsync());

            return (bool)_object["success"];
        }


    }
}