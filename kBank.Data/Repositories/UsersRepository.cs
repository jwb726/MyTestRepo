using System.Linq;
using kBank.Data.Core;
using kBank.Domain.Core;

namespace kBank.Data.Repositories
{
    public class UsersRepository : RepositoryBase<KnowledgeBankDb>
    {
        public User GetUser(int userId)
        {
            using (var context = DataContext)
            {
                var user = context.Users.SingleOrDefault(u => u.Id == userId);
                return user;
            }
        }

        public IQueryable<User> GetAllUsers(int userId)
        {
            using (var context = DataContext)
            {
                return context.Users.AsQueryable();
            }
        }
    }
}