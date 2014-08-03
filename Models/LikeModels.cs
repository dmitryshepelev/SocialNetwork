namespace SocialNetwork.Models
{
    public class LikeModel
    {
        public int Id { get; set; }
        public bool LikeValue { get; set; }
        public int UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
        public int UserTaskId { get; set; }
        public virtual UserTaskModel UserTask { get; set; }
    }
}