using System.Collections.Generic;
using SocialNetwork.Models;
using SocialNetwork.Repository.Interfaces;

namespace SocialNetwork.Repository.Implementations
{
    public class UserTaskRepository : GenericRepository<UserTaskModel>, IUserTaskRepository
    {
        public UserTaskRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public IList<UserTaskModel> GetUserTasks(string id)
        {
            return GetAll(x => x.UserId == id);
        }

        public int GetUserTasksAmountById(string id)
        {
            return GetAll(x => x.UserId == id).Count;
        }
    }
}