using MyPortfolioAspNetMvc5.DAL;
using MyPortfolioAspNetMvc5.Models.Entity;
using MyPortfolioAspNetMvc5.Repositoryies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static System.Collections.Specialized.BitVector32;

namespace MyPortfolioAspNetMvc5.Controllers
{
    public class ProfileSettingsController : Controller
    {
        AboutRepository aboutRepository = new AboutRepository();
        public ActionResult Index()
        {


            return View();
        }

        public PartialViewResult ProfileImagePartial()
        {
            var value = aboutRepository.GetList().First();
            ViewBag.NameSurname = value.Name + " " + value.Surname;
            ViewBag.ImageURL = value.ImageURL;
            return PartialView();
        }


        public PartialViewResult ProfileAboutPartial()
        {
            var value = aboutRepository.GetList().First();
            ViewBag.NameSurname = value.Name + " " + value.Surname;
            ViewBag.ImageURL = value.ImageURL;
            return PartialView();
        }
        DbCvEntities dbCvEntities = new DbCvEntities();
        [HttpGet]
        public PartialViewResult EditProfilePartial()
        {
            string session = Session["UserName"].ToString();

            var values = dbCvEntities.Admins.FirstOrDefault(x => x.UserName == session);

            return PartialView(values);
        }
        [HttpPost]
        public ActionResult EditProfilePartial(Admins admins)
        {
            string session = Session["UserName"].ToString();
            var values = dbCvEntities.Admins.FirstOrDefault(x => x.UserName == session);
            
           
            if (admins.Pwd == null || admins.UserName == null)
            {
                TempData["Result"] = "lütfen alanları doldurun";
                TempData["Icon"] = "danger";
                return PartialView(values);
            }
            else
            {
                var value = dbCvEntities.Admins.Find(admins.AdminID);
                value.UserName = admins.UserName;
                var guid = Guid.NewGuid();
                var guid2 = Guid.NewGuid();

                string fullpwd = guid + "_" + guid2;
                var HassedPassword = PasswordHasher.HashPassword(fullpwd + admins.Pwd + fullpwd);
                value.PwdSalt = fullpwd;
                value.Pwd = HassedPassword;
                dbCvEntities.SaveChanges();
                TempData["Result"] = "profiliniz güncellendi";
                TempData["Icon"] = "success";
                return PartialView(values);
            }


         
        }




    }
}