using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore;

namespace Models;

[Collection("Project")]
public class Project
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }
    public string Name { get; set; }
    public string Period { get; set; }
    public string StartDate { get; set; }
    public string DeadlineDate { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    public string[] TechnologyStack { get; set; }
    public MongoDBRef[] SkillRequirementID { get; set; }
    public MongoDBRef[] EmployeeID { get; set; }
    public MongoDBRef ProjectManagerID { get; set; }
    public MongoDBRef OrganizationID { get; set; }
    public MongoDBRef ProjectRolesID { get; set; }
}