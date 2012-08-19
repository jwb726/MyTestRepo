using System.Collections.Generic;
using kBank.Domain.Core;
using kBank.Domain.Primitives;

namespace kBank.Data.Repositories
{
    public interface IUserRepository
    {
        User GetUser(int userId);
        IEnumerable<User> GetAllUsers();
        OperationStatus<User> UpdateUser(User user);
        User CreateUser(User user);
        OperationStatus DeleteUser(int userId);
        IEnumerable<User> GetUsersForProject(int projectId);
    }
}