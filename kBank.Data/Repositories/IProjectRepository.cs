using System.Collections.Generic;
using System.Threading.Tasks;
using kBank.Domain.Core;
using kBank.Domain.Primitives;

namespace kBank.Data.Repositories
{
    public interface IProjectRepository
    {
        Project GetProject(int projectId);
        IEnumerable<Project> GetAllProjects();
        IEnumerable<Project> GetProjectsForUser(int userId);
        OperationStatus<Project> UpdateProject(Project project);
        Project CreateProject(Project project);
        OperationStatus DeleteProject(int projectId);
        Task<OperationStatus<ProjectUser>> AddUserToProject(int projectId, int userId, int userTypeId);
    }
}