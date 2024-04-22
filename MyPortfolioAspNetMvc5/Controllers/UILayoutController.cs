using MyPortfolioAspNetMvc5.Repositoryies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolioAspNetMvc5.Controllers
{
    [AllowAnonymous]
    public class UILayoutController : Controller
    {
        AboutRepository _aboutRepository = new AboutRepository();
        public ActionResult Index()
        {
           
            return View();
        }

        public PartialViewResult UIHeaderPartial()
        {

            return PartialView();
        }

        public PartialViewResult UINavbarPartial()
        {
            var value = _aboutRepository.GetList();
          
            return PartialView(value);
        }

        public PartialViewResult UIScriptsPartial()
        {

            return PartialView();
        }
    }
}