using SocialNetwork.Models;
using SocialNetwork.Repository.Interfaces;

namespace SocialNetwork.Repository.Implementations
{
    public class LikeRepository : GenericRepository<LikeModel>, ILikeRepository
    {
        public LikeRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}