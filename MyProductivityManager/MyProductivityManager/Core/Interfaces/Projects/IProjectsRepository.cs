using MyProductivityManager.Core.Models.Finance;
using MyProductivityManager.Core.Models.ProjectTasks;
using MyProductivityManager.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProductivityManager.Core.Interfaces
{
    public interface IProjectsRepository
    {
        Task<List<Project>> GetAll();
        Task<List<Project>> GetAll(ProjectQueryObject projectQueryObject);
        Task<Project?> GetById(int id);
        Task<Project> Add(Project project);
        Task<Project?> Edit(int id, Project project);
        Task<Project?> Delete(int id);
        Task<bool> ProjectExists(int id);
    }
}
