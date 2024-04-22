using FluentValidation;
using FluentValidation.Results;
using MyPortfolioAspNetMvc5.DAL;
using MyPortfolioAspNetMvc5.Models.Entity;
using MyPortfolioAspNetMvc5.Repositoryies;
using MyPortfolioAspNetMvc5.ValidationResults.AboutValidators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolioAspNetMvc5.Controllers
{
    [SessionTimeOut]
    public class AboutsController : Controller
    {

        AboutRepository aboutRepository = new AboutRepository();
        public ActionResult Index()
        {

            var values = aboutRepository.GetList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAbout(Abouts abouts)
        {
            if (abouts.ImageURL != null)
            {
                AboutValidator validationRules = new AboutValidator();
                ValidationResult validationResult = validationRules.Validate(abouts);
                if (validationResult.IsValid)
                {
                    var guid = Guid.NewGuid();
                    var ex = Path.GetExtension(Request.Files[0].FileName);
                    string fullname = guid + ex;
                    Request.Files[0].SaveAs(Server.MapPath("~/Images/Abouts/" + fullname));
                    abouts.ImageURL = "/Images/Abouts/" + fullname;
                    aboutRepository.Insert(abouts);
                    TempData["Result"] = "Kayıt eklendi";
                    TempData["Icon"] = "success";
                    TempData["IsError"] = "true";
                    return RedirectToAction("Index");
                }
                else
                {
                    string err = string.Join("<br>", validationResult.Errors.Select(y => y.ErrorMessage));
                    TempData["Result"] = err;
                    TempData["Icon"] = "danger";
                    return View();
                }
            }
            else
            {
                TempData["Result"] = "Resim seçiniz.";
                TempData["Icon"] = "danger";
                return View();
            }



        }



        [HttpGet]
        public ActionResult UpdateAbout(int id)
        {
            var value = aboutRepository.GetByID(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateAbout(Abouts abouts)
        {
            var value = aboutRepository.GetByID(abouts.AboutID);
            AboutValidator validationRules = new AboutValidator();
            ValidationResult validationResult = validationRules.Validate(abouts);
            if (validationResult.IsValid)
            {
              

                if (!string.IsNullOrEmpty(abouts.ImageURL))
                {
                    var guid = Guid.NewGuid();
                    var ex = Path.GetExtension(Request.Files[0].FileName);
                    string fullname = guid + ex;
                    Request.Files[0].SaveAs(Server.MapPath("~/Images/Abouts/" + fullname));
                    if (System.IO.File.Exists(Server.MapPath(value.ImageURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(value.ImageURL));
                    }
                    value.ImageURL = "/Images/Abouts/" + fullname;
                }

                value.Adress = abouts.Adress;
                value.Description = abouts.Description;
                value.Mail = abouts.Mail;
                value.Name = abouts.Name;
                value.Surname = abouts.Surname;
                value.PhoneNumber = abouts.PhoneNumber;

                aboutRepository.Update(abouts);
                TempData["Result"] = "Kayıt Güncellendi";
                TempData["Icon"] = "success";
                TempData["IsError"] = "true";
                return RedirectToAction("Index");
            }
            else
            {
                string err = string.Join("<br>", validationResult.Errors.Select(y => y.ErrorMessage));
                TempData["Result"] = err;
                TempData["Icon"] = "danger";
                return View(value);
            }
        }

        public ActionResult DeleteAbout(int id)
        {
            var value = aboutRepository.GetByID(id);
            if (System.IO.File.Exists(Server.MapPath(value.ImageURL)))
            {
                System.IO.File.Delete(Server.MapPath(value.ImageURL));
            }
            aboutRepository.Delete(value);
            TempData["Result"] = "Kayıt Silindi";
            TempData["Icon"] = "success";
            TempData["IsError"] = "true";
            return RedirectToAction("Index");
        }

    }
}