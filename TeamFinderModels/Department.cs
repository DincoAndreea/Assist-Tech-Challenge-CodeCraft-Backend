using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace TeamFinderModels
{
    public class Department
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name"), BsonRepresentation(BsonType.ObjectId)]
        public string Name { get; set; }

        [BsonElement("ManagerID"), BsonRepresentation(BsonType.ObjectId)]
        public string ManagerID { get; set; }

        [BsonElement("OrganizationID"), BsonRepresentation(BsonType.ObjectId)]
        public string OrganizationID { get; set; }
    }
}