using Fitness.Core.Entities;
using Fitness.Core.Enums;

namespace Fitness.Core.IRepositories;

public interface IQuestionMultiLanguageRepository : IGenericRepository<QuestionMultiLanguage>
{
    Task<IReadOnlyList<QuestionMultiLanguage>> GetAllByLanguageAsync(Languages language);
}