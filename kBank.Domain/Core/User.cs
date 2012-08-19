using System.ComponentModel.DataAnnotations;

namespace kBank.Domain.Core
{
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
}