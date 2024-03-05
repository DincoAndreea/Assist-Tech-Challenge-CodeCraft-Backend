using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.EntityFrameworkCore;

namespace TeamFinderModels;

[Collection("Organization")]
public class Organization
{
    [BsonId]
    [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
    public string ? Id { get; set; }

    [BsonElement("Name"), BsonRepresentation(BsonType.String)]
    public string Name { get; set; }

    [BsonElement("Address"), BsonRepresentation(BsonType.String)]
    public string Address { get; set; }
}