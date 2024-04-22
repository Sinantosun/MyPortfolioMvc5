using MyPortfolioAspNetMvc5.DAL;
using MyPortfolioAspNetMvc5.Repositoryies;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolioAspNetMvc5.Controllers
{
    [SessionTimeOut]
    public class ContactController : Controller
    {
        ContactRepository _contactRepository = new ContactRepository();
        public ActionResult Index()
        {
            var values = _contactRepository.FilterList(x => x.IsRead == false);
            TempData["MesssageType"] = "Okunmayan Mesajlar";
            return View(values);
        }

        public ActionResult IsReadMessagesList()
        {
            var values = _contactRepository.FilterList(x => x.IsRead == true);
            TempData["MesssageType"] = "Okunan Mesajlar";
            return View("Index", values);
        }

        public ActionResult GetAllMessages()
        {
            var values = _contactRepository.GetList();
            TempData["MesssageType"] = "Tüm Mesajlar";

            return View("Index", values);
        }

        public ActionResult ChangeReadingStatus(int id)
        {
            var value = _contactRepository.GetByID(id);
            if (value.IsRead == true)
            {
                value.IsRead = false;
            }
            else
            {
                value.IsRead = true;
            }
            _contactRepository.Update(value);


            TempData["Result"] = "Mesaj Durumu " + (value.IsRead == true ? "Okundu" : "Okunmadı") + " Olarak Güncellendi";
            TempData["Icon"] = "success";

            if (value.IsRead == true)
            {
                return RedirectToAction("Index");
            }
            else
            {
                return RedirectToAction("IsReadMessagesList");
            }

        }


        public ActionResult ContactDetail(int id)
        {
            var value = _contactRepository.GetByID(id);
            return View(value);
        }

        public JsonResult DeleteContact(int id)
        {
            var value = _contactRepository.GetByID(id);
            _contactRepository.Delete(value);
            TempData["Result"] = "Silindi";
            TempData["Icon"] = "success";
            TempData["IsError"] = "true";
            return Json(id);
        }
    }
}