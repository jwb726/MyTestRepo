using System.ComponentModel.DataAnnotations;

namespace kBank.Domain.Core
{
    public class Project
    {
        public int Id { get; set; }

        [MinLength(4), MaxLength(20)]
        public string Name { get; set; }
    }

    public class ProjectUserType
    {
        public int Id { get; set; }
        public string Description { get; set; }
    }

    public class User
    {
        public int Id { get; set; }

        [MinLength(1)]
        public string FirstName { get; set; }

        [MinLength(2)]
        public string LastName { get; set; }

        [MinLength(5)]
        public string UserName { get; set; }
    }

    public class ProjectUser
    {
        public int Id { get; set; }
        public virtual Project Project { get; set; }
        public virtual User User { get; set; }
        public virtual ProjectUserType UserType { get; set; }
    }
}
