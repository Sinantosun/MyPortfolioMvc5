using FluentValidation.Results;
using MyPortfolioAspNetMvc5.Models.Entity;
using MyPortfolioAspNetMvc5.Repositoryies;
using MyPortfolioAspNetMvc5.ValidationResults.ProjectValidators;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
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
            if (string.IsNullOrEmpty(projects.ProjectImage))
            {
                TempData["Result"] = "Proje görselini seçiniz.";
                TempData["Icon"] = "danger";
                return View();
            }
            else
            {

                var guid = Guid.NewGuid();
                var FileExtension = Path.GetExtension(Request.Files[0].FileName);
                var fullFileName = guid + FileExtension;
                if (FileExtension == ".jpg" || FileExtension == ".png" || FileExtension == ".jpeg")
                {
                    projects.ProjectImage = "/Images/Projects/" + fullFileName;
                    ProjectValidator validationRules = new ProjectValidator();
                    ValidationResult validationResult = validationRules.Validate(projects);
                    if (validationResult.IsValid)
                    {

                        Request.Files[0].SaveAs(Server.MapPath("~/Images/Projects/" + fullFileName));

                        _projectRepository.Insert(projects);
                        TempData["Result"] = "Kayıt Eklendi";
                        TempData["Icon"] = "success";
                        TempData["IsError"] = "true";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        var err = string.Join("<br>", validationResult.Errors.Select(y => y.ErrorMessage));
                        TempData["Result"] = err;
                        TempData["Icon"] = "danger";
                        return View();
                    }
                }
                else
                {
                   
                    TempData["Result"] = "Yüklemek istediğiniz göreslin uzantısı("+ FileExtension + ") desteklenmiyor lütfen .jpg - .jpeg - .png uzantılarıyla tekrar deneyin.";
                    TempData["Icon"] = "danger";
                    return View();
                }

                
            }


        }


        public ActionResult DeleteProjects(int id)
        {
            var values = _projectRepository.GetByID(id);
            _projectRepository.Delete(values);
            TempData["Result"] = "Kayıt Silindi";
            TempData["Icon"] = "success";
            TempData["IsError"] = "true";

            if (System.IO.File.Exists(Server.MapPath(values.ProjectImage)))
            {
                System.IO.File.Delete(Server.MapPath(values.ProjectImage));
            }
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
            ProjectValidator validationRules = new ProjectValidator();
            ValidationResult validationResult = validationRules.Validate(projects);
            var value = _projectRepository.GetByID(projects.ProjectID);
            if (validationResult.IsValid)
            {
                value.ProjectDescription = projects.ProjectDescription;
                value.ProjectGithubURL = projects.ProjectGithubURL;
                if (projects.ProjectImage != null)
                {
                    var guid = Guid.NewGuid();
                    var FileExtension = Path.GetExtension(Request.Files[0].FileName);
                    var fullFileName = guid + FileExtension;
                    if (System.IO.File.Exists(Server.MapPath(value.ProjectImage)))
                    {
                        System.IO.File.Delete(Server.MapPath(value.ProjectImage));
                    }
                    value.ProjectImage = "/Images/Projects/" + fullFileName;
                    Request.Files[0].SaveAs(Server.MapPath("~/Images/Projects/" + fullFileName));
                }
                value.ProjectName = projects.ProjectName;
                value.ProjectTitle = projects.ProjectTitle;
                _projectRepository.Update(projects);
                TempData["Result"] = "Kayıt Güncellendi";
                TempData["Icon"] = "success";
                TempData["IsError"] = "true";
                return RedirectToAction("Index");
            }
            else
            {
                var err = string.Join("<br>", validationResult.Errors.Select(y => y.ErrorMessage));
                TempData["Result"] = err;
                TempData["Icon"] = "danger";
                return View(value);
            }

        }
    }
}