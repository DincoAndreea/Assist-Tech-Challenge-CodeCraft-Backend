using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFinderModels
{
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

        [BsonElement("SkillIDs"), BsonRepresentation(BsonType.ObjectId)]
        public string[] SkillIDs { get; set; }

        [BsonElement("SystemRoleIDs"), BsonRepresentation(BsonType.ObjectId)]
        public string[] SystemRoleIDs { get; set; }
    }
}
