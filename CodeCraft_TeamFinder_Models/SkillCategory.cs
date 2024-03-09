using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCraft_TeamFinder_Models
{
    public class SkillCategory : BaseClass
    {

        [BsonElement("Name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }
    }
}
