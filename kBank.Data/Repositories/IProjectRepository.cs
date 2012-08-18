using System.Collections.Generic;
using kBank.Domain.Core;
using kBank.Domain.Primitives;

namespace kBank.Data.Repositories
{
    public interface IProjectRepository
    {
        Project GetProject(int projectId);
        IEnumerable<Project> GetAllProjects();
        OperationStatus<Project> UpdateProject(Project project);
        Project CreateProject(Project project);
        OperationStatus DeleteProject(int projectId);
    }

    public interface IUserRepository
    {
        User GetUser(int userId);
        IEnumerable<User> GetAllUsers();
        OperationStatus<User> UpdateUser(User user);
        User CreateUser(User user);
        OperationStatus DeleteUser(int userId);
    }
}