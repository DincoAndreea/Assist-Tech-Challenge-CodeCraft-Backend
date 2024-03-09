using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCraft_TeamFinder_Models
{
    public class Skill : BaseClass
    {

        [BsonElement("SkillCategoryID"), BsonRepresentation(BsonType.ObjectId)]
        public string SkillCategoryID { get; set; }

        [BsonElement("Name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("Description"), BsonRepresentation(BsonType.String)]
        public string Description { get; set; }

        [BsonElement("AuthorID"), BsonRepresentation(BsonType.ObjectId)]
        public string AuthorID { get; set; }

        [BsonElement("DepartmentID"), BsonRepresentation(BsonType.ObjectId)]
        public string DepartmentID { get; set; }
    }
}
