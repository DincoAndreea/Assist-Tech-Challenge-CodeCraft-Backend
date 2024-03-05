using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamFinderModels
{
    public class ProjectRole
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("TeamRoleID"), BsonRepresentation(BsonType.ObjectId)]
        public string TeamRoleID { get; set; }

        [BsonElement("EmplopyeeIDs"), BsonRepresentation(BsonType.ObjectId)]
        public string[] EmplopyeeIDs { get; set; }
    }
}
