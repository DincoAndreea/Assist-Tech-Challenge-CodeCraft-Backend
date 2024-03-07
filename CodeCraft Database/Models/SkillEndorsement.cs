using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore;

namespace Models;

[Collection("SkillEndorsement")]
public class SkillEndorsement
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int ID { get; set; }
    public string TrainingTitle { get; set; }
    public string TrainingDescription { get; set; }
    public MongoDBRef[] ProjectID { get; set; }
}