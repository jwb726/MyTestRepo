using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

        public IEnumerable<Project> GetProjectsForUser(int userId)
        {
            using (var context = DataContext)
            {
                var projects = context.ProjectUsers
                    .Where(pu => pu.User.Id == userId)
                    .Select(pu => pu.Project)
                    .ToList();

                return projects;
            }
        }

        public Task<OperationStatus<ProjectUser>> AddUserToProject(int projectId, int userId, int userTypeId)
        {
            return Task.Factory.StartNew(f =>
                {
                    using (var context = DataContext)
                    {
                        try
                        {
                            Thread.Sleep(4000);

                            var exists = context.ProjectUsers.Any(
                                    pu => pu.Project.Id == projectId && pu.User.Id == userId);

                            if (exists)
                            {
                                return OperationStatus<ProjectUser>.CreateFromFailure("User already exists");
                            }

                            var userType = context.ProjectUserTypes.Find(userTypeId);

                            if (userType == null)
                            {
                                return OperationStatus<ProjectUser>.CreateFromFailure("Unknown user type");
                            }

                            var project = context.Projects.Find(projectId);

                            if (project == null)
                            {
                                return OperationStatus<ProjectUser>.CreateFromFailure("Unknown project");
                            }

                            var user = context.Users.Find(userId);

                            if (user == null)
                            {
                                return OperationStatus<ProjectUser>.CreateFromFailure("Unknown user");
                            }

                            var projectUser = new ProjectUser
                                {
                                    Project = project,
                                    User = user,
                                    UserType = userType
                                };

                            var resultingUser = context.ProjectUsers.Add(projectUser);
                            context.SaveChanges();

                            return OperationStatus<ProjectUser>.CreateFromResult(resultingUser);
                        }
                        catch (Exception e)
                        {
                            return OperationStatus<ProjectUser>.CreateFromException("Adding user to project failed", e);
                        }
                    }

                }, null);


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