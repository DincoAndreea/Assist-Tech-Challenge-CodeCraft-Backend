using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFinderModels
{
    public class AssignmentProposal
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("EmployeeID"), BsonRepresentation(BsonType.ObjectId)]
        public string EmployeeID { get; set; }

        [BsonElement("ProjectID"), BsonRepresentation(BsonType.ObjectId)]
        public string ProjectID { get; set; }

        [BsonElement("WorkHours"), BsonRepresentation(BsonType.String)]
        public string WorkHours { get; set; }

        [BsonElement("TeamRoleIDs"), BsonRepresentation(BsonType.ObjectId)]
        public string[] TeamRoleIDs { get; set; }

        [BsonElement("Accepted"), BsonRepresentation(BsonType.Boolean)]
        public bool Accepted { get; set; }
    }
}
