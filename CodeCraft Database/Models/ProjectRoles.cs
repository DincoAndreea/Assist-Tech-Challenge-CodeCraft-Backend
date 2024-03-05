using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore;

namespace Models;

[Collection("Role")]
public class Role
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }
    public MongoDBRef TeamRoleID { get; set; }
    public MongoDBRef[] EmployeeID { get; set; }
}

[Collection("ProjectRoles")]
public class ProjectRoles
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }
    public MongoDBRef[] RoleID { get; set; }

}

// "Team Lead" -> [EmplyeeList]
// "Technical Lead" -> [EmplyeeList]
// "Scrum Master" -> [EmplyeeList]
// "Frontend Developer" -> [EmplyeeList]
// "Backend Deveoper" -> [EmplyeeList]
// "QA Engineer" -> [EmplyeeList]