using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFinderModels
{
    public struct Skills
    {
        [BsonElement("SkillID"), BsonRepresentation(BsonType.ObjectId)]
        public string SkillID { get; set; }

        [BsonElement("Level"), BsonRepresentation(BsonType.String)]
        public string Level { get; set; }

        [BsonElement("Experience"), BsonRepresentation(BsonType.String)]
        public string Experience { get; set; }

        [BsonElement("TrainingTitle"), BsonRepresentation(BsonType.String)]
        public string TrainingTitle { get; set; }

        [BsonElement("TrainingDescription"), BsonRepresentation(BsonType.String)]
        public string TrainingDescription { get; set; }

        [BsonElement("ProjectIDs"), BsonRepresentation(BsonType.ObjectId)]
        public string[] ProjectIDs { get; set; }

        [BsonElement("Status"), BsonRepresentation(BsonType.String)]
        public string[] Status { get; set;}
    }
    public class User
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("Email"), BsonRepresentation(BsonType.String)]
        public string Email { get; set; }

        [BsonElement("Password"), BsonRepresentation(BsonType.String)]
        public string Password { get; set; }

        [BsonElement("Name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("OrganizationID"), BsonRepresentation(BsonType.ObjectId)]
        public string OrganizationID { get; set; }

        [BsonElement("DepartmentID"), BsonRepresentation(BsonType.ObjectId)]
        public string DepartmentID { get; set; }

        [BsonElement("ProjectIDs"), BsonRepresentation(BsonType.ObjectId)]
        public string[] ProjectIDs { get; set; }

        [BsonElement("Skills"), BsonRepresentation(BsonType.Document)]
        public Skills[] Skills { get; set; }

        [BsonElement("SystemRoleIDs"), BsonRepresentation(BsonType.ObjectId)]
        public string[] SystemRoleIDs { get; set; }
    }
}
