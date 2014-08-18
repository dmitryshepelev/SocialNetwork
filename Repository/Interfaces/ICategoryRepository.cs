using SocialNetwork.Models;

namespace SocialNetwork.Repository.Interfaces
{
    public interface ICategoryRepository : IRepository<CategoryModel>
    {
        int GetId(string categoryName);
    }
}
