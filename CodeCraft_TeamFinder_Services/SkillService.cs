using CodeCraft_TeamFinder_Models;
using CodeCraft_TeamFinder_Repository;
using CodeCraft_TeamFinder_Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeCraft_TeamFinder_Services
{
    public class SkillService : ISkillService
    {
        private readonly IRepository<Skill> _repository;
        private readonly Lazy<ISkillCategoryService> _skillCategoryService;
        private readonly Lazy<IUserService> _userService;

        public SkillService(IRepository<Skill> repository, Lazy<ISkillCategoryService> skillCategoryService, Lazy<IUserService> userService)
        {
            _repository = repository;
            _skillCategoryService = skillCategoryService;
            _userService = userService;
        }

        public async Task<Skill> Get(string id)
        {
            return await _repository.Get(id);
        }

        public async Task<IEnumerable<Skill>> GetAll()
        {
            return await _repository.GetAll();
        }

        public async Task<IEnumerable<Skill>> GetSkillsByOrganization(string id)
        {
            var allSkillCategories = await _skillCategoryService.Value.GetAll();

            if (allSkillCategories != null)
            {
                var skillCategoriesByOrganization = allSkillCategories.Where(x => x.OrganizationID == id).ToList();

                var skills = new List<Skill>();

                foreach (var skillCategory in skillCategoriesByOrganization ?? Enumerable.Empty<SkillCategory>())
                {
                    var skillsByCategory = await this.GetSkillsBySkillCategory(skillCategory.Id);

                    skills.AddRange(skillsByCategory);                    
                }

                return skills;
            }

            return Enumerable.Empty<Skill>();
        }

        public async Task<IEnumerable<Skill>> GetSkillsBySkillCategory(string id)
        {
            return await _repository.Find("SkillCategoryID", id);
        }

        public async Task<IEnumerable<Skill>> GetSkillsByDepartment(string id)
        {
            return await _repository.Find("DepartmentIDs", id);
        }

        public async Task<IEnumerable<Skill>> GetSkillsByAuthor(string id)
        {
            return await _repository.Find("AuthorID", id);
        }

        public async Task<SkillStatisticsResponseDTO> GetSkillStatistics(SkillStatisticsRequestDTO skillStatisticsRequest)
        {
            var usersByDepartment = await _userService.Value.GetUsersByDepartment(skillStatisticsRequest.DepartmentID);

            var usersBySkill = usersByDepartment.Where(x => x.Skills?.Where(y => y.SkillID == skillStatisticsRequest.SkillID).Count() > 0).ToList();
            var usersBySkillLevel1 = usersByDepartment.Where(x => x.Skills?.Where(y => y.SkillID == skillStatisticsRequest.SkillID && y.Level == "Learns").Count() > 0).ToList();
            var usersBySkilllevel2 = usersByDepartment.Where(x => x.Skills?.Where(y => y.SkillID == skillStatisticsRequest.SkillID && y.Level == "Knows").Count() > 0).ToList();
            var usersBySkilllevel3 = usersByDepartment.Where(x => x.Skills?.Where(y => y.SkillID == skillStatisticsRequest.SkillID && y.Level == "Does").Count() > 0).ToList();
            var usersBySkillLevel4 = usersByDepartment.Where(x => x.Skills?.Where(y => y.SkillID == skillStatisticsRequest.SkillID && y.Level == "Helps").Count() > 0).ToList();
            var usersBySkillLevel5 = usersByDepartment.Where(x => x.Skills?.Where(y => y.SkillID == skillStatisticsRequest.SkillID && y.Level == "Teaches").Count() > 0).ToList();

            SkillStatisticsResponseDTO skillStatisticsResponseDTO = new SkillStatisticsResponseDTO
            {
                TotalCountOfUsers = usersBySkill.Count,
                CountOfUsersLevel1 = usersBySkillLevel1.Count,
                CountOfUsersLevel2 = usersBySkilllevel2.Count,
                CountOfUsersLevel3 = usersBySkilllevel3.Count,
                CountOfUsersLevel4 = usersBySkillLevel4.Count,
                CountOfUsersLevel5 = usersBySkillLevel5.Count
            };

            return skillStatisticsResponseDTO;
        }

        public async Task<IEnumerable<SkillValidationDTO>> GetSkillValidationProposals()
        {
            var allUsers = await _userService.Value.GetAll();

            List<SkillValidationDTO> skillValidationDTOs = new List<SkillValidationDTO>();

            var usersForSkillValidation = allUsers.Where(x => x.Skills != null && x.Skills.Where(y => y.Status == "Pending").Count() > 0).ToList();

            foreach (var user in usersForSkillValidation)
            {
                var skills = user.Skills;

                foreach (var skill in skills ?? Enumerable.Empty<Skills>())
                {
                    var skillDetails = await _repository.Get(skill.SkillID);

                    SkillValidationDTO skillValidationDTO = new SkillValidationDTO { EmployeeName = user.Name, Experience = skill.Experience, Level = skill.Level, Skill = skillDetails.Name, SkillID = skill.SkillID };

                    skillValidationDTOs.Add(skillValidationDTO);
                }
            }

            return skillValidationDTOs;
        }

        public async Task<bool> AcceptSkillValidation(SkillValidationStatusDTO skillValidationStatusDTO)
        {
            var user = await _userService.Value.Get(skillValidationStatusDTO.EmployeeID);

            var userSkills = user.Skills;

            if (userSkills != null)
            {
                var skill = userSkills.Where(x => x.SkillID ==  skillValidationStatusDTO.SkillID).FirstOrDefault();

                if (skillValidationStatusDTO.Status == "Accepted")
                {
                    user.Skills.Remove(skill);

                    skill.Status = skillValidationStatusDTO.Status;

                    user.Skills.Add(skill);

                    bool success = await _userService.Value.Update(user);

                    return success;
                }

                if (skillValidationStatusDTO.Status == "Rejected")
                {
                    user.Skills.Remove(skill);

                    bool success = await _userService.Value.Update(user);

                    return success;
                }
            }

            return false;
        }

        public async Task<IEnumerable<Skill>> Find(string fieldName, string fieldValue)
        {
            return await _repository.Find(fieldName, fieldValue);
        }

        public async Task<bool> Create(Skill skill)
        {
            return await _repository.Create(skill);
        }

        public async Task<bool> Update(Skill skill)
        {
            return await _repository.Update(skill);
        }

        public async Task<bool> Delete(string id)
        {
            return await _repository.Delete(id);
        }
    }
}
