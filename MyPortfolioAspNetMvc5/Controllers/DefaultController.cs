using FluentValidation.Results;
using MyPortfolioAspNetMvc5.Models.Entity;
using MyPortfolioAspNetMvc5.ValidationResults.ContactValidations;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MyPortfolioAspNetMvc5.Controllers
{
    public class DefaultController : Controller
    {
        DbCvEntities _context = new DbCvEntities();
        public ActionResult Index()
        {
            var values = _context.Abouts.ToList();
            return View(values);
        }
        public PartialViewResult ExperincePartial()
        {
            var value = _context.Experinces.ToList();
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
        public ActionResult ContactPartial(Contacts contacts)
        {
            ContactValidator validationRules = new ContactValidator();
            ValidationResult validationResult = validationRules.Validate(contacts);
            if (validationResult.IsValid)
            {
                contacts.MessageDate = Convert.ToDateTime(DateTime.Now.ToString("g"));
                _context.Contacts.Add(contacts);
                _context.SaveChanges();
                TempData["Result"] = "Mesajınız başarıyla iletilidi.";
                TempData["Icon"] = "success";
                TempData["Color"] = "#6c757d";
                return Redirect("../Default/Index#contact");
            }
            else
            {

                string err = string.Join("<br>", validationResult.Errors.Select(y => y.ErrorMessage));
                TempData["Result"] = err;
                TempData["Icon"] = "danger";
                TempData["Color"] = "red";
                return Redirect("../Default/Index#contact");
            }




        }

    }
}