using System.Collections.Generic;
using SocialNetwork.Models;

namespace SocialNetwork.Repository.Interfaces
{
    public interface IUserTaskRepository : IEnumerable<UserTaskModel>
    {
        IList<UserTaskModel> GetUserTasks(string id);
        int GetUserTasksAmountById(string id);
    }
}
