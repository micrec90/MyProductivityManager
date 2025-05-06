using Microsoft.EntityFrameworkCore;
using MyProductivityManager.Core.Context;
using MyProductivityManager.Core.Interfaces;
using MyProductivityManager.Core.Models.Finance;
using MyProductivityManager.Core.Models.ProjectTasks;
using MyProductivityManager.Core.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyProductivityManager.Core.Repositories
{
    public class ProjectsRepository : IProjectsRepository
    {
        private readonly ApplicationDBContext _context;
        public ProjectsRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        public async Task<Project> Add(Project project)
        {
            await _context.Projects.AddAsync(project);
            await _context.SaveChangesAsync();
            return project;
        }

        public async Task<Project?> Delete(int id)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);

            if (project == null)
                return null;

            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();

            return project;
        }

        public async Task<Project?> Edit(int id, Project project)
        {
            var projectToEdit = await _context.Projects.FirstOrDefaultAsync(x => x.Id == id);

            if (projectToEdit == null)
                return null;

            projectToEdit.Name = project.Name;
            projectToEdit.Description = project.Description;
            projectToEdit.DateUpdate = DateTime.Now;

            _context.Entry(projectToEdit).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return projectToEdit;
        }

        public async Task<List<Project>> GetAll()
        {
            return await _context.Projects.Include(t => t.Tasks).ToListAsync();
        }

        public async Task<List<Project>> GetAll(ProjectQueryObject projectQueryObject)
        {
            throw new NotImplementedException();
        }

        public async Task<Project?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> ProjectExists(int id)
        {
            throw new NotImplementedException();
        }
    }
}
