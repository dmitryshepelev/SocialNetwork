using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SocialNetwork.Models;
using SocialNetwork.Repository.Interfaces;

namespace SocialNetwork.Repository.Implementations
{
    public class ChartRepository : GenericRepository<ChartModel>, IChartRepository
    {
        public ChartRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}