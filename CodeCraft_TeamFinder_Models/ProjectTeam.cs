using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCraft_TeamFinder_Models
{
    public struct TeamMembers
    {
        [BsonElement("UserID"), BsonRepresentation(BsonType.ObjectId)]
        public string UserID { get; set; }

        [BsonElement("TeamRoleIDs"), BsonRepresentation(BsonType.ObjectId)]
        public string[] TeamRoleIDs { get; set; }

        [BsonElement("Active"), BsonRepresentation(BsonType.Boolean)]
        public bool Active { get; set; }

        [BsonElement("WorkHours"), BsonRepresentation(BsonType.Int32)]
        public int WorkHours { get; set; }

    }
    public class ProjectTeam : BaseClass
    {

        [BsonElement("ProjectID"), BsonRepresentation(BsonType.ObjectId)]
        public string ProjectID { get; set; }

        [BsonElement("TeamMembers")]
        public TeamMembers[] TeamMembers { get; set; }

    }

    public class TeamFinderRequestDTO
    {
        public bool PartiallyAvailable { get; set; }
        public bool ProjectsCloseToFinish { get; set; }
        public bool Unavailable { get; set; }
        public bool Available { get; set; }
        public int? Weeks { get; set; }
        public string[] TechnologyStack { get; set; }
        public string[] TeamRoleIDs { get; set; }
        public string OrganizationID { get; set; }
    }

    public class TeamFinderResponseDTO
    {
        public User? User { get; set; }
        public int WorkHours { get; set; }
    }

    public class ProjectTeamMembersDTO
    {
        public IEnumerable<User>? ProposedMembers { get; set; }
        public IEnumerable<User>? ActiveMembers { get; set; }
        public IEnumerable<User>? PastMembers { get; set; }
    }
}
