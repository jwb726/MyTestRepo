using kBank.Data.Core;
using kBank.Domain.Core;

namespace kBank.Data.Migrations
{
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KnowledgeBankDb>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(KnowledgeBankDb context)
        {
            context.ProjectUserTypes.AddOrUpdate(put => put.Description, new ProjectUserType { Description = "Owner" });
            context.ProjectUserTypes.AddOrUpdate(put => put.Description, new ProjectUserType { Description = "Administrator" });
            context.ProjectUserTypes.AddOrUpdate(put => put.Description, new ProjectUserType { Description = "Member" });
            context.ProjectUserTypes.AddOrUpdate(put => put.Description, new ProjectUserType { Description = "Guest" });

            context.Projects.AddOrUpdate(p => p.Name, new Project { Id = 1, Name = "Default" });
            context.Users.AddOrUpdate(u => u.UserName, new User { Id = 1, FirstName = "Jerry", LastName = "Backer", UserName = "Jerry" });
            context.SaveChanges();

            var project = context.Projects.Single(p => p.Name == "Default");
            var user = context.Users.Single(u => u.FirstName == "Jerry" && u.LastName == "Backer");
            var ownerType = context.ProjectUserTypes.Single(put => put.Description == "Owner");

            var projectUser = context.ProjectUsers.SingleOrDefault(pu => pu.User.Id == user.Id && pu.Project.Id == project.Id);
            if (projectUser == null)
            {
                context.ProjectUsers.Add(new ProjectUser { Project = project, User = user, UserType = ownerType});
            }
            else
            {
                projectUser.UserType = ownerType;
            }
        }
    }
}
