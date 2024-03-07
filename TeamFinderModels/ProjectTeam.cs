using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFinderModels
{
    public struct TeamMembers
    {
        [BsonElement("UserID"), BsonRepresentation(BsonType.ObjectId)]
        public string UserID { get; set; }

        [BsonElement("TeamRoleIDs"), BsonRepresentation(BsonType.ObjectId)]
        public string[] TeamRoleIDs { get; set; }

        [BsonElement("Active"), BsonRepresentation(BsonType.Boolean)]
        public bool Active { get; set; }

        [BsonElement("WorkHours"), BsonRepresentation(BsonType.Int32)]
        public int WorkHours { get; set; }

    }
    public class ProjectTeam
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("ProjectID"), BsonRepresentation(BsonType.ObjectId)]
        public string ProjectID { get; set; }

        [BsonElement("TeamMembers"), BsonRepresentation(BsonType.Document)]
        public TeamMembers[] TeamMembers { get; set; }

    }
}
