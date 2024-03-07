using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore;

namespace Models;

[Collection("Organization")]
public class Organization
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public MongoDBRef[] EmployeeID { get; set; }
}