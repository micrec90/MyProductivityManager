using MyProductivityManager.Core.Interfaces;
using MyProductivityManager.Core.Models.ProjectTasks;
using MyProductivityManager.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProductivityManager.Core.Repositories
{
    public class ProjectsTasksRepository : IProjectsTasksRepository
    {
        public async Task<ProjectTask> Add(ProjectTask projectTask)
        {
            throw new NotImplementedException();
        }

        public async Task<ProjectTask?> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ProjectTask?> Edit(int id, ProjectTask projectTask)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProjectTask>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task<List<ProjectTask>> GetAll(ProjectTaskQueryObject projectTaskQueryObject)
        {
            throw new NotImplementedException();
        }

        public async Task<ProjectTask?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ProjectTaskExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
