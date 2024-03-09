using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace CodeCraft_TeamFinder_Models
{
    public class Organization : BaseClass
    {

        [BsonElement("Name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("Address"), BsonRepresentation(BsonType.String)]
        public string? Address { get; set; }
    }
}
