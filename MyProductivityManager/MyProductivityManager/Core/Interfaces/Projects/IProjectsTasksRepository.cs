using MyProductivityManager.Core.Models.ProjectTasks;
using MyProductivityManager.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProductivityManager.Core.Interfaces
{
    public interface IProjectsTasksRepository
    {
        Task<List<ProjectTask>> GetAll();
        Task<List<ProjectTask>> GetAll(ProjectTaskQueryObject projectTaskQueryObject);
        Task<ProjectTask?> GetById(int id);
        Task<ProjectTask> Add(ProjectTask projectTask);
        Task<ProjectTask?> Edit(int id, ProjectTask projectTask);
        Task<ProjectTask?> Delete(int id);
        Task<bool> ProjectTaskExists(int id);
    }
}
