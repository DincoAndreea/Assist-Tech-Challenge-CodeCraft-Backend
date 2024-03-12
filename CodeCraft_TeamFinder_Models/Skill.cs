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

        [BsonElement("DepartmentID"), BsonRepresentation(BsonType.ObjectId)]
        public string DepartmentID { get; set; }
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
        public int CountOfUserslevel2 { get; set; }
        public int CountOfUsersLevel3 { get; set; }
        public int CountOfUsersLevel4 { get; set; }
        public int CountOfUsersLevel5 { get; set; }

    }

    public class SkillEndorsementDTO
    {
        public string UserID { get; set; }
        public string TrainingTitle { get; set; }
        public string TrainingDescription { get; set; }
        public string[]? ProjectIDs { get; set; }
    }
}
