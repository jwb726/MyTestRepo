using System.Data.Entity;
using System.Linq;
using kBank.Domain.Core;

namespace kBank.Data.Core
{
    public class KnowledgeBankDb : DbContext, IKnowledgeBankDataSource
    {
        public DbSet<Project> Projects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<ProjectUser> ProjectUsers { get; set; }
        public DbSet<ProjectUserType> ProjectUserTypes { get; set; }

        IQueryable<Project> IKnowledgeBankDataSource.Projects
        {
            get { return Projects; }
        }

        IQueryable<User>  IKnowledgeBankDataSource.Users
        {
            get { return Users; }
        }

        IQueryable<ProjectUser> IKnowledgeBankDataSource.ProjectUsers
        {
            get { return ProjectUsers; }
        }
    }
}
