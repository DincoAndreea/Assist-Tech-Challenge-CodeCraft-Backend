using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore;

namespace Models;

[Collection("Skill")]
public class Skill
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int ID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public MongoDBRef AuthorID { get; set; }
    public MongoDBRef[] DepartmentId { get; set; }
}