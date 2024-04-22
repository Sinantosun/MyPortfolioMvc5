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
    public class AdminLayoutController : Controller
    {

        public ActionResult Index()
        {

            return View();
        }
        AboutRepository aboutRepository = new AboutRepository();
        public PartialViewResult Invoke()
        {

            var value = aboutRepository.GetList().First();
            ViewBag.NameSurname = value.Name + " " + value.Surname;
            ViewBag.ImageURL = value.ImageURL;
            return PartialView();
        }
    }
}