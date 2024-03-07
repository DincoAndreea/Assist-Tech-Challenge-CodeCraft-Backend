using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore;

namespace Models;
[Collection("DeallocationProposal")]
public class DeallocationProposal
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int ID { get; set; }
    public string DeallocationReason {  get; set; }
    public bool Accepted { get; set; }
    public MongoDBRef EmployeeID {  get; set; }
    public MongoDBRef ProjectID {  get; set; }
}