using SocialNetwork.Models;
using SocialNetwork.Repository.Interfaces;

namespace SocialNetwork.Repository.Implementations
{
    public class TaskSolutionRepository : GenericRepository<TaskSolutionModel>, ITaskSolutionRepository
    {
        public TaskSolutionRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}