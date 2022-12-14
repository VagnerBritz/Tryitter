using System.ComponentModel.DataAnnotations;
using Tryitter.Enums;
using System.Collections.ObjectModel;

namespace Tryitter.Models
{
    public class User
    {

        public User()
        {
            Posts = new Collection<Post>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string? Name { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? PersonalStatus { get; set; }
        public StudyModule StudyModules { get; set; }
        public string? ProfileImageUrl { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
