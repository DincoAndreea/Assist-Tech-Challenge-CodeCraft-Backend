using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore;

namespace Models;

[Collection("Department")]
public class Department
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }
    public string Name { get; set; }
    public MongoDBRef ManagerID { get; set; }
    public MongoDBRef OrganizationID { get; set; }
    public MongoDBRef[] EmployeeID { get; set; }
}