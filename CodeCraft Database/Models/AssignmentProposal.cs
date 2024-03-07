using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore;
namespace Models;

public struct WorkHour
{
    public WorkHour(int Hour, int Minutes)
    {
        this.Hour = Hour;
        this.Minutes = Minutes;
    }
    int Hour { get; set; }
    int Minutes { get; set; }
};

[Collection("AssignmentProposal")]
public class AssignmentProposal
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string ID { get; set; }
    public WorkHour WorkHours {  get; set; }
    public bool Accepted {  get; set; }
    public MongoDBRef EmployeeID {  get; set; }
}