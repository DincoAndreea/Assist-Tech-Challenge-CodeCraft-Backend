using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCraft_TeamFinder_Models
{
    public class Skill : BaseClass
    {

        [BsonElement("SkillCategoryID"), BsonRepresentation(BsonType.ObjectId)]
        public string SkillCategoryID { get; set; }

        [BsonElement("Name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("Description"), BsonRepresentation(BsonType.String)]
        public string Description { get; set; }

        [BsonElement("AuthorID"), BsonRepresentation(BsonType.ObjectId)]
        public string AuthorID { get; set; }

        [BsonElement("DepartmentIDs"), BsonRepresentation(BsonType.ObjectId)]
        public List<string>? DepartmentIDs { get; set; }
    }

    public class SkillStatisticsRequestDTO
    {
        public string DepartmentID { get; set; }
        public string SkillID { get; set; }
    }

    public class SkillStatisticsResponseDTO
    {
        public int TotalCountOfUsers { get; set; }
        public int CountOfUsersLevel1 { get; set; }
        public int CountOfUsersLevel2 { get; set; }
        public int CountOfUsersLevel3 { get; set; }
        public int CountOfUsersLevel4 { get; set; }
        public int CountOfUsersLevel5 { get; set; }

    }

    public class SkillValidationDTO
    {
        public string EmployeeName { get; set; }
        public string SkillID { get; set; }
        public string Skill { get; set; }
        public string Level { get; set; }
        public string Experience { get; set; }
    }

    public class SkillValidationStatusDTO 
    {
        public string EmployeeID { get; set; }
        public string SkillID { get; set; }
        public string Status { get; set; }
    }
}
