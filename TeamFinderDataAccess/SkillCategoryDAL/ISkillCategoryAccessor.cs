
    using TeamFinderModels;

    namespace TeamFinderDataAccess.SkillCategoryDAL
    {
        public interface ISkillCategoryAccessor
        {
            void AddSkillCategory(SkillCategory skillCategory);
            void DeleteSkillCategory(string id);
            Task<SkillCategory> GetSkillCategory(string id);
            Task<List<SkillCategory>> GetSkillCategorys();
            void UpdateSkillCategory(SkillCategory skillCategory);
        }
    }
    