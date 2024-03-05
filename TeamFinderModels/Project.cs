using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFinderModels
{
    public class Project
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("Period"), BsonRepresentation(BsonType.String)]
        public string Period { get; set; }

        [BsonElement("StartDate"), BsonRepresentation(BsonType.DateTime)]
        public DateTime StartDate { get; set; }

        [BsonElement("DeadlineDate"), BsonRepresentation(BsonType.DateTime)]
        public DateTime DeadlineDate { get; set; }

        [BsonElement("Status"), BsonRepresentation(BsonType.String)]
        public string Status { get; set; }

        [BsonElement("Description"), BsonRepresentation(BsonType.String)]
        public string Description { get; set; }

        [BsonElement("SkillRequirements"), BsonRepresentation(BsonType.String)]
        public string[] SkillRequirements { get; set; }

        [BsonElement("TechnologyStack"), BsonRepresentation(BsonType.String)]
        public string[] TechnologyStack { get; set; }

        [BsonElement("ActiveMemberIDs"), BsonRepresentation(BsonType.ObjectId)]
        public string[] ActiveMemberIDs { get; set; }

        [BsonElement("PastMemberIDs"), BsonRepresentation(BsonType.ObjectId)]
        public string[] PastMemberIDs { get; set; }

        [BsonElement("ProjectManagerID"), BsonRepresentation(BsonType.ObjectId)]
        public string ProjectManagerID { get; set; }

        [BsonElement("OrganizationID"), BsonRepresentation(BsonType.ObjectId)]
        public string OrganizationID { get; set; }

        [BsonElement("ProjectRoleIDs"), BsonRepresentation(BsonType.ObjectId)]
        public string[] ProjectRoleIDs { get; set; }
    }
}
