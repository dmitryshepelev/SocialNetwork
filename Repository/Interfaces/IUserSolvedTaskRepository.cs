using System.Collections.Generic;
using SocialNetwork.Models;

namespace SocialNetwork.Repository.Interfaces
{
    public interface IUserSolvedTaskRepository : IRepository<UserSolvedTaskModel>
    {
        UserSolvedTaskModel GetUserSolvedTask(int taskId, string userId);
    }
}
