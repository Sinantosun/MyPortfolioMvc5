using FluentValidation.Results;
using MyPortfolioAspNetMvc5.DAL;
using MyPortfolioAspNetMvc5.Models.Entity;
using MyPortfolioAspNetMvc5.Repositoryies;
using MyPortfolioAspNetMvc5.ValidationResults.EducationValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolioAspNetMvc5.Controllers
{
    [SessionTimeOut]
    public class EducationController : Controller
    {
        EducationRepository _educationRepository = new EducationRepository();
        public ActionResult Index()
        {
            var value = _educationRepository.GetList();
            return View(value);
        }
        [HttpGet]
        public ActionResult AddEducation()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddEducation(Educations educations)
        {
            EducationValidator validationRules = new EducationValidator();
            ValidationResult validationResult = validationRules.Validate(educations);
            if (validationResult.IsValid)
            {
                _educationRepository.Insert(educations);

                TempData["Icon"] = "success";
                TempData["Result"] = "Yeni kayıt eklendi";
                TempData["IsError"] = "true";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Icon"] = "danger";
                string err = string.Join("<br>", validationResult.Errors.Select(x => x.ErrorMessage));
                TempData["Result"] = err;
            
                return View();
            }


        }

        [HttpGet]
        public ActionResult UpdateEducation(int id)
        {
            var value = _educationRepository.GetByID(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateEducation(Educations educations)
        {
            EducationValidator validationRules = new EducationValidator();
            ValidationResult validationResult = validationRules.Validate(educations);
            if (validationResult.IsValid)
            {
                var value = _educationRepository.GetByID(educations.EducationID);
                value.Date = educations.Date;
                value.EducationGNO = educations.EducationGNO;
                value.SubTitle1 = educations.SubTitle1;
                value.SubTitle2 = educations.SubTitle2;
                value.Title = educations.Title;

                _educationRepository.Update(educations);

                TempData["Icon"] = "success";
                TempData["Result"] = "Kayıt Güncellendi";
                TempData["IsError"] = "true";
                return RedirectToAction("Index");
            }
            else
            {
                TempData["Icon"] = "danger";
                string err = string.Join("<br>", validationResult.Errors.Select(x => x.ErrorMessage));
                TempData["Result"] = err;
                return View();
            }


        }

        public ActionResult DeleteEducation(int id)
        {
            var value = _educationRepository.GetByID(id);
            _educationRepository.Delete(value);
            TempData["Icon"] = "success";
            TempData["Result"] = "Kayıt Silindi";
            TempData["IsError"] = "true";
            return RedirectToAction("Index");
        }
    }


}