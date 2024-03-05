using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore;

namespace Models;

[Collection("Notification")]
public class Notification
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public int ID { get; set; }
    public string NotificationType { get; set; }
    public bool Unread { get; set; }
    public MongoDBRef EmployeeID { get; set; }
    public MongoDBRef ActionID {  get; set; }
}
