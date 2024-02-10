using Fitness.Core.Entities;
using Fitness.Core.Enums;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;
public class QuestionMultiLanguageService : IQuestionMultiLanguageService
{
    private readonly IQuestionMultiLanguageRepository _questionMultiLanguageRepository;

    public QuestionMultiLanguageService(IQuestionMultiLanguageRepository questionMultiLanguageRepository)
    {
        _questionMultiLanguageRepository = questionMultiLanguageRepository;
    }

    public Task<IReadOnlyList<QuestionMultiLanguage>> GetAllAsync()
    {
        throw new NotImplementedException();
    }
    
    public async Task<IReadOnlyList<QuestionMultiLanguage>> GetAllByLanguageAsync(Languages language)
    {
        return await _questionMultiLanguageRepository.GetAllByLanguageAsync(language);
    }

    public Task<QuestionMultiLanguage?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(QuestionMultiLanguage entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(QuestionMultiLanguage entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id, int userId)
    {
        throw new NotImplementedException();
    }
}