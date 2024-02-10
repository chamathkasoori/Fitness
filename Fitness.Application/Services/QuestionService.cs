using Fitness.Core.Entities;
using Fitness.Core.Enums;
using Fitness.Core.IRepositories;
using Fitness.Application.IServices;

namespace Fitness.Application.Services;

public class QuestionService : IQuestionService
{
    private readonly IQuestionRepository _questionRepository;

    public QuestionService(IQuestionRepository questionRepository)
    {
        _questionRepository = questionRepository;
    }

    public async Task<IReadOnlyList<Question>> GetAllAsync()
    {
        return await _questionRepository.GetAllAsync();
    }

    public Task<Question?> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task AddAsync(Question entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Question entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id, int userId)
    {
        throw new NotImplementedException();
    }
}