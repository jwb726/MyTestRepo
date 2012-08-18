using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using kBank.Data.Core;
using kBank.Domain.Core;
using kBank.Domain.Primitives;

namespace kBank.Data.Repositories
{
    public class UserRepository : RepositoryBase<KnowledgeBankDb>, IUserRepository
    {
        public User GetUser(int userId)
        {
            using (var context = DataContext)
            {
                var user = context.Users.SingleOrDefault(p => p.Id == userId);
                return user;
            }
        }

        public IEnumerable<User> GetAllUsers()
        {
            using (var context = DataContext)
            {
                var users = context.Users.ToList();
                return users;
            }
        }

        public OperationStatus<User> UpdateUser(User user)
        {
            using (var context = DataContext)
            {
                var existingUser = context.Users.Find(user.Id);

                if (existingUser == null)
                {
                    return OperationStatus<User>.CreateNotFound();
                }

                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.UserName = user.UserName;

                try
                {
                    int recordsAffected = context.SaveChanges();
                    return OperationStatus<User>.CreateFromResult(existingUser, recordsAffected);
                }
                catch (DbUpdateConcurrencyException e)
                {
                    return OperationStatus<User>.CreateFromException("Update user failed", e);
                }

            }
        }

        public User CreateUser(User user)
        {
            using (var context = DataContext)
            {
                context.Users.Add(user);
                context.SaveChanges();
                return user;
            }
        }

        public OperationStatus DeleteUser(int userId)
        {
            using (var context = DataContext)
            {
                var user = context.Users.Find(userId);
                if (user != null)
                {
                    context.Users.Remove(user);
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