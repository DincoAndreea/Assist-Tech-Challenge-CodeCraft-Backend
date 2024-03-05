
    using TeamFinderModels;

    namespace TeamFinderDataAccess.SkillValidationDAL
    {
        public interface ISkillValidationAccessor
        {
            void AddSkillValidation(SkillValidation skillvalidation);
            void DeleteSkillValidation(string id);
            Task<SkillValidation> GetSkillValidation(string id);
            Task<List<SkillValidation>> GetSkillValidations();
            void UpdateSkillValidation(SkillValidation skillvalidation);
        }
    }
    