using FluentValidation.Results;
using MyPortfolioAspNetMvc5.Models.Entity;
using MyPortfolioAspNetMvc5.Repositoryies;
using MyPortfolioAspNetMvc5.ValidationResults.SkillValidators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolioAspNetMvc5.Controllers
{
    public class SkillsController : Controller
    {
        SkillsRepository skillsRepository = new SkillsRepository();
        public ActionResult Index()
        {
            var value = skillsRepository.GetList();
            return View(value);
        }
        [HttpGet]
        public ActionResult AddSkill()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddSkill(Skills skills)
        {
            SkillValidator validationRules = new SkillValidator();
            ValidationResult validationResult = validationRules.Validate(skills);
            if (validationResult.IsValid)
            {
                skillsRepository.Insert(skills);
                TempData["Result"] = "Yeni Kayıt Eklendi.";
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
        public ActionResult DeleteSkill(int id)
        {
            var value = skillsRepository.GetByID(id);
            skillsRepository.Delete(value);
            TempData["Result"] = "Kayıt Silindi.";
            TempData["Icon"] = "success";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public ActionResult UpdateSkill(int id)
        {
            var value = skillsRepository.GetByID(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateSkill(Skills skills)
        {
            SkillValidator validationRules = new SkillValidator();
            ValidationResult validationResult = validationRules.Validate(skills);
            var value = skillsRepository.GetByID(skills.SkillsID);
            if (validationResult.IsValid)
            {
          
                value.Name = skills.Name;
                value.SkillValue = skills.SkillValue;
                skillsRepository.Update(skills);
                TempData["Result"] = "Kayıt Güncellendi.";
                TempData["Icon"] = "success";
                TempData["IsError"] = "true";
                return RedirectToAction("Index");
            }
            else
            {
                string err = string.Join("<br>", validationResult.Errors.Select(x => x.ErrorMessage));
                TempData["Result"] = err;
                TempData["Icon"] = "danger";
                return View(value);
            }
        }
    }
}