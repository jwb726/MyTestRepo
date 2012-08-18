using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using kBank.Data.Core;
using kBank.Domain.Core;
using kBank.Domain.Primitives;

namespace kBank.Data.Repositories
{
    public class ProjectRepository : RepositoryBase<KnowledgeBankDb>, IProjectRepository
    {
        public Project GetProject(int projectId)
        {
            using (var context = DataContext)
            {
                var project = context.Projects.SingleOrDefault(p => p.Id == projectId);
                return project;
            }
        }

        public IEnumerable<Project> GetAllProjects()
        {
            using (var context = DataContext)
            {
                var projects = context.Projects.ToList();
                return projects;
            }
        }

        public OperationStatus<Project> UpdateProject(Project project)
        {
            using (var context = DataContext)
            {
                var existingProject = context.Projects.Find(project.Id);
                existingProject.Name = project.Name;

                try
                {
                    int recordsAffected = context.SaveChanges();
                    return OperationStatus<Project>.CreateFromResult(existingProject, recordsAffected);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return OperationStatus<Project>.CreateFromException("Update project failed", e);
                }

            }
        }

        public Project CreateProject(Project project)
        {
            using (var context = DataContext)
            {
                context.Projects.Add(project);
                context.SaveChanges();
                return project;
            }
        }

        public OperationStatus DeleteProject(int projectId)
        {
            using (var context = DataContext)
            {
                var project = context.Projects.Find(projectId);
                if (project != null)
                {
                    context.Projects.Remove(project);
                    try
                    {
                        context.SaveChanges();
                        return new OperationStatus { Succeeded = true };
                    }
                    catch (DbUpdateConcurrencyException e)
                    {
                        return OperationStatus.CreateFromException("DeleteProject failed", e);
                    }
                }

                return new OperationStatus { Succeeded = true };
            }
        }
    }
}