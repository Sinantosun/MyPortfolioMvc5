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
    public class CertificateController : Controller
    {
        CertificateRepository certificateRepository = new CertificateRepository();
        public ActionResult Index()
        {
            var values = certificateRepository.GetList();
            return View(values);
        }

        [HttpGet]
        public ActionResult AddCertificate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCertificate(Certifications certifications)
        {
            if (string.IsNullOrEmpty(certifications.Description))
            {
                ModelState.AddModelError("Description", "Lütfen bu alanı doldurun.");
                return View();

            }
            else
            {
                certificateRepository.Insert(certifications);
                TempData["Result"] = "Kayıt Eklendi.";
                TempData["Icon"] = "success";
                TempData["IsError"] = "true";
                return RedirectToAction("Index");
            }
           
     
        }

        [HttpGet]
        public ActionResult UpdateCertificate(int id)
        {
            var values = certificateRepository.GetByID(id);

            return View(values);
        }
        [HttpPost]
        public ActionResult UpdateCertificate(Certifications certifications)
        {
            var values = certificateRepository.GetByID(certifications.CertificationsID);
            if (string.IsNullOrEmpty(certifications.Description))
            {
                ModelState.AddModelError("Description", "Lütfen bu alanı doldurun.");
                return View(values);

            }
            else
            {
         
                values.Description = certifications.Description;
                certificateRepository.Update(certifications);
                TempData["Result"] = "Kayıt Güncellendi.";
                TempData["Icon"] = "success";
                TempData["IsError"] = "true";
                return RedirectToAction("Index");
            }


        }

        public ActionResult DeleteCertificate(int id)
        {
            var values = certificateRepository.GetByID(id);
            certificateRepository.Delete(values);
            TempData["Result"] = "Kayıt Silindi.";
            TempData["Icon"] = "success";
            TempData["IsError"] = "true";
            return RedirectToAction("Index");

        }
    }
}