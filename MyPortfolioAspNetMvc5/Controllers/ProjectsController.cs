using MyPortfolioAspNetMvc5.Models.Entity;
using MyPortfolioAspNetMvc5.Repositoryies;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyPortfolioAspNetMvc5.Controllers
{
    public class ProjectsController : Controller
    {
        ProjectRepository _projectRepository = new ProjectRepository();
        public ActionResult Index()
        {
            var values = _projectRepository.GetList();
            return View(values);
        }
        [HttpGet]
        public ActionResult AddProject()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddProject(Projects projects)
        {
            _projectRepository.Insert(projects);
            TempData["Result"] = "Kayıt Eklendi";
            TempData["Icon"] = "success";
            TempData["IsError"] = "true";
            return RedirectToAction("Index");
        }


        public ActionResult DeleteProjects(int id)
        {
            var values = _projectRepository.GetByID(id);
            _projectRepository.Delete(values);
            TempData["Result"] = "Kayıt Silindi";
            TempData["Icon"] = "success";
            TempData["IsError"] = "true";
            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult UpdateProjects(int id)
        {
            var value = _projectRepository.GetByID(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateProjects(Projects projects)
        {
            var value = _projectRepository.GetByID(projects.ProjectID);
            value.ProjectDescription = projects.ProjectDescription;
            value.ProjectGithubURL = projects.ProjectGithubURL;
            value.ProjectImage = projects.ProjectImage;
            value.ProjectName = projects.ProjectName;
            value.ProjectTitle = projects.ProjectTitle;
            _projectRepository.Update(projects);
            TempData["Result"] = "Kayıt Güncellendi";
            TempData["Icon"] = "success";
            TempData["IsError"] = "true";
            return RedirectToAction("Index");
        }
    }
}