using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFinderModels
{
    public class SkillValidation
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("EmployeeID"), BsonRepresentation(BsonType.ObjectId)]
        public string EmployeeID { get; set; }

        [BsonElement("Level"), BsonRepresentation(BsonType.String)]
        public string Level { get; set; }

        [BsonElement("Experience"), BsonRepresentation(BsonType.String)]
        public string Experience { get; set; }

        [BsonElement("SkillID"), BsonRepresentation(BsonType.ObjectId)]
        public string SkillID { get; set; }

        [BsonElement("Validated"), BsonRepresentation(BsonType.Boolean)]
        public bool Validated { get; set; }
    }
}
