using System.Linq;
using SocialNetwork.Models;
using SocialNetwork.Repository.Interfaces;

namespace SocialNetwork.Repository.Implementations
{
    public class CategoryRepository : GenericRepository<CategoryModel>, ICategoryRepository
    {
        public CategoryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public int GetId(string categoryName)
        {
            return (from category in dbSet where categoryName == category.CategoryName select category.Id).FirstOrDefault();
        }
    }
}