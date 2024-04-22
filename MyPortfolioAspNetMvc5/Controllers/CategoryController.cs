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
    public class CategoryController : Controller
    {
        CategoryRepository _categoryRepository = new CategoryRepository();
        public ActionResult Index()
        {
            var value = _categoryRepository.GetList();
            return View(value);
        }
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddCategory(Category category)
        {
            if (category.Name == null)
            {
                ModelState.AddModelError("Name", "Lütfen Kategori Adını Girin.");
                return View();
            }
            else
            {
                _categoryRepository.Insert(category);
                TempData["Result"] = "Kayıt Eklendi";
                TempData["Icon"] = "success";
                TempData["IsError"] = "true";
                return RedirectToAction("Index");
            }
   
        }

        [HttpGet]
        public ActionResult UpdateCategory(int id)
        {
            var value = _categoryRepository.GetByID(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateCategory(Category category)
        {
            if (category.Name == null)
            {
                ModelState.AddModelError("Name", "Lütfen Kategori Adını Girin.");
                return View();
            }
            else
            {
                var value = _categoryRepository.GetByID(category.CategoryID);
                value.Name = category.Name;
                _categoryRepository.Update(category);
                TempData["Result"] = "Kayıt Güncellendi";
                TempData["Icon"] = "success";
                TempData["IsError"] = "true";
                return RedirectToAction("Index");
            }

        }

        public ActionResult DeleteCategory(int id)
        {
            var value = _categoryRepository.GetByID(id);
            _categoryRepository.Delete(value);
            TempData["Result"] = "Kayıt Silindi";
            TempData["Icon"] = "success";
            TempData["IsError"] = "true";
            return RedirectToAction("Index");
        }
    }
}