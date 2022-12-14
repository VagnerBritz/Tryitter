using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Tryitter.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(300)]
        public string Message { get; set; }
        public string ImageUrl { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public User User { get; set; }
        [JsonIgnore]
        public int? UserId { get; set; }
    }
}
