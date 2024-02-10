using Fitness.Core.Entities;
using Fitness.Core.Enums;

namespace Fitness.Application.IServices;
public interface IQuestionMultiLanguageService : IGenericService<QuestionMultiLanguage>
{
    Task<IReadOnlyList<QuestionMultiLanguage>> GetAllByLanguageAsync(Languages language);
}