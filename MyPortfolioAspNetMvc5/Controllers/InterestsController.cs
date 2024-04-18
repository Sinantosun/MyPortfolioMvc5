using FluentValidation.Results;
using MyPortfolioAspNetMvc5.Models.Entity;
using MyPortfolioAspNetMvc5.Repositoryies;
using MyPortfolioAspNetMvc5.ValidationResults.InterestsValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolioAspNetMvc5.Controllers
{
    public class InterestsController : Controller
    {
        InterestRepository interestRepository = new InterestRepository();
        public ActionResult Index()
        {
            var value = interestRepository.GetList();
            return View(value);
        }
        [HttpGet]
        public ActionResult Addinterests()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Addinterests(Interests interests)
        {
            InterestsValidator validationRules = new InterestsValidator();
            ValidationResult validationResult = validationRules.Validate(interests);
            if (validationResult.IsValid)
            {
                interestRepository.Insert(interests);
                TempData["Result"] = "Kayıt Eklendi";
                TempData["Icon"] = "success";
                TempData["IsError"] = "true";
                return RedirectToAction("Index");
            }
            else
            {
                string err = string.Join("<br>", validationResult.Errors.Select(x => x.ErrorMessage));
                TempData["Result"] = err;
                TempData["Icon"] = "danger";
                return View();
            }

        }


        [HttpGet]
        public ActionResult UpdateInterests(int id)
        {
            var value = interestRepository.GetByID(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateInterests(Interests interests)
        {
            var value = interestRepository.GetByID(interests.InterestsID);
            InterestsValidator validationRules = new InterestsValidator();
            ValidationResult validationResult = validationRules.Validate(interests);
            if (validationResult.IsValid)
            {
                value.Description = interests.Description;
                value.Description2 = interests.Description2;
                interestRepository.Update(interests);
                TempData["Result"] = "Kayıt Güncellendi";
                TempData["Icon"] = "success";
                TempData["IsError"] = "true";
                return RedirectToAction("Index");
            }
            else
            {
                string err = string.Join("<br>", validationResult.Errors.Select(x => x.ErrorMessage));
                TempData["Result"] = err;
                TempData["Icon"] = "danger";
                return View();
            }

        }


        public ActionResult DeleteInterests(int id)
        {
            var value = interestRepository.GetByID(id);
            interestRepository.Delete(value);
            TempData["Result"] = "Kayıt Silindi";
            TempData["Icon"] = "success";
            TempData["IsError"] = "true";
            return RedirectToAction("Index");
        }
    }
}