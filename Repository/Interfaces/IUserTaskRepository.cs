using System.Collections.Generic;
using SocialNetwork.Models;

namespace SocialNetwork.Repository.Interfaces
{
    public interface IUserTaskRepository : IRepository<UserTaskModel>
    {
        IList<UserTaskModel> GetUserTasks(string id);
    }
}
