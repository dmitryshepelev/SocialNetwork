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
            return GetAll(x => x.User.Id == id);
        }
    }
}