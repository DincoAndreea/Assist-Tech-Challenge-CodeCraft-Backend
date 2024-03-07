using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore;

namespace Models;

[Collection("SkillAssignment")]
public class SkillAssignment
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int ID { get; set; }
    public string Level {  get; set; }
    public string Experience { get; set; }
    public MongoDBRef SkillID { get; set; }
    public MongoDBRef SkillEndorsementID {  get; set; }
}