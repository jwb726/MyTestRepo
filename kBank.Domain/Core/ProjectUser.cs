using System.ComponentModel.DataAnnotations;

namespace kBank.Domain.Core
{
    public class ProjectUser
    {
        public int Id { get; set; }

        [Required]
        public virtual Project Project { get; set; }

        [Required]
        public virtual User User { get; set; }

        [Required]
        public virtual ProjectUserType UserType { get; set; }
    }
}