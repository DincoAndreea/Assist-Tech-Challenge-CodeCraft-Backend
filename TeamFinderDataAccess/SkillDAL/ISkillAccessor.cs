
    using TeamFinderModels;

    namespace TeamFinderDataAccess.SkillDAL
    {
        public interface ISkillAccessor
        {
            void AddSkill(Skill skill);
            void DeleteSkill(string id);
            Task<Skill> GetSkill(string id);
            Task<List<Skill>> GetSkills();
            void UpdateSkill(Skill skill);
        }
    }
    