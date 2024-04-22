using MyPortfolioAspNetMvc5.DAL;
using MyPortfolioAspNetMvc5.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MyPortfolioAspNetMvc5.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {

        [HttpGet]
        public ActionResult Index(string st, string ReturnUrl)
        {
            if (st == "1")
            {
                TempData["Result"] = "Oturum Süresi Doldu Lütfen Tekrar Giriş Yapın.";
            }

            TempData["returnUrl"] = ReturnUrl;

            return View();
        }

        [HttpPost]
        public ActionResult Index(Admins admins)
        {
            DbCvEntities dbCvEntities = new DbCvEntities();
            var userInfo = dbCvEntities.Admins.FirstOrDefault(x => x.UserName == admins.UserName);
            if (userInfo != null)
            {
                string hasedPassword = PasswordHasher.HashPassword(userInfo.PwdSalt + admins.Pwd + userInfo.PwdSalt);
                if (hasedPassword == userInfo.Pwd)
                {
                    FormsAuthentication.SetAuthCookie(userInfo.UserName, false);
                    Session["UserName"] = userInfo.UserName;
                    if (TempData["returnUrl"] == null)
                    {
                        return RedirectToAction("Index", "Abouts");
                    }
                    else
                    {
                        return Redirect(TempData["returnUrl"].ToString());
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Kullanıcı Adı Veya Şifre Hatalı");
                    return View();
                }
            }
            else
            {
                ModelState.AddModelError("", "Kullanıcı Adı Veya Şifre Hatalı");
                return View();
            }


        }


        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index");
        }
    }
}