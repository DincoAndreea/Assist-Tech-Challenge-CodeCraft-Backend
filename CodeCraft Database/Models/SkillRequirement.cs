using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore;

namespace Models;

[Collection("SkillRequirement")]
public class SkillRequirement
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int ID { get; set; }
    public string MinimumLevel {  get; set; }
    public MongoDBRef SkillID { get; set; }
}