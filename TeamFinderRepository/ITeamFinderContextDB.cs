using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using MongoFramework;
using TeamFinderModels;

namespace TeamFinderRepository
{
    public interface ITeamFinderContextDB
    {
        MongoDbSet<Organization> Organization { get; set; }
    }
}