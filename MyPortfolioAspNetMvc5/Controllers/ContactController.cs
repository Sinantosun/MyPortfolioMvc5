using MyPortfolioAspNetMvc5.Repositoryies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolioAspNetMvc5.Controllers
{
    public class ContactController : Controller
    {
        ContactRepository ContactRepository = new ContactRepository();
        public ActionResult Index()
        {
            var values = ContactRepository.GetList();
            return View(values);
        }
    }
}