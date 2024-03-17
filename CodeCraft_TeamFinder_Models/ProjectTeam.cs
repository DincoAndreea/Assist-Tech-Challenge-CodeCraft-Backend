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
        public List<string> TeamRoleIDs { get; set; }

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
        public List<TeamMembers>? TeamMembers { get; set; }

    }

    public class TeamFinderRequestDTO
    {
        public bool PartiallyAvailable { get; set; }
        public bool ProjectsCloseToFinish { get; set; }
        public bool Unavailable { get; set; }
        public bool Available { get; set; }
        public bool PastExperience {  get; set; }
        public int? Weeks { get; set; }
        public List<string> TechnologyStack { get; set; }
        public List<string> TeamRoleIDs { get; set; }
        public List<SkillRequirements>? SkillRequirements { get; set; }
        public string OrganizationID { get; set; }
    }

    public class TeamFinderResponseDTO
    {
        public User? User { get; set; }
        public int WorkHours { get; set; }
    }

    public class TeamFinderOpenAI
    {
        public Project Project { get; set; }
        public string? AdditionalContext { get; set; }
    }

    public class ProjectTeamMembersDTO
    {
        public List<User>? ProposedMembers { get; set; }
        public List<User>? ActiveMembers { get; set; }
        public List<User>? PastMembers { get; set; }
    }
}
