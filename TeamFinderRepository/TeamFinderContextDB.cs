using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoFramework;
using TeamFinderModels;

namespace TeamFinderRepository
{
    public class TeamFinderContextDB : MongoDbContext, ITeamFinderContextDB
    {
        public TeamFinderContextDB(IMongoDbConnection connection) : base(connection)
        {
        }
        public virtual MongoDbSet<Organization> Organization { get; set; }
    }
}
