using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCraft_TeamFinder_Models
{
    public class DeallocationProposal : BaseClass
    {

        [BsonElement("UserID"), BsonRepresentation(BsonType.ObjectId)]
        public string UserID { get; set; }

        [BsonElement("ProjectID"), BsonRepresentation(BsonType.ObjectId)]
        public string ProjectID { get; set; }

        [BsonElement("DeallocationReason"), BsonRepresentation(BsonType.String)]
        public string DeallocationReason { get; set; }

        [BsonElement("Accepted"), BsonRepresentation(BsonType.Boolean)]
        public bool Accepted { get; set; }
    }
}
