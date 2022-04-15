using Balto.Domain.Projects;
using Balto.Domain.Projects.ProjectTables;
using Balto.Domain.Projects.ProjectTasks;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Balto.Application.Projects
{
    public static class Queries
    {
        public static async Task<IEnumerable<Project>> GetAllProjects(this IQueryable<Project> projects)
        {
            return await projects
                .AsNoTracking()
                .ToListAsync();
        }

        public static async Task<Project> GetProjectById(this IQueryable<Project> projects, Guid projectId)
        {
            return await projects
                .Include(p => p.Contributors)
                .Include(p => p.Tables)
                .AsNoTracking()
                .SingleAsync(p => p.Id == projectId);
        }

        public static async Task<IEnumerable<ProjectTable>> GetAllTables(this IQueryable<Project> projects, Guid projectId)
        {
            return await projects
                .Include(p => p.Tables)
                .Where(p => p.Id == projectId)
                .SelectMany(p => p.Tables)
                .AsNoTracking()
                .ToListAsync();
        }

        public static async Task<ProjectTable> GetTableById(this IQueryable<Project> projects, Guid tableId)
        {
            return await projects
                .Include(p => p.Tables)
                .ThenInclude(p => p.Tasks)
                .ThenInclude(p => p.Tags)
                .SelectMany(p => p.Tables)
                .AsNoTracking()
                .SingleAsync(t => t.Id == tableId);
        }

        public static async Task<IEnumerable<ProjectTask>> GetAllTasks(this IQueryable<Project> projects, Guid tableId)
        {
            return await projects
                .Include(p => p.Tables)
                .ThenInclude(p => p.Tasks)
                .ThenInclude(p => p.Tags)
                .SelectMany(p => p.Tables)
                .Where(t => t.Id == tableId)
                .SelectMany(t => t.Tasks)
                .AsNoTracking()
                .ToListAsync();
        }

        public static async Task<ProjectTask> GetTaskById(this IQueryable<Project> projects, Guid taskId)
        {
            return await projects
                .Include(p => p.Tables)
                .ThenInclude(p => p.Tasks)
                .ThenInclude(p => p.Tags) 
                .SelectMany(p => p.Tables)
                .SelectMany(t => t.Tasks)
                .AsNoTracking()
                .SingleAsync(t => t.Id == taskId);
        }
    }
}
