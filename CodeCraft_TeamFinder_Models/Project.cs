using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCraft_TeamFinder_Models
{
    public struct SkillRequirements
    {
        [BsonElement("SkillID"), BsonRepresentation(BsonType.ObjectId)]
        public string SkillID { get; set; }

        [BsonElement("MinimumLevel"), BsonRepresentation(BsonType.String)]
        public string MinimumLevel { get; set; }
    }

    public struct ProjectRoles
    {
        [BsonElement("TeamRoleID"), BsonRepresentation(BsonType.ObjectId)]
        public string TeamRoleID { get; set; }

        [BsonElement("MembersCount"), BsonRepresentation(BsonType.Int32)]
        public int MembersCount { get; set; }
    }
    public class Project : BaseClass
    {

        [BsonElement("Name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("Period"), BsonRepresentation(BsonType.String)]
        public string Period { get; set; }

        [BsonElement("StartDate"), BsonRepresentation(BsonType.DateTime)]
        public DateTime StartDate { get; set; }

        [BsonElement("DeadlineDate"), BsonRepresentation(BsonType.DateTime)]
        public DateTime? DeadlineDate { get; set; }

        [BsonElement("Status"), BsonRepresentation(BsonType.String)]
        public string Status { get; set; }

        [BsonElement("Description"), BsonRepresentation(BsonType.String)]
        public string Description { get; set; }

        [BsonElement("SkillRequirements")]
        public SkillRequirements[]? SkillRequirements { get; set; }

        [BsonElement("TechnologyStack"), BsonRepresentation(BsonType.String)]
        public string[] TechnologyStack { get; set; }

        [BsonElement("ProjectManagerID"), BsonRepresentation(BsonType.ObjectId)]
        public string ProjectManagerID { get; set; }

        [BsonElement("OrganizationID"), BsonRepresentation(BsonType.ObjectId)]
        public string OrganizationID { get; set; }

        [BsonElement("ProjectRoles")]
        public ProjectRoles[] ProjectRoles { get; set; }
    }

    public struct ProjectInformation
    {
        public string ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string[] Roles { get; set; }
        public string[] TechnologyStack { get; set; }
    }

    public class EmployeeProjectsDTO
    {
        public IEnumerable<ProjectInformation> CurrentProjects { get; set; }
        public IEnumerable<ProjectInformation> PastProjects { get; set; }
    }

    public struct TeamMembersList
    {
        public IEnumerable<User> CurrentMembers { get; set; }
        public IEnumerable<User> PastMembers { get; set; }
    }

    public class ProjectDetailsDTO : BaseClass
    {
        public string Name { get; set; }
        public string Period { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string[] TechnologyStack { get; set; }
        public TeamMembersList TeamMembers { get; set; }
    }

    public class DepartmentProjectDTO
    {
        public string Name { get; set; }
        public DateTime? DeadlineDate { get; set; }
        public string Status { get; set; }
        public ProjectTeamMembersDTO TeamMembers { get; set; }
    }
}
