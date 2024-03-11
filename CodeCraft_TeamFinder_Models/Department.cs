using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCraft_TeamFinder_Models
{
    public class Department : BaseClass
    {

        [BsonElement("Name"), BsonRepresentation(BsonType.String)]
        public string Name { get; set; }

        [BsonElement("ManagerID"), BsonRepresentation(BsonType.ObjectId)]
        public string? ManagerID { get; set; }

        [BsonElement("OrganizationID"), BsonRepresentation(BsonType.ObjectId)]
        public string OrganizationID { get; set; }
    }

    public class DepartmentDTO : BaseClass
    {
        public string Name { get; set; }
        public string? ManagerID { get; set; }
        public string? ManagerName { get; set; }
        public string OrganizationID { get; set; }
    }

    public class AssignDepartmentManagerDTO
    {
        public string DepartmentID { get; set; }
        public string ManagerID { get; set; }
    }

    public class AddDepartmentMemberDTO
    {
        public string DepartmentID { get; set; }
        public string UserID { get; set; }
    }
}