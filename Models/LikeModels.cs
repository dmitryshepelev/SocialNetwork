namespace SocialNetwork.Models
{
    public class LikeModel
    {
        public int Id { get; set; }
        public bool LikeValue { get; set; }
        public int ApplicationUserId { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }
        public int UserTaskId { get; set; }
        public virtual UserTaskModel UserTask { get; set; }
    }
}