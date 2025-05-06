using Microsoft.EntityFrameworkCore;
using MyProductivityManager.Core.Context;
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
        private readonly ApplicationDBContext _context;
        public ProjectsTasksRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<ProjectTask> Add(ProjectTask projectTask)
        {
            await _context.ProjectTasks.AddAsync(projectTask);
            await _context.SaveChangesAsync();
            return projectTask;
        }

        public async Task<ProjectTask?> Delete(int id)
        {
            var task = await _context.ProjectTasks.FirstOrDefaultAsync(x => x.Id == id);

            if (task == null)
                return null;

            _context.ProjectTasks.Remove(task);
            await _context.SaveChangesAsync();

            return task;
        }

        public async Task<ProjectTask?> Edit(int id, ProjectTask projectTask)
        {
            var taskToEdit = await _context.ProjectTasks.FirstOrDefaultAsync(x => x.Id == id);

            if (taskToEdit == null)
                return null;

            taskToEdit.Title = projectTask.Title;
            taskToEdit.Description = projectTask.Description;
            taskToEdit.DueDate = projectTask.DueDate;
            taskToEdit.Status = projectTask.Status;
            taskToEdit.Priority = projectTask.Priority;
            taskToEdit.AssignedTo = projectTask.AssignedTo;

            _context.Entry(taskToEdit).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return taskToEdit;
        }

        public async Task<List<ProjectTask>> GetAll()
        {
            return await _context.ProjectTasks.ToListAsync();
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
