using System.ComponentModel.DataAnnotations;

namespace kBank.Domain.Core
{
    public class Project
    {
        public int Id { get; set; }

        [MinLength(4), MaxLength(20)]
        public string Name { get; set; }
    }
}
