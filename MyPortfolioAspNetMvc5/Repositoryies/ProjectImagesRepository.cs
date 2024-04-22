using MyPortfolioAspNetMvc5.Models.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MyPortfolioAspNetMvc5.Repositoryies
{
    public class ProjectImagesRepository : GenericRepository<ProjectImages>
    {
        DbCvEntities dbCvEntities = new DbCvEntities();
        public List<ProjectImages> ProjectImagesWithProjects(int id)
        {

            return dbCvEntities.ProjectImages.Include(x => x.Projects).Where(x => x.ProjectID == id).ToList();

        }

        public string GetProjectName(int id)
        {
            var value = dbCvEntities.ProjectImages.FirstOrDefault(x => x.ProjectID == id);
            if (value == null)
            {
                return "Bu projeye henüz hiç ek görsel eklemediniz.";
            }
            else
            {
                return dbCvEntities.ProjectImages.Include(x => x.Projects).FirstOrDefault(x => x.ProjectID == id).Projects.ProjectName + " Görsel Listesi";
            }

          
        }
    }
}