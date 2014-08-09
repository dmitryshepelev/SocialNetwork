using SocialNetwork.Models;
using SocialNetwork.Repository.Interfaces;

namespace SocialNetwork.Repository.Implementations
{
    public class UserSolvedTaskRepository : GenericRepository<UserSolvedTaskModel>, IUserSolvedTaskRepository
    {
        public UserSolvedTaskRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public int GetUserSolvedTasksAmount(string id)
        {
            return GetAll(x => x.UserId == id).Count;
        }
    }
}