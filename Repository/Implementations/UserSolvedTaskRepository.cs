using System;
using System.Linq;
using SocialNetwork.Models;
using SocialNetwork.Repository.Interfaces;

namespace SocialNetwork.Repository.Implementations
{
    public class UserSolvedTaskRepository : GenericRepository<UserSolvedTaskModel>, IUserSolvedTaskRepository
    {
        public UserSolvedTaskRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public UserSolvedTaskModel GetUserSolvedTask(int taskId, string userId)
        {
            try
            {
                return (from s in GetAll()
                    where s.UserTaskId == taskId
                    where s.UserId == userId
                    select s).Single();
            }
            catch (InvalidOperationException)
            {
                return null;
            }
        }
    }
}