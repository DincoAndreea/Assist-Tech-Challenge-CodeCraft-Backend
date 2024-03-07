using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore;

namespace Models;

[Collection("Employee")]
public class Employee
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public MongoDBRef OrganizationID { get; set; }
    public MongoDBRef DepartmentID { get; set; }
    public MongoDBRef[] ProjectID { get; set; }
    public MongoDBRef[] SkillID { get; set; }
    public MongoDBRef RoleString { get; set; }
}
