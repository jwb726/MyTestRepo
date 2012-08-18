using System.Linq;

namespace kBank.Domain.Core
{
    public interface IKnowledgeBankDataSource
    {
        IQueryable<Project> Projects { get; }
        IQueryable<User> Users { get; }
        IQueryable<ProjectUser> ProjectUsers { get; } 
    }
}
