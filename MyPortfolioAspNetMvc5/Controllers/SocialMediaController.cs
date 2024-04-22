using MyPortfolioAspNetMvc5.DAL;
using MyPortfolioAspNetMvc5.Models.Entity;
using MyPortfolioAspNetMvc5.Repositoryies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolioAspNetMvc5.Controllers
{
    [SessionTimeOut]
    public class SocialMediaController : Controller
    {
        SocialMediaRepository socialMediaRepository = new SocialMediaRepository();
        public ActionResult Index()
        {
            var values = socialMediaRepository.GetList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddSocialMedia()
        {

            return View();
        }
        [HttpPost]
        public ActionResult AddSocialMedia(SocialMedias socialMedias)
        {
            if (socialMedias.Icon == null || socialMedias.URL == null)
            {
                TempData["Result"] = "Lütfen verileri eksiksiz girdiğinizden emin olun.";
                TempData["Icon"] = "danger";
                return View();
            }
            else
            {
                socialMediaRepository.Insert(socialMedias);
                TempData["Result"] = "Kayıt eklendi";
                TempData["Icon"] = "success";
                TempData["IsError"] = "true";
                return RedirectToAction("Index");
              
            }
        }

        [HttpGet]
        public ActionResult UpdateSocialMedia(int id)
        {
            var value = socialMediaRepository.Filter(x=>x.SocailMediaID==id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateSocialMedia(SocialMedias socialMedias)
        {
            if (socialMedias.Icon == null || socialMedias.URL == null)
            {
                TempData["Result"] = "Lütfen verileri eksiksiz girdiğinizden emin olun.";
                TempData["Icon"] = "danger";
                return View();
            }
            else
            {
                var value = socialMediaRepository.GetByID(socialMedias.SocailMediaID);
                value.Icon = socialMedias.Icon;
                value.URL = socialMedias.URL;

                socialMediaRepository.Update(socialMedias);
                TempData["Result"] = "Kayıt Güncellendi";
                TempData["Icon"] = "success";
                TempData["IsError"] = "true";
                return RedirectToAction("Index");

            }
        }

        public ActionResult DeleteSocailMedia(int id)
        {
            var value = socialMediaRepository.GetByID(id);
            socialMediaRepository.Delete(value);
            TempData["Result"] = "Kayıt Silindi";
            TempData["Icon"] = "success";
            TempData["IsError"] = "true";
            return RedirectToAction("Index");
        }
    }
}