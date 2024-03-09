using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCraft_TeamFinder_Models
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
    public class ProjectTeam : BaseClass
    {

        [BsonElement("ProjectID"), BsonRepresentation(BsonType.ObjectId)]
        public string ProjectID { get; set; }

        [BsonElement("TeamMembers")]
        public TeamMembers[] TeamMembers { get; set; }

    }
}
