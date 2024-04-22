using FluentValidation.Results;
using MyPortfolioAspNetMvc5.DAL;
using MyPortfolioAspNetMvc5.Models.Entity;
using MyPortfolioAspNetMvc5.Repositoryies;
using MyPortfolioAspNetMvc5.ValidationResults.ProjectValidators;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web.Mvc;

namespace MyPortfolioAspNetMvc5.Controllers
{
    [SessionTimeOut]
    public class ProjectsController : Controller
    {
        ProjectRepository _projectRepository = new ProjectRepository();
        ProjectImagesRepository _projectImagesRepository = new ProjectImagesRepository();
        CategoryRepository _categoryRepository = new CategoryRepository();
        public ActionResult Index()
        {
            var values = _projectRepository.FilterList(x => x.IsActive == true);
            return View(values);
        }

        void loadCategoryList()
        {
            List<SelectListItem> values = (from x in _categoryRepository.GetList()
                                           select new SelectListItem
                                           {
                                               Text = x.Name,
                                               Value = x.CategoryID.ToString()
                                           }).ToList();
            ViewBag.categoryList = values;
        }

        [HttpGet]
        public ActionResult AddProject()
        {

            loadCategoryList();
            return View();
        }
        [HttpPost]
        public ActionResult AddProject(Projects projects)
        {
            if (string.IsNullOrEmpty(projects.ProjectImage))
            {
                TempData["Result"] = "Proje görselini seçiniz.";
                TempData["Icon"] = "danger";
                loadCategoryList();
                return View();
            }
            else
            {

                if (projects.CategoryID == null)
                {

                    TempData["Result"] = "Lütfen proje kategorisini kategori seçiniz";
                    TempData["Icon"] = "danger";
                    loadCategoryList();
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
                            projects.IsActive = true;
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
                            loadCategoryList();
                            return View();
                        }
                    }
                    else
                    {

                        TempData["Result"] = "Yüklemek istediğiniz göreslin uzantısı(" + FileExtension + ") desteklenmiyor lütfen .jpg - .jpeg - .png uzantılarıyla tekrar deneyin.";
                        TempData["Icon"] = "danger";
                        loadCategoryList();
                        return View();
                    }
                }



            }


        }


        public ActionResult ChangeProjectStatus(int id)
        {
            var values = _projectRepository.GetByID(id);
            if (values.IsActive == true)
            {
                TempData["Result"] = "Kayıt yayından kaldırıldı.";
                values.IsActive = false;
            }
            else
            {
                TempData["Result"] = "Kayıt yayınlandı.";
                values.IsActive = true;
            }

            _projectRepository.Update(values);

            TempData["Icon"] = "success";
            TempData["IsError"] = "true";

            return RedirectToAction("Index");
        }



        [HttpGet]
        public ActionResult UpdateProjects(int id)
        {
            var value = _projectRepository.GetByID(id);
            loadCategoryList();
            return View(value);

        }
        [HttpPost]
        public ActionResult UpdateProjects(Projects projects)
        {
            if (projects.CategoryID == null)
            {
                TempData["Result"] = "Lütfen kategori seçin";
                TempData["Icon"] = "danger";

                loadCategoryList();
                return View();
            }
            else
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
                    value.CategoryID = projects.CategoryID;
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

                    loadCategoryList();
                    return View(value);
                }

            }

        }



        void dropdown()
        {
            List<SelectListItem> values = (from x in _projectRepository.GetList()
                                           select new SelectListItem()
                                           {
                                               Text = x.ProjectName,
                                               Value = x.ProjectID.ToString()
                                           }).ToList();
            ViewBag.ProjectNameList = values;

        }

        [HttpGet]
        public ActionResult AddProjectImage()
        {
            dropdown();
            return View();
        }
        [HttpPost]
        public ActionResult AddProjectImage(ProjectImages projectImages)
        {

            if (projectImages.ProjectID == null)
            {
                dropdown();
                TempData["Result"] = "Lütfen görseli eklemek istediğiniz projenin adını seçin";
                TempData["Icon"] = "danger";
                return View();
            }
            else
            {
                if (string.IsNullOrEmpty(projectImages.ImageURL))
                {
                    dropdown();
                    TempData["Result"] = "Lütfen görsel seçin.";
                    TempData["Icon"] = "danger";
                    return View();
                }
                else
                {

                    dropdown();
                    var guid = Guid.NewGuid();
                    var ex = Path.GetExtension(Request.Files[0].FileName);
                    bool checkEx = ExtensionValidator.checkExtension(ex);
                    string fullFileName = guid + ex;
                    if (checkEx)
                    {

                        Request.Files[0].SaveAs(Server.MapPath("~/Images/Projects/" + fullFileName));
                        projectImages.ImageURL = "/Images/Projects/" + fullFileName;
                        _projectImagesRepository.Insert(projectImages);
                        TempData["Result"] = "Seçilen görsel eklendi.";
                        TempData["Icon"] = "success";
                        TempData["IsError"] = "true";
                        return View();
                    }
                    else
                    {
                        TempData["Result"] = "Seçtiğiniz dosya uzantısı desteklenmiyor.";
                        TempData["Icon"] = "danger";
                        return View();
                    }
                }
            }

        }
        public ActionResult ProjectsImages(int id)
        {

            var value = _projectImagesRepository.FilterList(x => x.ProjectID == id);

            var ProjectName = _projectImagesRepository.GetProjectName(id);

            ViewBag.ProjectName = ProjectName;


          

            return View(value);
        }

        [HttpGet]
        public ActionResult UpdateProjectImage(int id)
        {

            var value = _projectImagesRepository.GetByID(id);
            return View(value);
        }
        [HttpPost]
        public ActionResult UpdateProjectImage(ProjectImages projectImages)
        {
            var value = _projectImagesRepository.GetByID(projectImages.ProjectImageID);
            if (value != null)
            {
                if (projectImages.ImageURL != null)
                {
                    var guid = Guid.NewGuid();
                    var fileEx = Path.GetExtension(Request.Files[0].FileName);
                    string fullFile = guid + fileEx;

                    if (System.IO.File.Exists(Server.MapPath(value.ImageURL)))
                    {
                        System.IO.File.Delete(Server.MapPath(value.ImageURL));
                    }

                    Request.Files[0].SaveAs(Server.MapPath("~/Images/Projects/" + fullFile));


                    value.ImageURL = "/Images/Projects/" + fullFile;

                    TempData["Result"] = "Kayıt Güncellendi";
                    TempData["Icon"] = "success";
                    TempData["IsError"] = "true";

                    _projectImagesRepository.Update(value);


                    return RedirectToAction("UpdateProjectImage");
                }
                else
                {
                    TempData["Result"] = "Lütfen resim seçiniz.";
                    TempData["Icon"] = "danger";
                    return View(value);
                }
            }
            else
            {
                TempData["Result"] = "bir hata oluştu value değeri null.";
                TempData["Icon"] = "danger";
                return RedirectToAction("Index");
            }



        }


        public ActionResult DeleteProjectImage(int id)
        {
            var value = _projectImagesRepository.GetByID(id);
            _projectImagesRepository.Delete(value);
            TempData["Result"] = "Proje görseli silindi";
            TempData["Icon"] = "danger";
            TempData["IsError"] = "true";

            if (System.IO.File.Exists(Server.MapPath(value.ImageURL)))
            {
                System.IO.File.Delete(Server.MapPath(value.ImageURL));
            }
            return RedirectToAction("Index");
        }


    }
}