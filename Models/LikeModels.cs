using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SocialNetwork.Models
{
    [Table("Likes")]
    public class LikeModel
    {
        [Key]
        public int Id { get; set; }
        public bool LikeValue { get; set; }
        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int UserTaskId { get; set; }
        public virtual UserTaskModel UserTask { get; set; }
    }
}