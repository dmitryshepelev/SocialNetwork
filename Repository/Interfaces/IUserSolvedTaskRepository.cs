using System.Collections.Generic;
using SocialNetwork.Models;

namespace SocialNetwork.Repository.Interfaces
{
    public interface IUserSolvedTaskRepository : IEnumerable<UserSolvedTaskModel>
    {
        int GetUserSolvedTasksAmount(string id);
    }
}
