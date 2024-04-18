using FluentValidation.Results;
using MyPortfolioAspNetMvc5.Models.Entity;
using MyPortfolioAspNetMvc5.Repositoryies;
using MyPortfolioAspNetMvc5.ValidationResults.ExperinceValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolioAspNetMvc5.Controllers
{
    public class ExperinceController : Controller
    {


        ExperincesRepository experincesRepository = new ExperincesRepository();

        public ActionResult Index()
        {
            var values = experincesRepository.GetList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddExperince()
        {
          
            return View();
        }
        [HttpPost]
        public ActionResult AddExperince(Experinces experinces)
        {
            ExperinceValidator validationRules = new ExperinceValidator();
            ValidationResult validationResult = validationRules.Validate(experinces);
            if (validationResult.IsValid)
            {
                experincesRepository.Insert(experinces);
                TempData["Result"] = "Yeni kayıt başarıyla eklendi.";
                TempData["Icon"] = "success";
                TempData["IsError"] = "true";
                return RedirectToAction("Index");

            }
            else
            {

                var err = string.Join("<br>",validationResult.Errors.Select(y=>y.ErrorMessage));
                TempData["Result"] = err;
                TempData["Icon"] = "danger";
            }
            return View();
        }

        public ActionResult DeleteExperince(int id)
        {
            var findExperince = experincesRepository.GetByID(id);
            experincesRepository.Delete(findExperince);
            TempData["Result"] = "Kayıt Silindi";
            TempData["Icon"] = "success";
            TempData["IsError"] = "true";
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult UpdateExperince(int id)
        {
            var values = experincesRepository.GetByID(id);
            return View(values);
        }
        [HttpPost]
        public ActionResult UpdateExperince(Experinces experinces)
        {
            ExperinceValidator validationRules = new ExperinceValidator();
            ValidationResult validationResult = validationRules.Validate(experinces);
            var value = experincesRepository.GetByID(experinces.ExperinceID);
            if (validationResult.IsValid)
            {
                
                value.Date = experinces.Date;
                value.Description = experinces.Description;
                value.SubTitle = experinces.SubTitle;
                value.Title = experinces.Title;
                experincesRepository.Update(experinces);
                TempData["Result"] = "Kayıt başarıyla güncellendi.";
                TempData["Icon"] = "success";
                TempData["IsError"] = "true";
                return RedirectToAction("Index");

            }
            else
            {

                var err = string.Join("<br>", validationResult.Errors.Select(y => y.ErrorMessage));
                TempData["Result"] = err;
                TempData["Icon"] = "danger";
            }
            return View(value);
        }
    }
}