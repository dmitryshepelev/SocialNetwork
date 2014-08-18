using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Models;
using SocialNetwork.Repository.Interfaces;

namespace SocialNetwork.Repository.Implementations
{
    public class UserTaskTagRepository : GenericRepository<UserTaskTagModel>, IUserTaskTagsRepository
    {
        public UserTaskTagRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}