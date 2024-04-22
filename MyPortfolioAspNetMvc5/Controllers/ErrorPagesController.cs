using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolioAspNetMvc5.Controllers
{
    [AllowAnonymous]
    public class ErrorPagesController : Controller
    {
        // GET: ErrorPages
        public ActionResult Page404()
        {
            Response.StatusCode = 404;
            Response.TrySkipIisCustomErrors = true;
            return View();
        }
    }
}